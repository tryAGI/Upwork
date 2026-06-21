namespace Upwork;

/// <summary>
/// Common marketplace job type filter values.
/// </summary>
public static class UpworkJobTypes
{
    /// <summary>
    /// Hourly job.
    /// </summary>
    public const string Hourly = "HOURLY";

    /// <summary>
    /// Fixed-price job-posting creation value.
    /// </summary>
    public const string FixedPrice = "FIXED_PRICE";

    /// <summary>
    /// Fixed-price marketplace search value.
    /// </summary>
    public const string Fixed = "FIXED";
}

/// <summary>
/// Common job duration filter values.
/// </summary>
public static class UpworkJobDurations
{
    /// <summary>
    /// One-week duration.
    /// </summary>
    public const string Week = "WEEK";

    /// <summary>
    /// One-month duration.
    /// </summary>
    public const string Month = "MONTH";

    /// <summary>
    /// Three-month duration.
    /// </summary>
    public const string Quarter = "QUARTER";

    /// <summary>
    /// Six-month duration.
    /// </summary>
    public const string Semester = "SEMESTER";

    /// <summary>
    /// Ongoing project.
    /// </summary>
    public const string Ongoing = "ONGOING";
}

/// <summary>
/// Common marketplace workload filter values.
/// </summary>
public static class UpworkWorkloads
{
    /// <summary>
    /// Full-time workload.
    /// </summary>
    public const string FullTime = "FULL_TIME";

    /// <summary>
    /// Part-time workload.
    /// </summary>
    public const string PartTime = "PART_TIME";

    /// <summary>
    /// As-needed workload.
    /// </summary>
    public const string AsNeeded = "AS_NEEDED";
}

/// <summary>
/// Common freelancer experience-level filter values.
/// </summary>
public static class UpworkExperienceLevels
{
    /// <summary>
    /// Entry-level work.
    /// </summary>
    public const string EntryLevel = "ENTRY_LEVEL";

    /// <summary>
    /// Intermediate-level work.
    /// </summary>
    public const string Intermediate = "INTERMEDIATE";

    /// <summary>
    /// Expert-level work.
    /// </summary>
    public const string Expert = "EXPERT";
}

/// <summary>
/// Marketplace job search type values.
/// </summary>
public static class UpworkMarketplaceJobPostingSearchTypes
{
    /// <summary>
    /// Standard user job search.
    /// </summary>
    public const string UserJobsSearch = "USER_JOBS_SEARCH";

    /// <summary>
    /// Rehydration search.
    /// </summary>
    public const string Rehydration = "REHYDRATION";

    /// <summary>
    /// User saved-search execution.
    /// </summary>
    public const string UserSavedSearch = "USER_SAVED_SEARCH";
}

/// <summary>
/// Marketplace search sort fields.
/// </summary>
public static class UpworkMarketplaceJobPostingSearchSortFields
{
    /// <summary>
    /// Sort by recency.
    /// </summary>
    public const string Recency = "RECENCY";

    /// <summary>
    /// Sort by relevance.
    /// </summary>
    public const string Relevance = "RELEVANCE";

    /// <summary>
    /// Sort by client rating.
    /// </summary>
    public const string ClientRating = "CLIENT_RATING";

    /// <summary>
    /// Sort by client spend.
    /// </summary>
    public const string ClientSpend = "CLIENT_SPEND";
}

/// <summary>
/// Upwork sort direction values.
/// </summary>
public static class UpworkSortOrders
{
    /// <summary>
    /// Ascending sort order.
    /// </summary>
    public const string Ascending = "ASC";

    /// <summary>
    /// Descending sort order.
    /// </summary>
    public const string Descending = "DESC";
}

/// <summary>
/// Vendor proposal sort fields.
/// </summary>
public static class UpworkVendorProposalSortFields
{
    /// <summary>
    /// Sort by proposal creation timestamp.
    /// </summary>
    public const string CreatedDateTime = "CREATEDDATETIME";

    /// <summary>
    /// Sort by proposal modification timestamp.
    /// </summary>
    public const string ModifiedDateTime = "MODIFIEDDATETIME";
}

/// <summary>
/// Vendor proposal status filter values.
/// </summary>
public static class UpworkProposalStatuses
{
    /// <summary>
    /// Proposal was submitted and validated.
    /// </summary>
    public const string Accepted = "Accepted";

    /// <summary>
    /// Proposal was declined by the client.
    /// </summary>
    public const string Declined = "Declined";

    /// <summary>
    /// Proposal was withdrawn by the freelancer.
    /// </summary>
    public const string Withdrawn = "Withdrawn";

    /// <summary>
    /// Offer was sent or accepted through an invite flow.
    /// </summary>
    public const string Offered = "Offered";

    /// <summary>
    /// Proposal was activated after an accepted invite.
    /// </summary>
    public const string Activated = "Activated";

    /// <summary>
    /// Proposal was archived.
    /// </summary>
    public const string Archived = "Archived";

    /// <summary>
    /// Freelancer was hired.
    /// </summary>
    public const string Hired = "Hired";

    /// <summary>
    /// Proposal is pending validation.
    /// </summary>
    public const string Pending = "Pending";
}

/// <summary>
/// Proposal reason type values.
/// </summary>
public static class UpworkReasonTypes
{
    /// <summary>
    /// Reasons for proposal decline flows.
    /// </summary>
    public const string ProposalDecline = "PROPOSAL_DECLINE";
}

/// <summary>
/// Vendor proposal annotation values.
/// </summary>
public static class UpworkVendorProposalAnnotations
{
    /// <summary>
    /// Hidden proposal annotation.
    /// </summary>
    public const string Hidden = "Hidden";

    /// <summary>
    /// On-hold proposal annotation.
    /// </summary>
    public const string OnHold = "OnHold";
}
