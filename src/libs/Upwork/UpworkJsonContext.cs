using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// Source-generated JSON metadata for the Upwork SDK.
/// </summary>
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(UpworkGraphQLRequest))]
[JsonSerializable(typeof(UpworkGraphQLError[]))]
[JsonSerializable(typeof(UpworkTokenResponse))]
[JsonSerializable(typeof(UpworkPublicMarketplaceJobFilter))]
[JsonSerializable(typeof(UpworkMarketplaceJobFilter))]
[JsonSerializable(typeof(IReadOnlyList<UpworkMarketplaceJobPostingSearchSort>))]
[JsonSerializable(typeof(IReadOnlyList<string>))]
public sealed partial class UpworkJsonContext : JsonSerializerContext;
