using System.Text.Json.Serialization;

namespace Upwork;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(PublicMarketplaceJobPostingsSearchQueryData))]
[JsonSerializable(typeof(MarketplaceJobPostingsSearchQueryData))]
[JsonSerializable(typeof(MarketplaceJobPostingQueryData))]
[JsonSerializable(typeof(MarketplaceJobPostingsContentsQueryData))]
internal sealed partial class UpworkInternalJsonContext : JsonSerializerContext;
