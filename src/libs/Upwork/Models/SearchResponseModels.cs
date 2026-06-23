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
    /// Linked marketplace job posting aggregate.
    /// </summary>
    public UpworkMarketplaceJobPosting? Job { get; init; }

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
    public string? RecordNumber { get; init; }

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
    public IReadOnlyList<string>? PreferredFreelancerLocation { get; init; }

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
    public string? LocalJobUserDistance { get; init; }

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
    public string? PublishTime { get; init; }

    /// <summary>
    /// Hours inactive.
    /// </summary>
    public int? HoursInactive { get; init; }
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
    /// Company name.
    /// </summary>
    public string? CompanyName { get; init; }

    /// <summary>
    /// EDC user ID.
    /// </summary>
    public string? EdcUserId { get; init; }

    /// <summary>
    /// Platform of the client's last contract.
    /// </summary>
    public string? LastContractPlatform { get; init; }

    /// <summary>
    /// Last contract RID.
    /// </summary>
    public string? LastContractRid { get; init; }

    /// <summary>
    /// Title of the client's last contract.
    /// </summary>
    public string? LastContractTitle { get; init; }

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
    /// Ownership details.
    /// </summary>
    public UpworkMarketplacePostingOwnership? Ownership { get; init; }

    /// <summary>
    /// Job annotations.
    /// </summary>
    public UpworkMarketplaceJobPostingAnnotations? Annotations { get; init; }

    /// <summary>
    /// Job activity statistics.
    /// </summary>
    public UpworkMarketplacePostingActivityStat? ActivityStat { get; init; }

    /// <summary>
    /// Job content.
    /// </summary>
    public UpworkMarketplaceJobPostingContent? Content { get; init; }

    /// <summary>
    /// Attachments.
    /// </summary>
    public IReadOnlyList<UpworkMarketplaceJobPostingAttachment>? Attachments { get; init; }

    /// <summary>
    /// Marketplace classification attributes.
    /// </summary>
    public UpworkMarketplacePostingClassification? Classification { get; init; }

    /// <summary>
    /// Marketplace segmentation metadata.
    /// </summary>
    public UpworkMarketplacePostingSegmentationData? SegmentationData { get; init; }

    /// <summary>
    /// Contract terms advertised on the job.
    /// </summary>
    public UpworkMarketplacePostingContractTerms? ContractTerms { get; init; }

    /// <summary>
    /// Contractor selection criteria advertised on the job.
    /// </summary>
    public UpworkMarketplacePostingContractorSelection? ContractorSelection { get; init; }

    /// <summary>
    /// Additional search info.
    /// </summary>
    public UpworkMarketplaceJobPostingAdditionalSearchInfo? AdditionalSearchInfo { get; init; }

    /// <summary>
    /// Public-facing client company details.
    /// </summary>
    public UpworkMarketplacePublicCompanyInfo? ClientCompanyPublic { get; init; }

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
/// Marketplace job ownership details.
/// </summary>
public sealed record UpworkMarketplacePostingOwnership
{
    /// <summary>
    /// Company owning the job posting.
    /// </summary>
    public UpworkGenericOrganization? Company { get; init; }

    /// <summary>
    /// Team owning the job posting.
    /// </summary>
    public UpworkGenericOrganization? Team { get; init; }
}

/// <summary>
/// Generic organization reference returned by Upwork.
/// </summary>
public sealed record UpworkGenericOrganization
{
    /// <summary>
    /// Organization ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Organization record ID.
    /// </summary>
    public string? Rid { get; init; }

    /// <summary>
    /// Organization name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Organization type.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Legacy organization type.
    /// </summary>
    public string? LegacyType { get; init; }

    /// <summary>
    /// Organization photo URL.
    /// </summary>
    public Uri? PhotoUrl { get; init; }
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
/// Marketplace job activity statistics.
/// </summary>
public sealed record UpworkMarketplacePostingActivityStat
{
    /// <summary>
    /// Bid statistics.
    /// </summary>
    public UpworkApplicationsBidStats? ApplicationsBidStats { get; init; }

    /// <summary>
    /// Job activity metrics.
    /// </summary>
    public UpworkJobActivity? JobActivity { get; init; }
}

/// <summary>
/// Bid statistics for a marketplace job.
/// </summary>
public sealed record UpworkApplicationsBidStats
{
    /// <summary>
    /// Average bid rate.
    /// </summary>
    public UpworkMoney? AvgRateBid { get; init; }

