using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// Input for creating a direct upload link for job-application proposal attachments.
/// </summary>
public sealed record UpworkCreateDirectUploadLinkInput(
    [property: JsonPropertyName("fileName")] string FileName)
{
    /// <summary>
    /// Upload-link expiration date.
    /// </summary>
    public string? ExpirationDate { get; init; }

    /// <summary>
    /// Maximum file size in bytes.
    /// </summary>
    public int? MaxFileSize { get; init; }

    /// <summary>
    /// MIME content type.
    /// </summary>
    public string? ContentType { get; init; }

    /// <summary>
    /// Whether SSL is enabled.
    /// </summary>
    public bool? SslEnabled { get; init; }

    /// <summary>
    /// File metadata.
    /// </summary>
    public string? MetaData { get; init; }
}

/// <summary>
/// Direct upload link details returned by Upwork.
/// </summary>
public sealed record UpworkFileInfo
{
    /// <summary>
    /// File identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Upload URL.
    /// </summary>
    public Uri? UploadUrl { get; init; }

    /// <summary>
    /// Form key-values required by the upload endpoint.
    /// </summary>
    public IReadOnlyList<UpworkStringMapElement>? FormKeyValues { get; init; }
}

/// <summary>
/// Metadata reference values for proposal workflows.
/// </summary>
public sealed record UpworkProposalMetadata
{
    /// <summary>
    /// Available engagement duration values.
    /// </summary>
    public IReadOnlyList<UpworkProposalEngagementDuration>? EngagementDurationValues { get; init; }

    /// <summary>
    /// Proposal reasons for the requested reason type.
    /// </summary>
    public IReadOnlyList<UpworkReasonsMetadata>? Reasons { get; init; }
}

/// <summary>
/// Estimated duration option for proposal terms.
/// </summary>
public sealed record UpworkProposalEngagementDuration
{
    /// <summary>
    /// Duration identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Human-readable duration label.
    /// </summary>
    public string? Label { get; init; }
}

/// <summary>
/// Reason metadata returned by proposal metadata queries.
/// </summary>
public sealed record UpworkReasonsMetadata
{
    /// <summary>
    /// Reason identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Reason text.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// Reason alias.
    /// </summary>
    public string? Alias { get; init; }
}

/// <summary>
/// Reason attached to a vendor proposal status.
/// </summary>
public sealed record UpworkProposalReason
{
    /// <summary>
    /// Reason identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Reason text.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// Reason description.
    /// </summary>
    public string? Description { get; init; }
}

/// <summary>
/// Terms submitted with a vendor proposal.
/// </summary>
public sealed record UpworkProposalTerms
{
    /// <summary>
    /// Hourly charge rate or fixed-price charge amount.
    /// </summary>
    public UpworkMoney? ChargeRate { get; init; }

    /// <summary>
    /// Estimated engagement duration.
    /// </summary>
    public UpworkProposalEngagementDuration? EstimatedDuration { get; init; }

    /// <summary>
    /// Upfront payment percent for fixed-price proposals.
    /// </summary>
    public decimal? UpfrontPaymentPercent { get; init; }
}

/// <summary>
/// Project plan submitted with a proposal.
/// </summary>
public sealed record UpworkProposalProjectPlan
{
    /// <summary>
    /// Project plan identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Proposed milestones.
    /// </summary>
    public IReadOnlyList<UpworkProposalMilestone>? Milestones { get; init; }
}

/// <summary>
/// Milestone in a proposal project plan.
/// </summary>
public sealed record UpworkProposalMilestone
{
    /// <summary>
    /// Milestone description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Milestone due date.
    /// </summary>
    public string? DueDate { get; init; }

    /// <summary>
    /// Milestone payment amount.
    /// </summary>
    public string? Amount { get; init; }
}

/// <summary>
/// Audit details for a proposal.
/// </summary>
public sealed record UpworkProposalAuditDetails
{
    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public DateTimeOffset? CreatedDateTime { get; init; }

    /// <summary>
    /// Last modification timestamp.
    /// </summary>
    public DateTimeOffset? ModifiedDateTime { get; init; }
}

/// <summary>
/// Status of a vendor proposal.
/// </summary>
public sealed record UpworkVendorProposalStatus
{
    /// <summary>
    /// Status name.
    /// </summary>
    public string? Status { get; init; }

    /// <summary>
    /// Status reason.
    /// </summary>
    public UpworkProposalReason? Reason { get; init; }
}

/// <summary>
/// Proposal submitted by a freelancer or agency.
/// </summary>
public sealed record UpworkVendorProposal
{
    /// <summary>
    /// Vendor proposal identifier.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Marketplace job posting associated with this proposal.
    /// </summary>
    public UpworkMarketplaceJobPosting? MarketplaceJobPosting { get; init; }

    /// <summary>
    /// Proposal terms.
    /// </summary>
    public UpworkProposalTerms? Terms { get; init; }

    /// <summary>
    /// Legacy cover letter field.
    /// </summary>
    public string? CoverLetter { get; init; }

    /// <summary>
    /// Proposal cover letter.
    /// </summary>
    public string? ProposalCoverLetter { get; init; }

    /// <summary>
    /// Proposal project plan.
    /// </summary>
    public UpworkProposalProjectPlan? ProjectPlan { get; init; }

    /// <summary>
    /// Audit details.
    /// </summary>
    public UpworkProposalAuditDetails? AuditDetails { get; init; }

    /// <summary>
    /// Proposal status.
    /// </summary>
    public UpworkVendorProposalStatus? Status { get; init; }

    /// <summary>
    /// Proposal annotations.
    /// </summary>
    public IReadOnlyList<string>? Annotations { get; init; }
}

/// <summary>
/// Connection returned by vendor proposal search.
/// </summary>
public sealed record UpworkVendorProposalsConnection
{
    /// <summary>
    /// Total result count.
    /// </summary>
    public int? TotalCount { get; init; }

    /// <summary>
    /// Result edges.
    /// </summary>
    public IReadOnlyList<UpworkVendorProposalEdge>? Edges { get; init; }

    /// <summary>
    /// Page info.
    /// </summary>
    public UpworkPageInfo? PageInfo { get; init; }
}

/// <summary>
/// Edge returned by vendor proposal search.
/// </summary>
public sealed record UpworkVendorProposalEdge
{
    /// <summary>
    /// Result cursor.
    /// </summary>
    public string? Cursor { get; init; }

    /// <summary>
    /// Vendor proposal node.
    /// </summary>
    public UpworkVendorProposal? Node { get; init; }
}
