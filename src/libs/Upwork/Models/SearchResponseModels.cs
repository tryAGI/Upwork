using System.Text.Json;

namespace Upwork;

/// <summary>
/// Result returned by public marketplace job search.
/// </summary>
public sealed record UpworkPublicMarketplaceJobPostingsSearch
{
    /// <summary>
    /// Matching jobs.
    /// </summary>
    public IReadOnlyList<UpworkPublicMarketplaceJobPosting>? Jobs { get; init; }

    /// <summary>
    /// Paging info.
    /// </summary>
    public UpworkPageInfo? Paging { get; init; }

    /// <summary>
    /// Search facets.
    /// </summary>
    public UpworkPublicMarketplaceJobSearchFacets? Facets { get; init; }
}

/// <summary>
/// Public marketplace job posting.
/// </summary>
public sealed record UpworkPublicMarketplaceJobPosting
{
    /// <summary>
    /// Job ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Job title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public DateTimeOffset? CreatedDateTime { get; init; }

    /// <summary>
    /// Job type.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Upwork ciphertext identifier.
    /// </summary>
    public string? Ciphertext { get; init; }

    /// <summary>
    /// Job description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Matched skills.
    /// </summary>
    public IReadOnlyList<UpworkSkill>? Skills { get; init; }

    /// <summary>
    /// Engagement label.
    /// </summary>
    public string? Engagement { get; init; }

    /// <summary>
    /// Fixed-price amount.
    /// </summary>
    public UpworkMoney? Amount { get; init; }

    /// <summary>
    /// Record number.
    /// </summary>
    public long? Recno { get; init; }

    /// <summary>
    /// Contractor tier.
    /// </summary>
    public string? ContractorTier { get; init; }

    /// <summary>
    /// Job status.
    /// </summary>
    public string? JobStatus { get; init; }

    /// <summary>
    /// Client summary.
    /// </summary>
    public UpworkPublicMarketplaceJobSearchClient? Client { get; init; }

    /// <summary>
    /// Category.
    /// </summary>
    public string? Category { get; init; }

    /// <summary>
    /// Subcategory.
    /// </summary>
    public string? Subcategory { get; init; }

    /// <summary>
    /// Number of freelancers requested.
    /// </summary>
    public int? FreelancersToHire { get; init; }

    /// <summary>
    /// Whether this is an enterprise job.
    /// </summary>
    public bool? EnterpriseJob { get; init; }

    /// <summary>
    /// Job timestamp.
    /// </summary>
    public DateTimeOffset? JobTs { get; init; }

    /// <summary>
    /// Number of applicants.
    /// </summary>
    public int? TotalApplicants { get; init; }

    /// <summary>
    /// Whether preferred freelancer location is mandatory.
    /// </summary>
    public bool? PrefFreelancerLocationMandatory { get; init; }

    /// <summary>
    /// Published timestamp.
    /// </summary>
    public DateTimeOffset? PublishedDateTime { get; init; }

    /// <summary>
    /// Whether this is a local job.
    /// </summary>
    public bool? Local { get; init; }

    /// <summary>
    /// Job locations.
    /// </summary>
    public IReadOnlyList<UpworkFeedJobLocation>? Locations { get; init; }

    /// <summary>
    /// Duration label.
    /// </summary>
    public string? DurationLabel { get; init; }

    /// <summary>
    /// Whether the authenticated user has applied, when returned.
    /// </summary>
    public bool? Applied { get; init; }

    /// <summary>
    /// Ontology skills.
    /// </summary>
    public IReadOnlyList<UpworkOntologyNode>? OntologySkills { get; init; }

    /// <summary>
    /// Duration value.
    /// </summary>
    public string? Duration { get; init; }

    /// <summary>
    /// Hourly budget type.
    /// </summary>
    public string? HourlyBudgetType { get; init; }

    /// <summary>
    /// Minimum hourly budget.
    /// </summary>
    public decimal? HourlyBudgetMin { get; init; }

    /// <summary>
    /// Maximum hourly budget.
    /// </summary>
    public decimal? HourlyBudgetMax { get; init; }

    /// <summary>
    /// Occupation hierarchy.
    /// </summary>
    public UpworkOccupation? Occupations { get; init; }

    /// <summary>
    /// Weekly budget.
    /// </summary>
    public UpworkMoney? WeeklyBudget { get; init; }

    /// <summary>
    /// Engagement duration.
    /// </summary>
    public UpworkEngagementDuration? EngagementDuration { get; init; }
}

/// <summary>
/// Client summary returned by public marketplace job search.
/// </summary>
public sealed record UpworkPublicMarketplaceJobSearchClient
{
    /// <summary>
    /// Total hires.
    /// </summary>
    public int? TotalHires { get; init; }

    /// <summary>
    /// Total posted jobs.
    /// </summary>
    public int? TotalPostedJobs { get; init; }