    /// <summary>
    /// Minimum bid rate.
    /// </summary>
    public UpworkMoney? MinRateBid { get; init; }

    /// <summary>
    /// Maximum bid rate.
    /// </summary>
    public UpworkMoney? MaxRateBid { get; init; }

    /// <summary>
    /// Average interviewed bid rate.
    /// </summary>
    public UpworkMoney? AvgInterviewedRateBid { get; init; }
}

/// <summary>
/// Activity metrics for a marketplace job.
/// </summary>
public sealed record UpworkJobActivity
{
    /// <summary>
    /// Last client activity timestamp.
    /// </summary>
    public string? LastClientActivity { get; init; }

    /// <summary>
    /// Number of invites sent.
    /// </summary>
    public int? InvitesSent { get; init; }

    /// <summary>
    /// Total invited to interview.
    /// </summary>
    public int? TotalInvitedToInterview { get; init; }

    /// <summary>
    /// Total hired.
    /// </summary>
    public int? TotalHired { get; init; }

    /// <summary>
    /// Total unanswered invites.
    /// </summary>
    public int? TotalUnansweredInvites { get; init; }

    /// <summary>
    /// Total offers made.
    /// </summary>
    public int? TotalOffered { get; init; }

    /// <summary>
    /// Total recommended candidates.
    /// </summary>
    public int? TotalRecommended { get; init; }
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
/// Marketplace job classification attributes.
/// </summary>
public sealed record UpworkMarketplacePostingClassification
{
    /// <summary>
    /// Primary job category.
    /// </summary>
    public UpworkMarketplacePostingOntologyEntity? Category { get; init; }

    /// <summary>
    /// Job subcategory.
    /// </summary>
    public UpworkMarketplacePostingOntologyEntity? SubCategory { get; init; }

    /// <summary>
    /// Occupation classification.
    /// </summary>
    public UpworkMarketplacePostingOntologyEntity? Occupation { get; init; }

    /// <summary>
    /// Core skills required.
    /// </summary>
    public IReadOnlyList<UpworkMarketplacePostingOntologyEntity>? Skills { get; init; }

    /// <summary>
    /// Additional skills.
    /// </summary>
    public IReadOnlyList<UpworkMarketplacePostingOntologyEntity>? AdditionalSkills { get; init; }
}

/// <summary>
/// Ontology entity returned in marketplace job classification and segmentation.
/// </summary>
public sealed record UpworkMarketplacePostingOntologyEntity
{
    /// <summary>
    /// Entity ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Stable ontology ID.
    /// </summary>
    public string? OntologyId { get; init; }

    /// <summary>
    /// Entity type values.
    /// </summary>
    public IReadOnlyList<string>? Type { get; init; }

    /// <summary>
    /// Entity status.
    /// </summary>
    public string? EntityStatus { get; init; }

    /// <summary>
    /// Preferred display label.
    /// </summary>
    public string? PreferredLabel { get; init; }

    /// <summary>
    /// Entity definition.
    /// </summary>
    public string? Definition { get; init; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public string? CreatedDateTime { get; init; }

    /// <summary>
    /// Modification timestamp.
    /// </summary>
    public string? ModifiedDateTime { get; init; }
}

/// <summary>
/// Marketplace job segmentation metadata.
/// </summary>
public sealed record UpworkMarketplacePostingSegmentationData
{
    /// <summary>
    /// Segmentation values.
    /// </summary>
    public IReadOnlyList<UpworkMarketplacePostingSegmentationValue>? SegmentationValues { get; init; }
}

/// <summary>
/// Marketplace job segmentation value.
/// </summary>
public sealed record UpworkMarketplacePostingSegmentationValue
{
    /// <summary>
    /// Custom segmentation value entered by the client.
    /// </summary>
    public string? CustomValue { get; init; }

    /// <summary>
    /// Segmentation metadata.
    /// </summary>
    public UpworkMarketplacePostingSegmentationInfo? SegmentationInfo { get; init; }
}

/// <summary>
/// Marketplace job segmentation metadata.
/// </summary>
public sealed record UpworkMarketplacePostingSegmentationInfo
{
    /// <summary>
    /// Segmentation info ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Display label.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Reference name.
    /// </summary>
    public string? ReferenceName { get; init; }

    /// <summary>
    /// Sort order.
    /// </summary>
    public int? SortOrder { get; init; }

