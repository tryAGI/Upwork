namespace Upwork;

/// <summary>
/// Company selector response for discovering available organization contexts.
/// </summary>
public sealed record UpworkCompanySelector
{
    /// <summary>
    /// Organizations available to the current user.
    /// </summary>
    public IReadOnlyList<UpworkCompanySelectorItem>? Items { get; init; }
}

/// <summary>
/// Organization context available in the company selector.
/// </summary>
public sealed record UpworkCompanySelectorItem
{
    /// <summary>
    /// Selector item title.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Selector item thumbnail URL.
    /// </summary>
    public Uri? PhotoUrl { get; init; }

    /// <summary>
    /// Organization UID.
    /// </summary>
    public string? OrganizationId { get; init; }

    /// <summary>
    /// Organization RID.
    /// </summary>
    public string? OrganizationRid { get; init; }

    /// <summary>
    /// Organization type.
    /// </summary>
    public string? OrganizationType { get; init; }

    /// <summary>
    /// Legacy organization type.
    /// </summary>
    public string? OrganizationLegacyType { get; init; }

    /// <summary>
    /// Enterprise organization type.
    /// </summary>
    public string? OrganizationEnterpriseType { get; init; }

    /// <summary>
    /// Whether this is a legacy enterprise organization.
    /// </summary>
    public bool? LegacyEnterpriseOrganization { get; init; }

    /// <summary>
    /// Human-readable organization type title.
    /// </summary>
    public string? TypeTitle { get; init; }
}
