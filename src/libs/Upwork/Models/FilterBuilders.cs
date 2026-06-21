namespace Upwork;

/// <summary>
/// Convenience factories for public marketplace job search filters.
/// </summary>
public static class UpworkPublicMarketplaceJobFilters
{
    /// <summary>
    /// Creates a public marketplace keyword search filter.
    /// </summary>
    public static UpworkPublicMarketplaceJobFilter Keywords(
        string searchExpression,
        int pageSize = 10,
        int pageOffset = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchExpression);

        return new UpworkPublicMarketplaceJobFilter
        {
            SearchExpression = searchExpression,
            Pagination = new UpworkOffsetPagination(pageOffset, pageSize),
        };
    }

    /// <summary>
    /// Creates a public marketplace occupation search filter.
    /// </summary>
    public static UpworkPublicMarketplaceJobFilter Occupations(
        IReadOnlyList<string> occupationIds,
        int pageSize = 10,
        int pageOffset = 0)
    {
        RequireValues(occupationIds, nameof(occupationIds));

        return new UpworkPublicMarketplaceJobFilter
        {
            OccupationIdsAny = occupationIds,
            Pagination = new UpworkOffsetPagination(pageOffset, pageSize),
        };
    }

    /// <summary>
    /// Creates a public marketplace duration search filter.
    /// </summary>
    public static UpworkPublicMarketplaceJobFilter Durations(
        IReadOnlyList<string> durations,
        int pageSize = 10,
        int pageOffset = 0)
    {
        RequireValues(durations, nameof(durations));

        return new UpworkPublicMarketplaceJobFilter
        {
            DurationAny = durations,
            Pagination = new UpworkOffsetPagination(pageOffset, pageSize),
        };
    }

    private static void RequireValues(IReadOnlyList<string> values, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(values, parameterName);
        if (values.Count == 0)
        {
            throw new ArgumentException("At least one value is required.", parameterName);
        }

        foreach (var value in values)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        }
    }
}

/// <summary>
/// Convenience factories for authenticated marketplace job search filters.
/// </summary>
public static class UpworkMarketplaceJobFilters
{
    /// <summary>
    /// Creates an authenticated marketplace keyword search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Keywords(string searchExpression, int first = 10)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(searchExpression);

        return new UpworkMarketplaceJobFilter
        {
            SearchExpression = searchExpression,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    /// <summary>
    /// Creates an authenticated marketplace category search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Categories(IReadOnlyList<string> categoryIds, int first = 10)
    {
        RequireValues(categoryIds, nameof(categoryIds));

        return new UpworkMarketplaceJobFilter
        {
            CategoryIdsAny = categoryIds,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    /// <summary>
    /// Creates an authenticated marketplace subcategory search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Subcategories(IReadOnlyList<string> subcategoryIds, int first = 10)
    {
        RequireValues(subcategoryIds, nameof(subcategoryIds));

        return new UpworkMarketplaceJobFilter
        {
            SubcategoryIdsAny = subcategoryIds,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    /// <summary>
    /// Creates an authenticated marketplace occupation search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Occupations(IReadOnlyList<string> occupationIds, int first = 10)
    {
        RequireValues(occupationIds, nameof(occupationIds));

        return new UpworkMarketplaceJobFilter
        {
            OccupationIdsAny = occupationIds,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    /// <summary>
    /// Creates an authenticated marketplace skill search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Skills(IReadOnlyList<string> ontologySkillIds, int first = 10)
    {
        RequireValues(ontologySkillIds, nameof(ontologySkillIds));

        return new UpworkMarketplaceJobFilter
        {
            OntologySkillIdsAll = ontologySkillIds,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    /// <summary>
    /// Creates an authenticated marketplace duration search filter.
    /// </summary>
    public static UpworkMarketplaceJobFilter Durations(IReadOnlyList<string> durations, int first = 10)
    {
        RequireValues(durations, nameof(durations));

        return new UpworkMarketplaceJobFilter
        {
            DurationAny = durations,
            Pagination = new UpworkCursorPagination(first: first),
        };
    }

    private static void RequireValues(IReadOnlyList<string> values, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(values, parameterName);
        if (values.Count == 0)
        {
            throw new ArgumentException("At least one value is required.", parameterName);
        }

        foreach (var value in values)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        }
    }
}

/// <summary>
/// Convenience factories for vendor proposal filters.
/// </summary>
public static class UpworkVendorProposalFilters
{
    /// <summary>
    /// Creates a vendor proposal filter by status.
    /// </summary>
    public static UpworkVendorProposalFilter ByStatus(string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        return new UpworkVendorProposalFilter
        {
            Status = status,
        };
    }

    /// <summary>
    /// Creates a vendor proposal filter by job posting ID and status.
    /// </summary>
    public static UpworkVendorProposalFilter ForJobPosting(
        string jobPostingId,
        string status = UpworkProposalStatuses.Accepted)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jobPostingId);
        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        return new UpworkVendorProposalFilter
        {
            Status = status,
            JobPostingIdsAny = [jobPostingId],
        };
    }

    /// <summary>
    /// Creates a vendor proposal filter by annotation and status.
    /// </summary>
    public static UpworkVendorProposalFilter WithAnnotation(
        string annotation,
        string status = UpworkProposalStatuses.Accepted)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(annotation);
        ArgumentException.ThrowIfNullOrWhiteSpace(status);

        return new UpworkVendorProposalFilter
        {
            Status = status,
            Annotation = annotation,
        };
    }
}
