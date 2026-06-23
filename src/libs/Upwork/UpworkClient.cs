using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace Upwork;

/// <summary>
/// Client for the Upwork GraphQL API.
/// </summary>
public sealed class UpworkClient : IUpworkClient, IDisposable
{
    /// <summary>
    /// Default Upwork GraphQL endpoint.
    /// </summary>
    public static readonly Uri DefaultEndpoint = new("https://api.upwork.com/graphql");

    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;
    private readonly IUpworkRateLimitHandler? _rateLimitHandler;
    private readonly int _maxRateLimitRetries;
    private string? _accessToken;
    private IUpworkAccessTokenProvider? _accessTokenProvider;
    private string? _tenantId;

    /// <summary>
    /// Creates an Upwork GraphQL client.
    /// </summary>
    public UpworkClient(
        string? accessToken = null,
        string? tenantId = null,
        HttpClient? httpClient = null,
        Uri? endpoint = null)
        : this(
            new UpworkClientOptions
            {
                AccessToken = accessToken,
                TenantId = tenantId,
                Endpoint = endpoint,
            },
            httpClient)
    {
    }

    /// <summary>
    /// Creates an Upwork GraphQL client.
    /// </summary>
    public UpworkClient(UpworkClientOptions options, HttpClient? httpClient = null)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.MaxRateLimitRetries < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(options),
                options.MaxRateLimitRetries,
                "MaxRateLimitRetries cannot be negative.");
        }

        Endpoint = options.Endpoint ?? DefaultEndpoint;
        _httpClient = httpClient ?? new HttpClient();
        _disposeHttpClient = httpClient is null;
        _accessToken = NormalizeAccessToken(options.AccessToken);
        _accessTokenProvider = options.AccessTokenProvider;
        _tenantId = string.IsNullOrWhiteSpace(options.TenantId) ? null : options.TenantId;
        _rateLimitHandler = options.RateLimitHandler;
        _maxRateLimitRetries = options.MaxRateLimitRetries;
    }

    /// <summary>
    /// GraphQL endpoint used by this client.
    /// </summary>
    public Uri Endpoint { get; }

    /// <summary>
    /// Updates the bearer access token used on subsequent calls.
    /// </summary>
    public void SetAccessToken(string? accessToken)
    {
        _accessToken = NormalizeAccessToken(accessToken);
        _accessTokenProvider = null;
    }

    /// <summary>
    /// Updates the optional Upwork tenant header used on subsequent calls.
    /// </summary>
    public void SetTenantId(string? tenantId)
    {
        _tenantId = string.IsNullOrWhiteSpace(tenantId) ? null : tenantId;
    }

    /// <inheritdoc />
    public async Task<JsonDocument> ExecuteRawAsync(
        UpworkGraphQLRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Query);

        var requestJson = JsonSerializer.Serialize(request, UpworkJsonContext.Default.UpworkGraphQLRequest);

        for (var attempt = 0; ; attempt++)
        {
            using var message = new HttpRequestMessage(HttpMethod.Post, Endpoint);
            message.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var accessToken = await ApplyHeadersAsync(message, cancellationToken).ConfigureAwait(false);

            using var response = await _httpClient
                .SendAsync(message, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);
            var responseBody = await response.Content
                .ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return JsonDocument.Parse(responseBody);
            }

            var sensitiveValues = new[] { accessToken };
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                var retryAfter = GetRetryAfter(response.Headers.RetryAfter);
                var redactedBody = UpworkSecretRedactor.Redact(responseBody, sensitiveValues);
                var rateLimitException = new UpworkRateLimitException(
                    response.StatusCode,
                    responseBody,
                    retryAfter,
                    sensitiveValues);

                if (_rateLimitHandler is not null && attempt < _maxRateLimitRetries)
                {
                    var delay = await _rateLimitHandler
                        .GetDelayAsync(
                            new UpworkRateLimitContext(response.StatusCode, attempt, retryAfter, redactedBody),
                            cancellationToken)
                        .ConfigureAwait(false);

                    if (delay is not null)
                    {
                        if (delay.Value > TimeSpan.Zero)
                        {
                            await Task.Delay(delay.Value, cancellationToken).ConfigureAwait(false);
                        }

                        continue;
                    }
                }

                throw rateLimitException;
            }

            throw new UpworkHttpException(response.StatusCode, responseBody, sensitiveValues);
        }
    }

    /// <inheritdoc />
    public async Task<TData?> ExecuteAsync<TData>(
        string query,
        JsonTypeInfo<TData> dataTypeInfo,
        JsonObject? variables = null,
        string? operationName = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(query);
        ArgumentNullException.ThrowIfNull(dataTypeInfo);

        using var document = await ExecuteRawAsync(
            new UpworkGraphQLRequest
            {
                Query = query,
                Variables = variables,
                OperationName = operationName,
            },
            cancellationToken).ConfigureAwait(false);

        var root = document.RootElement;
        ThrowIfGraphQLErrors(root);

        if (!root.TryGetProperty("data", out var dataElement) ||
            dataElement.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return default;
        }

        return JsonSerializer.Deserialize(dataElement, dataTypeInfo);
    }

    /// <inheritdoc />
    public async Task<UpworkPublicMarketplaceJobPostingsSearch> SearchPublicMarketplaceJobPostingsAsync(
        UpworkPublicMarketplaceJobFilter filter,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var variables = new JsonObject
        {
            ["marketPlaceJobFilter"] = JsonSerializer.SerializeToNode(
                filter,
                UpworkJsonContext.Default.UpworkPublicMarketplaceJobFilter),
        };

        var data = await ExecuteAsync(
            UpworkQueries.PublicMarketplaceJobPostingsSearch,
            UpworkInternalJsonContext.Default.PublicMarketplaceJobPostingsSearchQueryData,
            variables,
            "publicMarketplaceJobPostingsSearch",
            cancellationToken).ConfigureAwait(false);

        return data?.PublicMarketplaceJobPostingsSearch
            ?? throw new UpworkInvalidResponseException("The Upwork response did not include publicMarketplaceJobPostingsSearch.");
    }

    /// <inheritdoc />
    public async Task<UpworkMarketplaceJobPostingSearchConnection> SearchMarketplaceJobPostingsAsync(
        UpworkMarketplaceJobFilter? filter = null,
        string searchType = UpworkMarketplaceJobPostingSearchTypes.UserJobsSearch,
        IReadOnlyList<UpworkMarketplaceJobPostingSearchSort>? sortAttributes = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchType);

        var variables = new JsonObject
        {
            ["marketPlaceJobFilter"] = filter is null
                ? null
                : JsonSerializer.SerializeToNode(filter, UpworkJsonContext.Default.UpworkMarketplaceJobFilter),
            ["searchType"] = searchType,
            ["sortAttributes"] = sortAttributes is null
                ? null
                : JsonSerializer.SerializeToNode(
                    sortAttributes,
                    UpworkJsonContext.Default.IReadOnlyListUpworkMarketplaceJobPostingSearchSort),
        };

        var data = await ExecuteAsync(
            UpworkQueries.MarketplaceJobPostingsSearch,
            UpworkInternalJsonContext.Default.MarketplaceJobPostingsSearchQueryData,
            variables,
            "marketplaceJobPostingsSearch",
            cancellationToken).ConfigureAwait(false);

        return data?.MarketplaceJobPostingsSearch
            ?? throw new UpworkInvalidResponseException("The Upwork response did not include marketplaceJobPostingsSearch.");
    }

    /// <inheritdoc />
    public async Task<UpworkMarketplaceJobPosting?> GetMarketplaceJobPostingAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var variables = new JsonObject
        {
            ["id"] = id,
        };

        var data = await ExecuteAsync(
            UpworkQueries.MarketplaceJobPosting,
            UpworkInternalJsonContext.Default.MarketplaceJobPostingQueryData,
            variables,
            "marketplaceJobPosting",
            cancellationToken).ConfigureAwait(false);

        return data?.MarketplaceJobPosting;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<UpworkMarketplaceJobPostingContentRecord>> GetMarketplaceJobPostingsContentsAsync(
        IReadOnlyList<string> ids,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(ids);
        if (ids.Count == 0)
        {
            return [];
        }

        foreach (var id in ids)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(id);
        }

        var variables = new JsonObject
        {
            ["ids"] = JsonSerializer.SerializeToNode(ids, UpworkJsonContext.Default.IReadOnlyListString),
        };

        var data = await ExecuteAsync(
            UpworkQueries.MarketplaceJobPostingsContents,
            UpworkInternalJsonContext.Default.MarketplaceJobPostingsContentsQueryData,
            variables,
            "marketplaceJobPostingsContents",
            cancellationToken).ConfigureAwait(false);

        return data?.MarketplaceJobPostingsContents ?? [];
    }

    /// <inheritdoc />
    public async Task<UpworkCompanySelector> GetCompanySelectorAsync(CancellationToken cancellationToken = default)
    {
        var data = await ExecuteAsync(
            UpworkQueries.CompanySelector,
            UpworkInternalJsonContext.Default.CompanySelectorQueryData,
            operationName: "companySelector",
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return data?.CompanySelector
            ?? throw new UpworkInvalidResponseException("The Upwork response did not include companySelector.");
    }

    /// <inheritdoc />
    public async Task<UpworkProposalMetadata> GetProposalMetadataAsync(
        string? reasonType = null,
        CancellationToken cancellationToken = default)
    {
        var variables = new JsonObject
        {
            ["reasonType"] = string.IsNullOrWhiteSpace(reasonType) ? null : reasonType,
        };

        var data = await ExecuteAsync(
            UpworkQueries.ProposalMetadata,
            UpworkInternalJsonContext.Default.ProposalMetadataQueryData,
            variables,
            "proposalMetadata",
            cancellationToken).ConfigureAwait(false);

        return data?.ProposalMetadata
            ?? throw new UpworkInvalidResponseException("The Upwork response did not include proposalMetadata.");
    }

    /// <inheritdoc />
    public async Task<UpworkVendorProposal?> GetVendorProposalAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        var variables = new JsonObject
        {
            ["id"] = id,
        };

        var data = await ExecuteAsync(
            UpworkQueries.VendorProposal,
            UpworkInternalJsonContext.Default.VendorProposalQueryData,
            variables,
            "vendorProposal",
            cancellationToken).ConfigureAwait(false);

        return data?.VendorProposal;
    }

    /// <inheritdoc />
    public async Task<UpworkVendorProposalsConnection> SearchVendorProposalsAsync(
        UpworkVendorProposalFilter filter,
        UpworkVendorProposalSort? sortAttribute = null,
        UpworkCursorPagination? pagination = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filter);
        ArgumentException.ThrowIfNullOrWhiteSpace(filter.Status);

        sortAttribute ??= new UpworkVendorProposalSort(UpworkVendorProposalSortFields.CreatedDateTime);
        pagination ??= new UpworkCursorPagination(first: 20);

        var variables = new JsonObject
        {
            ["filter"] = JsonSerializer.SerializeToNode(
                filter,
                UpworkJsonContext.Default.UpworkVendorProposalFilter),
            ["sortAttribute"] = JsonSerializer.SerializeToNode(
                sortAttribute,
                UpworkJsonContext.Default.UpworkVendorProposalSort),
            ["pagination"] = JsonSerializer.SerializeToNode(
                pagination,
                UpworkJsonContext.Default.UpworkCursorPagination),
        };

        var data = await ExecuteAsync(
            UpworkQueries.VendorProposals,
            UpworkInternalJsonContext.Default.VendorProposalsQueryData,
            variables,
            "vendorProposals",
            cancellationToken).ConfigureAwait(false);

        return data?.VendorProposals
            ?? throw new UpworkInvalidResponseException("The Upwork response did not include vendorProposals.");
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }
    }

    private static void ThrowIfGraphQLErrors(JsonElement root)
    {
        if (!root.TryGetProperty("errors", out var errorsElement) ||
            errorsElement.ValueKind != JsonValueKind.Array ||
            errorsElement.GetArrayLength() == 0)
        {
            return;
        }

        var errors = JsonSerializer.Deserialize(
            errorsElement,
            UpworkJsonContext.Default.UpworkGraphQLErrorArray) ?? [];

        if (errors.Any(IsMissingScopeError))
        {
            throw new UpworkMissingScopeException(errors);
        }

        throw new UpworkGraphQLException(errors);
    }

    private static bool IsMissingScopeError(UpworkGraphQLError error)
    {
        var message = error.Message ?? string.Empty;
        var extensions = error.Extensions?.GetRawText() ?? string.Empty;
        var text = $"{message} {extensions}";

        return text.Contains("scope", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("permission", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("OAuth", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("forbidden", StringComparison.OrdinalIgnoreCase) ||
            text.Contains("unauthorized", StringComparison.OrdinalIgnoreCase);
    }

    private static TimeSpan? GetRetryAfter(RetryConditionHeaderValue? retryAfter)
    {
        if (retryAfter is null)
        {
            return null;
        }

        if (retryAfter.Delta is { } delta)
        {
            return delta < TimeSpan.Zero ? TimeSpan.Zero : delta;
        }

        if (retryAfter.Date is { } date)
        {
            var delay = date - DateTimeOffset.UtcNow;
            return delay < TimeSpan.Zero ? TimeSpan.Zero : delay;
        }

        return null;
    }

    private static string? NormalizeAccessToken(string? accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return null;
        }

        const string bearerPrefix = "Bearer ";
        return accessToken.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase)
            ? accessToken[bearerPrefix.Length..]
            : accessToken;
    }

    private async ValueTask<string?> ApplyHeadersAsync(
        HttpRequestMessage message,
        CancellationToken cancellationToken)
    {
        message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var accessToken = _accessTokenProvider is null
            ? _accessToken
            : NormalizeAccessToken(await _accessTokenProvider.GetAccessTokenAsync(cancellationToken).ConfigureAwait(false));

        if (accessToken is { Length: > 0 })
        {
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        if (_tenantId is { Length: > 0 })
        {
            message.Headers.TryAddWithoutValidation("X-Upwork-API-TenantId", _tenantId);
        }

        return accessToken;
    }
}
