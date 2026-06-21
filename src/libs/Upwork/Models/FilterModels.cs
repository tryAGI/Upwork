using System.Text.Json;
using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// Public marketplace job search filter.
/// </summary>
public sealed record UpworkPublicMarketplaceJobFilter
{
    /// <summary>
    /// Search expression.
    /// </summary>
    [JsonPropertyName("searchExpression_eq")]
    public string? SearchExpression { get; init; }

    /// <summary>
    /// Job type, for example <c>HOURLY</c> or <c>FIXED</c>.
    /// </summary>
    [JsonPropertyName("jobType_eq")]
    public string? JobType { get; init; }

    /// <summary>
    /// Workload to match.
    /// </summary>
    [JsonPropertyName("workload_eq")]
    public string? Workload { get; init; }

    /// <summary>
    /// Required client hire count range.
    /// </summary>
    [JsonPropertyName("clientHiresRange_eq")]
    public UpworkIntRange? ClientHiresRange { get; init; }

    /// <summary>
    /// Required budget range.
    /// </summary>
    [JsonPropertyName("budgetRange_eq")]
    public UpworkIntRange? BudgetRange { get; init; }

    /// <summary>
    /// Required client feedback range.
    /// </summary>
    [JsonPropertyName("clientFeedBackRange_eq")]
    public UpworkFloatRange? ClientFeedbackRange { get; init; }

    /// <summary>
    /// Maximum job age in days.
    /// </summary>
    [JsonPropertyName("daysPosted_eq")]
    public int? DaysPosted { get; init; }

    /// <summary>
    /// Contractor tier.
    /// </summary>
    [JsonPropertyName("contractorTier_eq")]
    public string? ContractorTier { get; init; }

    /// <summary>
    /// Whether client payment must be verified.
    /// </summary>
    [JsonPropertyName("verifiedPaymentOnly_eq")]
    public bool? VerifiedPaymentOnly { get; init; }

    /// <summary>
    /// Proposal count range.
    /// </summary>
    [JsonPropertyName("proposalRange_eq")]
    public UpworkIntRange? ProposalRange { get; init; }

    /// <summary>
    /// Durations to include.
    /// </summary>
    [JsonPropertyName("duration_any")]
    public IReadOnlyList<string>? DurationAny { get; init; }

    /// <summary>
    /// Occupation IDs to include.
    /// </summary>
    [JsonPropertyName("occupationIds_any")]
    public IReadOnlyList<string>? OccupationIdsAny { get; init; }

    /// <summary>
    /// Number of freelancers needed.
    /// </summary>
    [JsonPropertyName("freelancersNeeded_eq")]
    public int? FreelancersNeeded { get; init; }

    /// <summary>
    /// Client or job locations to include.
    /// </summary>
    [JsonPropertyName("locations_any")]
    public IReadOnlyList<string>? LocationsAny { get; init; }

    /// <summary>
    /// Whether to only include enterprise jobs.
    /// </summary>
    [JsonPropertyName("enterpriseOnly_eq")]
    public bool? EnterpriseOnly { get; init; }

    /// <summary>
    /// Geographic area filter.
    /// </summary>
    [JsonPropertyName("area_eq")]
    public UpworkAreaFilter? Area { get; init; }

    /// <summary>
    /// Offset pagination.
    /// </summary>
    [JsonPropertyName("pagination")]
    public UpworkOffsetPagination? Pagination { get; init; }
}

/// <summary>
/// Authenticated marketplace job search filter.
/// </summary>
public sealed record UpworkMarketplaceJobFilter
{
    /// <summary>
    /// Search expression.
    /// </summary>
    [JsonPropertyName("searchExpression_eq")]
    public string? SearchExpression { get; init; }

    /// <summary>
    /// Skill search expression.
    /// </summary>
    [JsonPropertyName("skillExpression_eq")]
    public string? SkillExpression { get; init; }

    /// <summary>
    /// Title search expression.
    /// </summary>
    [JsonPropertyName("titleExpression_eq")]
    public string? TitleExpression { get; init; }