    /// <summary>
    /// Segmentation type.
    /// </summary>
    public UpworkSegmentationType? SegmentationType { get; init; }

    /// <summary>
    /// Linked skill.
    /// </summary>
    public UpworkMarketplacePostingOntologyEntity? Skill { get; init; }
}

/// <summary>
/// Marketplace segmentation type metadata.
/// </summary>
public sealed record UpworkSegmentationType
{
    /// <summary>
    /// Segmentation type ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Segmentation type name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Reference name.
    /// </summary>
    public string? ReferenceName { get; init; }
}

/// <summary>
/// Marketplace job contract terms.
/// </summary>
public sealed record UpworkMarketplacePostingContractTerms
{
    /// <summary>
    /// Contract start date.
    /// </summary>
    public string? ContractStartDate { get; init; }

    /// <summary>
    /// Contract end date.
    /// </summary>
    public string? ContractEndDate { get; init; }

    /// <summary>
    /// Contract type.
    /// </summary>
    public string? ContractType { get; init; }

    /// <summary>
    /// On-site work requirement.
    /// </summary>
    public string? OnSiteType { get; init; }

    /// <summary>
    /// Number of people to hire.
    /// </summary>
    public int? PersonsToHire { get; init; }

    /// <summary>
    /// Required experience level.
    /// </summary>
    public string? ExperienceLevel { get; init; }

    /// <summary>
    /// Whether the client is unsure about number of people to hire.
    /// </summary>
    public bool? NotSurePersonsToHire { get; init; }

    /// <summary>
    /// Whether the client is unsure about experience level.
    /// </summary>
    public bool? NotSureExperiencelevel { get; init; }

    /// <summary>
    /// Fixed-price terms.
    /// </summary>
    public UpworkFixedPriceContractTerms? FixedPriceContractTerms { get; init; }

    /// <summary>
    /// Hourly terms.
    /// </summary>
    public UpworkHourlyContractTerms? HourlyContractTerms { get; init; }
}

/// <summary>
/// Fixed-price contract terms.
/// </summary>
public sealed record UpworkFixedPriceContractTerms
{
    /// <summary>
    /// Fixed budget amount.
    /// </summary>
    public UpworkMoney? Amount { get; init; }

    /// <summary>
    /// Maximum budget amount.
    /// </summary>
    public UpworkMoney? MaxAmount { get; init; }

    /// <summary>
    /// Expected project duration.
    /// </summary>
    public UpworkEngagementDuration? EngagementDuration { get; init; }
}

/// <summary>
/// Hourly contract terms.
/// </summary>
public sealed record UpworkHourlyContractTerms
{
    /// <summary>
    /// Expected engagement duration.
    /// </summary>
    public UpworkEngagementDuration? EngagementDuration { get; init; }

    /// <summary>
    /// Engagement type.
    /// </summary>
    public string? EngagementType { get; init; }

    /// <summary>
    /// Whether the client is unsure about project duration.
    /// </summary>
    public bool? NotSureProjectDuration { get; init; }

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
}

/// <summary>
/// Marketplace job contractor selection criteria.
/// </summary>
public sealed record UpworkMarketplacePostingContractorSelection
{
    /// <summary>
    /// Proposal requirements for the job posting.
    /// </summary>
    public UpworkMarketplaceProposalRequirements? ProposalRequirement { get; init; }

    /// <summary>
    /// Freelancer qualification criteria.
    /// </summary>
    public UpworkMarketplaceQualification? Qualification { get; init; }

    /// <summary>
    /// Location constraints.
    /// </summary>
    public UpworkMarketplaceLocation? Location { get; init; }
}

/// <summary>
/// Proposal requirements advertised on a marketplace job.
/// </summary>
public sealed record UpworkMarketplaceProposalRequirements
{
    /// <summary>
    /// Whether a cover letter is required.
    /// </summary>
    public bool? CoverLetterRequired { get; init; }

    /// <summary>
    /// Whether freelancer-created milestones are allowed.
    /// </summary>
    public bool? FreelancerMilestonesAllowed { get; init; }

    /// <summary>
    /// Screening questions.
    /// </summary>
    public IReadOnlyList<UpworkMarketplaceQuestion>? ScreeningQuestions { get; init; }
}

/// <summary>
/// Marketplace screening question.
/// </summary>
public sealed record UpworkMarketplaceQuestion
{
    /// <summary>
    /// Question text.
    /// </summary>
    public string? Question { get; init; }

