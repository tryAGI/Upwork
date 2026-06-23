using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// OAuth2 application configuration for Upwork authorization-code and refresh-token grants.
/// </summary>
public sealed record UpworkOAuthConfig
{
    /// <summary>
    /// Creates Upwork OAuth2 application configuration.
    /// </summary>
    public UpworkOAuthConfig(string clientId, string clientSecret, Uri redirectUri)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
        ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
        ArgumentNullException.ThrowIfNull(redirectUri);

        ClientId = clientId;
        ClientSecret = clientSecret;
        RedirectUri = redirectUri;
    }

    /// <summary>
    /// OAuth2 client identifier.
    /// </summary>
    public string ClientId { get; init; }

    /// <summary>
    /// OAuth2 client secret.
    /// </summary>
    public string ClientSecret { get; init; }

    /// <summary>
    /// Redirect URI configured for the Upwork application.
    /// </summary>
    public Uri RedirectUri { get; init; }
}

/// <summary>
/// Upwork OAuth scope labels required by the read-only marketplace job workflow.
/// </summary>
public static class UpworkOAuthScopeNames
{
    /// <summary>
    /// Common entities read-only access.
    /// </summary>
    public const string CommonEntitiesReadOnly = "Common Entities - Read-Only Access";

    /// <summary>
    /// Read marketplace job postings.
    /// </summary>
    public const string ReadMarketplaceJobPostings = "Read marketplace Job Postings";
}

/// <summary>
/// OAuth token response returned by Upwork.
/// </summary>
public sealed class UpworkTokenResponse
{
    /// <summary>
    /// Bearer access token.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    /// <summary>
    /// Refresh token, when returned by the grant type.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    /// <summary>
    /// Token type, usually <c>bearer</c>.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    /// <summary>
    /// Lifetime in seconds.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int? ExpiresIn { get; init; }

    /// <summary>
    /// Granted scope string.
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; init; }

}
