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
    /// Fixed-price job in authenticated marketplace search.
    /// </summary>
    public const string FixedPrice = "FIXED_PRICE";

    /// <summary>
    /// Fixed-price job in public marketplace search.
    /// </summary>
    public const string Fixed = "FIXED";
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