    /// <summary>
    /// Total reviews.
    /// </summary>
    public int? TotalReviews { get; init; }

    /// <summary>
    /// Total feedback.
    /// </summary>
    public decimal? TotalFeedback { get; init; }

    /// <summary>
    /// Verification status.
    /// </summary>
    public string? VerificationStatus { get; init; }

    /// <summary>
    /// Client location.
    /// </summary>
    public UpworkFeedJobLocation? Location { get; init; }
}

/// <summary>
/// Facets returned by public marketplace job search.
/// </summary>
public sealed record UpworkPublicMarketplaceJobSearchFacets
{
    /// <summary>
    /// Job type facet map.
    /// </summary>
    public JsonElement? JobType { get; init; }

    /// <summary>
    /// Workload facet map.
    /// </summary>
    public JsonElement? Workload { get; init; }

    /// <summary>
    /// Client hires facet map.
    /// </summary>
    public JsonElement? ClientHires { get; init; }

    /// <summary>
    /// Budget facet map.
    /// </summary>
    public JsonElement? Budget { get; init; }

    /// <summary>
    /// Client feedback facet map.
    /// </summary>
    public JsonElement? ClientFeedback { get; init; }

    /// <summary>
    /// Days posted facet map.
    /// </summary>
    public JsonElement? DaysPosted { get; init; }

    /// <summary>
    /// Contractor tier facet map.
    /// </summary>
    public JsonElement? ContractorTier { get; init; }

    /// <summary>
    /// Categories facet map.
    /// </summary>
    public JsonElement? Categories { get; init; }

    /// <summary>
    /// Payment facet map.
    /// </summary>
    public JsonElement? Payment { get; init; }

    /// <summary>
    /// Proposals facet map.
    /// </summary>
    public JsonElement? Proposals { get; init; }

    /// <summary>
    /// Duration facet map.
    /// </summary>
    public JsonElement? Duration { get; init; }

    /// <summary>
    /// Occupations facet map.
    /// </summary>
    public JsonElement? Occupations { get; init; }

    /// <summary>
    /// Freelancers-needed facet map.
    /// </summary>
    public JsonElement? FreelancersNeeded { get; init; }
}

/// <summary>
/// Connection returned by authenticated marketplace job search.
/// </summary>
public sealed record UpworkMarketplaceJobPostingSearchConnection
{
    /// <summary>
    /// Total result count.
    /// </summary>
    public int? TotalCount { get; init; }

    /// <summary>
    /// Result edges.
    /// </summary>
    public IReadOnlyList<UpworkMarketplaceJobPostingSearchEdge>? Edges { get; init; }

    /// <summary>
    /// Page info.
    /// </summary>
    public UpworkPageInfo? PageInfo { get; init; }
}

/// <summary>
/// Authenticated marketplace job search edge.
/// </summary>
public sealed record UpworkMarketplaceJobPostingSearchEdge
{
    /// <summary>
    /// Result cursor.
    /// </summary>
    public string? Cursor { get; init; }

    /// <summary>
    /// Job node.
    /// </summary>
    public UpworkMarketplaceJobPostingSearchResult? Node { get; init; }
}

/// <summary>
/// Authenticated marketplace job search result.
/// </summary>
public sealed record UpworkMarketplaceJobPostingSearchResult
{
    /// <summary>
    /// Job ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Job title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Job description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Upwork ciphertext identifier.
    /// </summary>
    public string? Ciphertext { get; init; }

    /// <summary>
    /// Duration.
    /// </summary>
    public string? Duration { get; init; }

    /// <summary>
    /// Duration label.
    /// </summary>
    public string? DurationLabel { get; init; }

    /// <summary>
    /// Engagement label.
    /// </summary>
    public string? Engagement { get; init; }

    /// <summary>
    /// Fixed-price amount.
    /// </summary>
    public UpworkMoney? Amount { get; init; }

    /// <summary>
    /// Record number.
    /// </summary>
    public long? RecordNumber { get; init; }

    /// <summary>
    /// Experience level.
    /// </summary>
    public string? ExperienceLevel { get; init; }

    /// <summary>
    /// Category.
    /// </summary>
    public string? Category { get; init; }

    /// <summary>
    /// Subcategory.
    /// </summary>
    public string? Subcategory { get; init; }

    /// <summary>
    /// Number of freelancers requested.
    /// </summary>
    public int? FreelancersToHire { get; init; }

    /// <summary>
    /// Relevance metadata.
    /// </summary>
    public UpworkMarketplaceJobPostingRelevance? Relevance { get; init; }

    /// <summary>
    /// Whether this is an enterprise job.
    /// </summary>
    public bool? Enterprise { get; init; }

    /// <summary>
    /// Encoded relevance payload.
    /// </summary>
    public string? RelevanceEncoded { get; init; }

