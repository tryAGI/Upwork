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
    /// Workload values to match.
    /// </summary>
    [JsonPropertyName("workload_eq")]
    public IReadOnlyList<string>? Workload { get; init; }

    /// <summary>
    /// Required client hire count range.
    /// </summary>
    [JsonPropertyName("clientHiresRange_eq")]
    public UpworkIntRange? ClientHiresRange { get; init; }

    /// <summary>
    /// Required budget range.
    /// </summary>
    [JsonPropertyName("budgetRange_eq")]
    public UpworkFloatRange? BudgetRange { get; init; }

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
    /// Category IDs or slugs to include.
    /// </summary>
    [JsonPropertyName("categories_any")]
    public IReadOnlyList<string>? CategoriesAny { get; init; }

    /// <summary>
    /// Whether client payment must be verified.
    /// </summary>
    [JsonPropertyName("paymentVerified_eq")]
    public bool? PaymentVerified { get; init; }

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
    [JsonPropertyName("occupation_any")]
    public IReadOnlyList<string>? OccupationAny { get; init; }

    /// <summary>
    /// Number of freelancers needed.
    /// </summary>
    [JsonPropertyName("freelancersNeeded_eq")]
    public int? FreelancersNeeded { get; init; }

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
    /// Job category IDs or slugs.
    /// </summary>
    [JsonPropertyName("jobCategoryIds_any")]
    public IReadOnlyList<string>? JobCategoryIdsAny { get; init; }

    /// <summary>
    /// Occupation IDs.
    /// </summary>
    [JsonPropertyName("occupationIds_any")]
    public IReadOnlyList<string>? OccupationIdsAny { get; init; }

    /// <summary>
    /// Skill IDs.
    /// </summary>
    [JsonPropertyName("skillIds_any")]
    public IReadOnlyList<string>? SkillIdsAny { get; init; }

    /// <summary>
    /// One or more job types.
    /// </summary>
    [JsonPropertyName("jobType_any")]
    public IReadOnlyList<string>? JobTypeAny { get; init; }

    /// <summary>
    /// Experience levels.
    /// </summary>
    [JsonPropertyName("experienceLevel_any")]
    public IReadOnlyList<string>? ExperienceLevelAny { get; init; }

    /// <summary>
    /// Project length values.
    /// </summary>
    [JsonPropertyName("projectLength_any")]
    public IReadOnlyList<string>? ProjectLengthAny { get; init; }

    /// <summary>
    /// Client countries.
    /// </summary>
    [JsonPropertyName("clientCountries_any")]
    public IReadOnlyList<string>? ClientCountriesAny { get; init; }

    /// <summary>
    /// Minimum client feedback.
    /// </summary>
    [JsonPropertyName("clientFeedback_gte")]
    public decimal? ClientFeedbackGreaterThanOrEqual { get; init; }

    /// <summary>
    /// Minimum client hire count.
    /// </summary>
    [JsonPropertyName("clientHires_gte")]
    public int? ClientHiresGreaterThanOrEqual { get; init; }

    /// <summary>
    /// Whether client payment must be verified.
    /// </summary>
    [JsonPropertyName("verifiedPaymentOnly_eq")]
    public bool? VerifiedPaymentOnly { get; init; }

    /// <summary>
    /// Whether to include jobs already applied to.
    /// </summary>
    [JsonPropertyName("includeApplied_eq")]
    public bool? IncludeApplied { get; init; }

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
