using System.Text.Json.Serialization;

namespace Upwork;

internal sealed record PublicMarketplaceJobPostingsSearchQueryData(
    [property: JsonPropertyName("publicMarketplaceJobPostingsSearch")]
    UpworkPublicMarketplaceJobPostingsSearch? PublicMarketplaceJobPostingsSearch);

internal sealed record MarketplaceJobPostingsSearchQueryData(
    [property: JsonPropertyName("marketplaceJobPostingsSearch")]
    UpworkMarketplaceJobPostingSearchConnection? MarketplaceJobPostingsSearch);

internal sealed record MarketplaceJobPostingQueryData(
    [property: JsonPropertyName("marketplaceJobPosting")]
    UpworkMarketplaceJobPosting? MarketplaceJobPosting);

internal sealed record MarketplaceJobPostingsContentsQueryData(
    [property: JsonPropertyName("marketplaceJobPostingsContents")]
    IReadOnlyList<UpworkMarketplaceJobPostingContentRecord>? MarketplaceJobPostingsContents);