    /// <summary>
    /// Total applicants.
    /// </summary>
    public int? TotalApplicants { get; init; }

    /// <summary>
    /// Preferred freelancer location.
    /// </summary>
    public string? PreferredFreelancerLocation { get; init; }

    /// <summary>
    /// Whether preferred freelancer location is mandatory.
    /// </summary>
    public bool? PreferredFreelancerLocationMandatory { get; init; }

    /// <summary>
    /// Whether this is a premium job.
    /// </summary>
    public bool? Premium { get; init; }

    /// <summary>
    /// Client unsure fields.
    /// </summary>
    public IReadOnlyList<string>? ClientNotSureFields { get; init; }

    /// <summary>
    /// Client private fields.
    /// </summary>
    public IReadOnlyList<string>? ClientPrivateFields { get; init; }

    /// <summary>
    /// Whether the authenticated user already applied.
    /// </summary>
    public bool? Applied { get; init; }

    /// <summary>
    /// Created timestamp.
    /// </summary>
    public DateTimeOffset? CreatedDateTime { get; init; }

    /// <summary>
    /// Published timestamp.
    /// </summary>
    public DateTimeOffset? PublishedDateTime { get; init; }

    /// <summary>
    /// Renewed timestamp.
    /// </summary>
    public DateTimeOffset? RenewedDateTime { get; init; }

    /// <summary>
    /// Client summary.
    /// </summary>
    public UpworkMarketplaceJobSearchClient? Client { get; init; }

    /// <summary>
    /// Skills.
    /// </summary>
    public IReadOnlyList<UpworkSkill>? Skills { get; init; }

    /// <summary>
    /// Occupation hierarchy.
    /// </summary>
    public UpworkOccupation? Occupations { get; init; }

    /// <summary>
    /// Hourly budget type.
    /// </summary>
    public string? HourlyBudgetType { get; init; }

    /// <summary>
    /// Minimum hourly budget.
    /// </summary>
    public UpworkMoney? HourlyBudgetMin { get; init; }

    /// <summary>
    /// Maximum hourly budget.
    /// </summary>
    public UpworkMoney? HourlyBudgetMax { get; init; }

    /// <summary>
    /// Local distance to authenticated user.
    /// </summary>
    public decimal? LocalJobUserDistance { get; init; }

    /// <summary>
    /// Weekly budget.
    /// </summary>
    public UpworkMoney? WeeklyBudget { get; init; }

    /// <summary>
    /// Engagement duration.
    /// </summary>
    public UpworkEngagementDuration? EngagementDuration { get; init; }

    /// <summary>
    /// Total freelancers to hire.
    /// </summary>
    public int? TotalFreelancersToHire { get; init; }

    /// <summary>
    /// Team ID.
    /// </summary>
    public string? TeamId { get; init; }

    /// <summary>
    /// Existing freelancer-client relationship.
    /// </summary>
    public UpworkFreelancerClientRelation? FreelancerClientRelation { get; init; }
}

/// <summary>
/// Relevance metadata returned by authenticated marketplace search.
/// </summary>
public sealed record UpworkMarketplaceJobPostingRelevance
{
    /// <summary>
    /// Relevance ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Effective candidate count.
    /// </summary>
    public int? EffectiveCandidates { get; init; }

    /// <summary>
    /// Recommended effective candidate count.
    /// </summary>
    public int? RecommendedEffectiveCandidates { get; init; }

    /// <summary>
    /// Unique impression count.
    /// </summary>
    public int? UniqueImpressions { get; init; }

    /// <summary>
    /// Publish time.
    /// </summary>
    public DateTimeOffset? PublishTime { get; init; }

    /// <summary>
    /// Hours inactive.
    /// </summary>
    public decimal? HoursInactive { get; init; }
}

/// <summary>
/// Client summary returned by authenticated marketplace job search.
/// </summary>
public sealed record UpworkMarketplaceJobSearchClient
{
    /// <summary>
    /// Member-since timestamp.
    /// </summary>
    public DateTimeOffset? MemberSinceDateTime { get; init; }

    /// <summary>
    /// Total hires.
    /// </summary>
    public int? TotalHires { get; init; }

    /// <summary>
    /// Total posted jobs.
    /// </summary>
    public int? TotalPostedJobs { get; init; }

    /// <summary>
    /// Total spend.
    /// </summary>
    public UpworkMoney? TotalSpent { get; init; }

    /// <summary>
    /// Verification status.
    /// </summary>
    public string? VerificationStatus { get; init; }

    /// <summary>
    /// Client location.
    /// </summary>
    public UpworkClientLocation? Location { get; init; }

    /// <summary>
    /// Total reviews.
    /// </summary>
    public int? TotalReviews { get; init; }