    /// <summary>
    /// Sequence number.
    /// </summary>
    public int? SequenceNumber { get; init; }
}

/// <summary>
/// Marketplace freelancer qualification criteria.
/// </summary>
public sealed record UpworkMarketplaceQualification
{
    /// <summary>
    /// Type of contractor.
    /// </summary>
    public string? ContractorType { get; init; }

    /// <summary>
    /// Required English proficiency.
    /// </summary>
    public string? EnglishProficiency { get; init; }

    /// <summary>
    /// Whether portfolio is required.
    /// </summary>
    public bool? HasPortfolio { get; init; }

    /// <summary>
    /// Minimum hours worked.
    /// </summary>
    public int? HoursWorked { get; init; }

    /// <summary>
    /// Whether rising talent is preferred.
    /// </summary>
    public bool? RisingTalent { get; init; }

    /// <summary>
    /// Minimum job success score.
    /// </summary>
    public int? JobSuccessScore { get; init; }

    /// <summary>
    /// Minimum earning requirement.
    /// </summary>
    public string? MinEarning { get; init; }

    /// <summary>
    /// Preferred freelancer groups.
    /// </summary>
    public IReadOnlyList<UpworkPreferredGroup>? PreferredGroups { get; init; }

    /// <summary>
    /// Preferred tests or certifications.
    /// </summary>
    public IReadOnlyList<UpworkPreferredTest>? PreferenceTests { get; init; }
}

/// <summary>
/// Preferred freelancer group.
/// </summary>
public sealed record UpworkPreferredGroup
{
    /// <summary>
    /// Group ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Group name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Group logo URL.
    /// </summary>
    public Uri? Logo { get; init; }
}

/// <summary>
/// Preferred test or certification.
/// </summary>
public sealed record UpworkPreferredTest
{
    /// <summary>
    /// Test ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Test name.
    /// </summary>
    public string? Name { get; init; }
}

/// <summary>
/// Marketplace location constraints.
/// </summary>
public sealed record UpworkMarketplaceLocation
{
    /// <summary>
    /// Allowed countries.
    /// </summary>
    public IReadOnlyList<string>? Countries { get; init; }

    /// <summary>
    /// Allowed states.
    /// </summary>
    public IReadOnlyList<string>? States { get; init; }

    /// <summary>
    /// Allowed timezones.
    /// </summary>
    public IReadOnlyList<string>? Timezones { get; init; }

    /// <summary>
    /// Whether local check is required.
    /// </summary>
    public bool? LocalCheckRequired { get; init; }

    /// <summary>
    /// Whether this is local-market only.
    /// </summary>
    public bool? LocalMarket { get; init; }

    /// <summary>
    /// Specific geographic areas.
    /// </summary>
    public IReadOnlyList<UpworkArea>? Areas { get; init; }

    /// <summary>
    /// Whether the client is unsure about location preference.
    /// </summary>
    public bool? NotSureLocationPreference { get; init; }

    /// <summary>
    /// Description of local requirement.
    /// </summary>
    public string? LocalDescription { get; init; }

    /// <summary>
    /// Description of location flexibility.
    /// </summary>
    public string? LocalFlexibilityDescription { get; init; }
}

/// <summary>
/// Geographic area element.
/// </summary>
public sealed record UpworkArea
{
    /// <summary>
    /// Area ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Area type.
    /// </summary>
    public string? AreaType { get; init; }

    /// <summary>
    /// Area name.
    /// </summary>
    public string? Name { get; init; }
}

/// <summary>
/// Public-facing marketplace company information.
/// </summary>
public sealed record UpworkMarketplacePublicCompanyInfo
{
    /// <summary>
    /// Company ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Legacy organization type.
    /// </summary>
    public string? LegacyType { get; init; }

    /// <summary>
    /// Whether teams are enabled.
    /// </summary>
    public bool? TeamsEnabled { get; init; }

    /// <summary>
    /// Whether the company can hire.
    /// </summary>
    public bool? CanHire { get; init; }

    /// <summary>
    /// Whether the company is hidden.
    /// </summary>
    public bool? Hidden { get; init; }

    /// <summary>
    /// State.
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// City.
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Timezone.
    /// </summary>
    public string? Timezone { get; init; }

    /// <summary>
    /// Accounting entity code.
    /// </summary>
    public string? AccountingEntity { get; init; }

    /// <summary>
    /// Billing type.
    /// </summary>
    public string? BillingType { get; init; }
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
