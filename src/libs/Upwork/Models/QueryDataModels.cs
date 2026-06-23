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

internal sealed record CompanySelectorQueryData(
    [property: JsonPropertyName("companySelector")]
    UpworkCompanySelector? CompanySelector);

internal sealed record ProposalMetadataQueryData(
    [property: JsonPropertyName("proposalMetadata")]
    UpworkProposalMetadata? ProposalMetadata);

internal sealed record VendorProposalQueryData(
    [property: JsonPropertyName("vendorProposal")]
    UpworkVendorProposal? VendorProposal);

internal sealed record VendorProposalsQueryData(
    [property: JsonPropertyName("vendorProposals")]
    UpworkVendorProposalsConnection? VendorProposals);
