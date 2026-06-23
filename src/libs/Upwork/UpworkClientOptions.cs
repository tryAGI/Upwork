using System.Net;

namespace Upwork;

/// <summary>
/// Supplies access tokens to the Upwork GraphQL client.
/// </summary>
public interface IUpworkAccessTokenProvider
{
    /// <summary>
    /// Gets the current access token. The SDK does not persist tokens by default.
    /// </summary>
    ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Handles HTTP 429 responses before the SDK retries a request.
/// </summary>
public interface IUpworkRateLimitHandler
{
    /// <summary>
    /// Returns the delay before retrying, or <c>null</c> to stop retrying.
    /// </summary>
    ValueTask<TimeSpan?> GetDelayAsync(
        UpworkRateLimitContext context,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Context passed to rate-limit handlers.
/// </summary>
public sealed record UpworkRateLimitContext
{
    /// <summary>
    /// Creates rate-limit context.
    /// </summary>
    public UpworkRateLimitContext(
        HttpStatusCode statusCode,
        int attempt,
        TimeSpan? retryAfter,
        string? responseBody)
    {
        StatusCode = statusCode;
        Attempt = attempt;
        RetryAfter = retryAfter;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// HTTP status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Zero-based retry attempt.
    /// </summary>
    public int Attempt { get; }

    /// <summary>
    /// Retry delay parsed from the <c>Retry-After</c> header, when provided.
    /// </summary>
    public TimeSpan? RetryAfter { get; }

    /// <summary>
    /// Redacted response body.
    /// </summary>
    public string? ResponseBody { get; }
}

/// <summary>
/// Optional configuration for the Upwork GraphQL client.
/// </summary>
public sealed record UpworkClientOptions
{
    /// <summary>
    /// Bearer access token for simple in-memory usage.
    /// </summary>
    public string? AccessToken { get; init; }

    /// <summary>
    /// Token provider for consuming applications that own token storage and refresh.
    /// </summary>
    public IUpworkAccessTokenProvider? AccessTokenProvider { get; init; }

    /// <summary>
    /// Optional Upwork tenant header value.
    /// </summary>
    public string? TenantId { get; init; }

    /// <summary>
    /// GraphQL endpoint. Defaults to <see cref="UpworkClient.DefaultEndpoint" />.
    /// </summary>
    public Uri? Endpoint { get; init; }

    /// <summary>
    /// Rate-limit handler invoked for HTTP 429 responses.
    /// </summary>
    public IUpworkRateLimitHandler? RateLimitHandler { get; init; }

    /// <summary>
    /// Maximum number of retries after HTTP 429 responses.
    /// </summary>
    public int MaxRateLimitRetries { get; init; }
}