    /// <summary>
    /// Total feedback.
    /// </summary>
    public decimal? TotalFeedback { get; init; }

    /// <summary>
    /// Company RID.
    /// </summary>
    public string? CompanyRid { get; init; }

    /// <summary>
    /// EDC user ID.
    /// </summary>
    public string? EdcUserId { get; init; }

    /// <summary>
    /// Last contract RID.
    /// </summary>
    public string? LastContractRid { get; init; }

    /// <summary>
    /// Company organization UID.
    /// </summary>
    public string? CompanyOrgUid { get; init; }

    /// <summary>
    /// Whether client financial privacy is enabled.
    /// </summary>
    public bool? HasFinancialPrivacy { get; init; }
}

/// <summary>
/// Existing relation between the freelancer and client.
/// </summary>
public sealed record UpworkFreelancerClientRelation
{
    /// <summary>
    /// Company RID.
    /// </summary>
    public string? CompanyRid { get; init; }

    /// <summary>
    /// Company name.
    /// </summary>
    public string? CompanyName { get; init; }

    /// <summary>
    /// EDC user ID.
    /// </summary>
    public string? EdcUserId { get; init; }

    /// <summary>
    /// Last contract platform.
    /// </summary>
    public string? LastContractPlatform { get; init; }

    /// <summary>
    /// Last contract RID.
    /// </summary>
    public string? LastContractRid { get; init; }

    /// <summary>
    /// Last contract title.
    /// </summary>
    public string? LastContractTitle { get; init; }
}

/// <summary>
/// Marketplace job posting details.
/// </summary>
public sealed record UpworkMarketplaceJobPosting
{
    /// <summary>
    /// Job ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Workflow state.
    /// </summary>
    public UpworkMarketplaceJobPostingWorkflowState? WorkFlowState { get; init; }

    /// <summary>
    /// Job annotations.
    /// </summary>
    public UpworkMarketplaceJobPostingAnnotations? Annotations { get; init; }

    /// <summary>
    /// Job content.
    /// </summary>
    public UpworkMarketplaceJobPostingContent? Content { get; init; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public IReadOnlyList<UpworkMarketplaceJobPostingAttachment>? Attachments { get; init; }

    /// <summary>
    /// Additional search info.
    /// </summary>
    public UpworkMarketplaceJobPostingAdditionalSearchInfo? AdditionalSearchInfo { get; init; }

    /// <summary>
    /// Whether the client can receive a contract proposal.
    /// </summary>
    public bool? CanClientReceiveContractProposal { get; init; }
}

/// <summary>
/// Marketplace job workflow state.
/// </summary>
public sealed record UpworkMarketplaceJobPostingWorkflowState
{
    /// <summary>
    /// Close result.
    /// </summary>
    public string? CloseResult { get; init; }

    /// <summary>
    /// Current status.
    /// </summary>
    public string? Status { get; init; }
}

/// <summary>
/// Marketplace job annotations.
/// </summary>
public sealed record UpworkMarketplaceJobPostingAnnotations
{
    /// <summary>
    /// Tags.
    /// </summary>
    public IReadOnlyList<string>? Tags { get; init; }

    /// <summary>
    /// Custom fields.
    /// </summary>
    public IReadOnlyList<UpworkCustomField>? CustomFields { get; init; }
}

/// <summary>
/// Marketplace job content.
/// </summary>
public sealed record UpworkMarketplaceJobPostingContent
{
    /// <summary>
    /// Job title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Job description.
    /// </summary>
    public string? Description { get; init; }
}

/// <summary>
/// Marketplace job attachment.
/// </summary>
public sealed record UpworkMarketplaceJobPostingAttachment
{
    /// <summary>
    /// Attachment ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Sequence number.
    /// </summary>
    public int? SequenceNumber { get; init; }

    /// <summary>
    /// File name.
    /// </summary>
    public string? FileName { get; init; }

    /// <summary>
    /// File size in bytes.
    /// </summary>
    public long? FileSize { get; init; }
}

/// <summary>
/// Marketplace job additional search metadata.
/// </summary>
public sealed record UpworkMarketplaceJobPostingAdditionalSearchInfo
{
    /// <summary>
    /// Highlighted title.
    /// </summary>
    public string? HighlightTitle { get; init; }
}

/// <summary>
/// Marketplace job content record returned by bulk content lookup.
/// </summary>
public sealed record UpworkMarketplaceJobPostingContentRecord
{
    /// <summary>
    /// Job ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Upwork ciphertext identifier.
    /// </summary>
    public string? Ciphertext { get; init; }

    /// <summary>
    /// Job title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Job description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Published timestamp.
    /// </summary>
    public DateTimeOffset? PublishedDateTime { get; init; }

    /// <summary>
    /// Job annotations.
    /// </summary>
    public UpworkMarketplaceJobPostingAnnotations? Annotations { get; init; }
}
