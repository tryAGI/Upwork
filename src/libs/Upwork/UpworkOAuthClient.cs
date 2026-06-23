using System.Text.Json;

namespace Upwork;

/// <summary>
/// OAuth2 helper for Upwork access-token requests.
/// </summary>
public sealed class UpworkOAuthClient : IDisposable
{
    /// <summary>
    /// Default Upwork OAuth authorization endpoint.
    /// </summary>
    public static readonly Uri DefaultAuthorizationEndpoint = new("https://www.upwork.com/ab/account-security/oauth2/authorize");

    /// <summary>
    /// Default Upwork OAuth token endpoint.
    /// </summary>
    public static readonly Uri DefaultTokenEndpoint = new("https://www.upwork.com/api/v3/oauth2/token");

    private readonly HttpClient _httpClient;
    private readonly bool _disposeHttpClient;
    private readonly UpworkOAuthConfig? _config;

    /// <summary>
    /// Creates an OAuth client.
    /// </summary>
    public UpworkOAuthClient(
        UpworkOAuthConfig? config = null,
        HttpClient? httpClient = null,
        Uri? tokenEndpoint = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _disposeHttpClient = httpClient is null;
        _config = config;
        TokenEndpoint = tokenEndpoint ?? DefaultTokenEndpoint;
    }

    /// <summary>
    /// Token endpoint used by this client.
    /// </summary>
    public Uri TokenEndpoint { get; }

    /// <summary>
    /// Builds the authorization URL for the authorization-code OAuth flow.
    /// </summary>
    public Uri CreateAuthorizationUri(
        string? state = null,
        Uri? authorizationEndpoint = null)
    {
        var config = GetConfig();
        return CreateAuthorizationUri(config.ClientId, config.RedirectUri, state, authorizationEndpoint);
    }

    /// <summary>
    /// Builds the authorization URL for the authorization-code OAuth flow.
    /// </summary>
    public static Uri CreateAuthorizationUri(
        string clientId,
        Uri redirectUri,
        string? state = null,
        Uri? authorizationEndpoint = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentNullException.ThrowIfNull(redirectUri);

        var endpoint = authorizationEndpoint ?? DefaultAuthorizationEndpoint;
        var builder = new UriBuilder(endpoint);
        var query = new List<string>
        {
            $"response_type=code",
            $"client_id={Uri.EscapeDataString(clientId)}",
            $"redirect_uri={Uri.EscapeDataString(redirectUri.ToString())}",
        };

        if (!string.IsNullOrWhiteSpace(state))
        {
            query.Add($"state={Uri.EscapeDataString(state)}");
        }

        builder.Query = string.Join('&', query);
        return builder.Uri;
    }

    /// <summary>
    /// Exchanges an authorization code for access and refresh tokens using configured OAuth settings.
    /// </summary>
    public Task<UpworkTokenResponse> ExchangeAuthorizationCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        var config = GetConfig();
        return ExchangeAuthorizationCodeAsync(
            config.ClientId,
            config.ClientSecret,
            code,
            config.RedirectUri,
            cancellationToken);
    }

    /// <summary>
    /// Exchanges an authorization code for access and refresh tokens.
    /// </summary>
    public Task<UpworkTokenResponse> ExchangeAuthorizationCodeAsync(
        string clientId,
        string clientSecret,
        string code,
        Uri redirectUri,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentNullException.ThrowIfNull(redirectUri);

        return RequestTokenAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["code"] = code,
                ["redirect_uri"] = redirectUri.ToString(),
            },
            [clientSecret, code],
            cancellationToken);
    }

    /// <summary>
    /// Exchanges a refresh token for a new access token using configured OAuth settings.
    /// </summary>
    public Task<UpworkTokenResponse> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var config = GetConfig();
        return RefreshTokenAsync(
            config.ClientId,
            config.ClientSecret,
            refreshToken,
            cancellationToken);
    }

    /// <summary>
    /// Exchanges a refresh token for a new access token.
    /// </summary>
    public Task<UpworkTokenResponse> RefreshTokenAsync(
        string clientId,
        string clientSecret,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
        ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);

        return RequestTokenAsync(
            new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["refresh_token"] = refreshToken,
            },
            [clientSecret, refreshToken],
            cancellationToken);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient.Dispose();
        }
    }

    private async Task<UpworkTokenResponse> RequestTokenAsync(
        IReadOnlyDictionary<string, string> parameters,
        IReadOnlyList<string?> sensitiveValues,
        CancellationToken cancellationToken)
    {
        using var content = new FormUrlEncodedContent(parameters);
        using var request = new HttpRequestMessage(HttpMethod.Post, TokenEndpoint)
        {
            Content = content,
        };
        request.Headers.Accept.ParseAdd("application/json");

        using var response = await _httpClient
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        var responseBody = await response.Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new UpworkHttpException(response.StatusCode, responseBody, sensitiveValues);
        }

        return JsonSerializer.Deserialize(responseBody, UpworkJsonContext.Default.UpworkTokenResponse)
            ?? throw new UpworkInvalidResponseException("The Upwork token response was empty.");
    }

    private UpworkOAuthConfig GetConfig()
    {
        return _config
            ?? throw new InvalidOperationException("This OAuth operation requires UpworkOAuthConfig.");
    }
}