    /// <summary>
    /// Additional search term input.
    /// </summary>
    [JsonPropertyName("searchTerm_eq")]
    public JsonElement? SearchTerm { get; init; }

    /// <summary>
    /// Category IDs to include.
    /// </summary>
    [JsonPropertyName("categoryIds_any")]
    public IReadOnlyList<string>? CategoryIdsAny { get; init; }

    /// <summary>
    /// Subcategory IDs to include.
    /// </summary>
    [JsonPropertyName("subcategoryIds_any")]
    public IReadOnlyList<string>? SubcategoryIdsAny { get; init; }

    /// <summary>
    /// Occupation IDs.
    /// </summary>
    [JsonPropertyName("occupationIds_any")]
    public IReadOnlyList<string>? OccupationIdsAny { get; init; }

    /// <summary>
    /// Ontology skill IDs that all must match.
    /// </summary>
    [JsonPropertyName("ontologySkillIds_all")]
    public IReadOnlyList<string>? OntologySkillIdsAll { get; init; }

    /// <summary>
    /// Return jobs with IDs greater than this ID.
    /// </summary>
    [JsonPropertyName("sinceId_eq")]
    public string? SinceId { get; init; }

    /// <summary>
    /// Return jobs with IDs less than this ID.
    /// </summary>
    [JsonPropertyName("maxId_eq")]
    public string? MaxId { get; init; }

    /// <summary>
    /// Job type.
    /// </summary>
    [JsonPropertyName("jobType_eq")]
    public string? JobType { get; init; }

    /// <summary>
    /// Project duration values.
    /// </summary>
    [JsonPropertyName("duration_any")]
    public IReadOnlyList<string>? DurationAny { get; init; }

    /// <summary>
    /// Workload.
    /// </summary>
    [JsonPropertyName("workload_eq")]
    public string? Workload { get; init; }

    /// <summary>
    /// Required client hire count range.
    /// </summary>
    [JsonPropertyName("clientHiresRange_eq")]
    public UpworkIntRange? ClientHiresRange { get; init; }

    /// <summary>
    /// Required client feedback range.
    /// </summary>
    [JsonPropertyName("clientFeedBackRange_eq")]
    public UpworkFloatRange? ClientFeedbackRange { get; init; }

    /// <summary>
    /// Required fixed-price budget range.
    /// </summary>
    [JsonPropertyName("budgetRange_eq")]
    public UpworkIntRange? BudgetRange { get; init; }

    /// <summary>
    /// Whether client payment must be verified.
    /// </summary>
    [JsonPropertyName("verifiedPaymentOnly_eq")]
    public bool? VerifiedPaymentOnly { get; init; }

    /// <summary>
    /// Whether to only return jobs from previous clients.
    /// </summary>
    [JsonPropertyName("previousClients_eq")]
    public bool? PreviousClients { get; init; }

    /// <summary>
    /// Experience level.
    /// </summary>
    [JsonPropertyName("experienceLevel_eq")]
    public string? ExperienceLevel { get; init; }

    /// <summary>
    /// Client or job locations to include.
    /// </summary>
    [JsonPropertyName("locations_any")]
    public IReadOnlyList<string>? LocationsAny { get; init; }

    /// <summary>
    /// City IDs for local jobs.
    /// </summary>
    [JsonPropertyName("cityId_any")]
    public IReadOnlyList<string>? CityIdsAny { get; init; }

    /// <summary>
    /// ZIP code IDs for local jobs.
    /// </summary>
    [JsonPropertyName("zipCodeId_any")]
    public IReadOnlyList<string>? ZipCodeIdsAny { get; init; }

    /// <summary>
    /// Search radius in miles for city or ZIP filters.
    /// </summary>
    [JsonPropertyName("radius_eq")]
    public int? Radius { get; init; }

    /// <summary>
    /// Area IDs for local jobs.
    /// </summary>
    [JsonPropertyName("areaId_any")]
    public IReadOnlyList<string>? AreaIdsAny { get; init; }

