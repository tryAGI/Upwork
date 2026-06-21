using System.Text.Json;
using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// OAuth token response returned by Upwork.
/// </summary>
public sealed record UpworkTokenResponse
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

    /// <summary>
    /// Additional provider-specific token properties.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalProperties { get; init; }
}