    /// <summary>
    /// Client timezone.
    /// </summary>
    [JsonPropertyName("timezone_eq")]
    public string? Timezone { get; init; }

    /// <summary>
    /// Client US state.
    /// </summary>
    [JsonPropertyName("usState_eq")]
    public string? UsState { get; init; }

    /// <summary>
    /// Maximum job age in days.
    /// </summary>
    [JsonPropertyName("daysPosted_eq")]
    public int? DaysPosted { get; init; }

    /// <summary>
    /// Job posting access realm.
    /// </summary>
    [JsonPropertyName("jobPostingAccess")]
    public string? JobPostingAccess { get; init; }

    /// <summary>
    /// Private Talent Cloud IDs.
    /// </summary>
    [JsonPropertyName("ptcIds_any")]
    public IReadOnlyList<string>? PtcIdsAny { get; init; }

    /// <summary>
    /// Whether to only return Private Talent Cloud jobs.
    /// </summary>
    [JsonPropertyName("ptcOnly_eq")]
    public bool? PtcOnly { get; init; }

    /// <summary>
    /// Whether to only return enterprise jobs.
    /// </summary>
    [JsonPropertyName("enterpriseOnly_eq")]
    public bool? EnterpriseOnly { get; init; }

    /// <summary>
    /// Proposal count range.
    /// </summary>
    [JsonPropertyName("proposalRange_eq")]
    public UpworkIntRange? ProposalRange { get; init; }

    /// <summary>
    /// Geographic area filter.
    /// </summary>
    [JsonPropertyName("area_eq")]
    public UpworkAreaFilter? Area { get; init; }

    /// <summary>
    /// Whether to preserve facets.
    /// </summary>
    [JsonPropertyName("preserveFacet_eq")]
    public string? PreserveFacet { get; init; }

    /// <summary>
    /// Whether to return only jobs matching the authenticated user's location.
    /// </summary>
    [JsonPropertyName("userLocationMatch_eq")]
    public bool? UserLocationMatch { get; init; }

    /// <summary>
    /// Visitor country filter.
    /// </summary>
    [JsonPropertyName("visitorCountry_eq")]
    public string? VisitorCountry { get; init; }

    /// <summary>
    /// Cursor pagination.
    /// </summary>
    [JsonPropertyName("pagination_eq")]
    public UpworkCursorPagination? Pagination { get; init; }
}

/// <summary>
/// Sort attribute for authenticated marketplace job search.
/// </summary>
public sealed record UpworkMarketplaceJobPostingSearchSort(
    [property: JsonPropertyName("field")] string Field,
    [property: JsonPropertyName("sortOrder")] string SortOrder = UpworkSortOrders.Descending);

/// <summary>
/// Vendor proposal filter for freelancer and agency proposal workflows.
/// </summary>
public sealed record UpworkVendorProposalFilter
{
    /// <summary>
    /// Required vendor proposal status.
    /// </summary>
    [JsonPropertyName("status_eq")]
    public string? Status { get; init; }

    /// <summary>
    /// Proposal annotation to include.
    /// </summary>
    [JsonPropertyName("annotations_eq")]
    public string? Annotation { get; init; }

    /// <summary>
    /// Freelancer ID to include.
    /// </summary>
    [JsonPropertyName("freelancerId_eq")]
    public string? FreelancerId { get; init; }

    /// <summary>
    /// Agency or freelancer organization ID to include.
    /// </summary>
    [JsonPropertyName("organizationId_eq")]
    public string? OrganizationId { get; init; }

    /// <summary>
    /// Marketplace job posting IDs to include.
    /// </summary>
    [JsonPropertyName("jobPostingIds_any")]
    public IReadOnlyList<string>? JobPostingIdsAny { get; init; }
}

/// <summary>
/// Sort attribute for vendor proposal search.
/// </summary>
public sealed record UpworkVendorProposalSort(
    [property: JsonPropertyName("field")] string Field,
    [property: JsonPropertyName("sortOrder")] string SortOrder = UpworkSortOrders.Descending);
