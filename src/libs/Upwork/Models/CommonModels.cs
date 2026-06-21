using System.Text.Json;
using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// Money scalar object returned by Upwork.
/// </summary>
public sealed record UpworkMoney
{
    /// <summary>
    /// Numeric amount value.
    /// </summary>
    public decimal? RawValue { get; init; }

    /// <summary>
    /// Currency code.
    /// </summary>
    public string? Currency { get; init; }

    /// <summary>
    /// Human-readable amount value.
    /// </summary>
    public string? DisplayValue { get; init; }
}

/// <summary>
/// Inclusive numeric range used by Upwork GraphQL filters.
/// </summary>
public sealed record UpworkIntRange(
    [property: JsonPropertyName("rangeStart")] int? RangeStart = null,
    [property: JsonPropertyName("rangeEnd")] int? RangeEnd = null);

/// <summary>
/// Inclusive decimal range used by Upwork GraphQL filters.
/// </summary>
public sealed record UpworkFloatRange(
    [property: JsonPropertyName("rangeStart")] decimal? RangeStart = null,
    [property: JsonPropertyName("rangeEnd")] decimal? RangeEnd = null);

/// <summary>
/// Geographic area filter by coordinates.
/// </summary>
public sealed record UpworkAreaFilter(
    [property: JsonPropertyName("latitude")] decimal Latitude,
    [property: JsonPropertyName("longitude")] decimal Longitude,
    [property: JsonPropertyName("radius")] decimal Radius);

/// <summary>
/// Offset paging input used by public marketplace search.
/// </summary>
public sealed record UpworkOffsetPagination
{
    /// <summary>
    /// Creates offset pagination input.
    /// </summary>
    public UpworkOffsetPagination(int pageOffset = 0, int pageSize = 10)
    {
        PageOffset = pageOffset;
        PageSize = pageSize;
    }

    /// <summary>
    /// Zero-based page offset.
    /// </summary>
    [JsonPropertyName("pageOffset")]
    public int PageOffset { get; init; }

    /// <summary>
    /// Page size.
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }
}

/// <summary>
/// Cursor paging input used by authenticated marketplace search.
/// </summary>
public sealed record UpworkCursorPagination
{
    /// <summary>
    /// Creates cursor pagination input.
    /// </summary>
    public UpworkCursorPagination(string? after = null, int? first = null)
    {
        After = after;
        First = first;
    }

    /// <summary>
    /// Cursor after which results should be returned.
    /// </summary>
    [JsonPropertyName("after")]
    public string? After { get; init; }

    /// <summary>
    /// Maximum number of results to return.
    /// </summary>
    [JsonPropertyName("first")]
    public int? First { get; init; }
}

/// <summary>
/// Page info returned by Upwork connection-style queries.
/// </summary>
public sealed record UpworkPageInfo
{
    /// <summary>
    /// Cursor for the next page.
    /// </summary>
    public string? EndCursor { get; init; }

    /// <summary>
    /// Whether another page exists.
    /// </summary>
    public bool? HasNextPage { get; init; }
}

/// <summary>
/// Skill result returned by Upwork search.
/// </summary>
public sealed record UpworkSkill
{
    /// <summary>
    /// Skill ID, when available.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Canonical skill name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Display skill name.
    /// </summary>
    public string? PrettyName { get; init; }

    /// <summary>
    /// Whether this skill matched the search expression.
    /// </summary>
    public bool? Highlighted { get; init; }
}

/// <summary>
/// Ontology node returned by Upwork.
/// </summary>
public sealed record UpworkOntologyNode
{
    /// <summary>
    /// Node ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Preferred display label.
    /// </summary>
    public string? PrefLabel { get; init; }

    /// <summary>
    /// Whether this node matched the search expression.
    /// </summary>
    public bool? Highlighted { get; init; }

    /// <summary>
    /// Free-text value, when the node was created from user text.
    /// </summary>
    public string? FreeText { get; init; }
}

/// <summary>
/// Occupation hierarchy returned by Upwork search.
/// </summary>
public sealed record UpworkOccupation
{
    /// <summary>
    /// Top-level category.
    /// </summary>
    public UpworkOntologyNode? Category { get; init; }

    /// <summary>
    /// Subcategories.
    /// </summary>
    public IReadOnlyList<UpworkOntologyNode>? SubCategories { get; init; }

    /// <summary>
    /// Occupation service.
    /// </summary>
    public UpworkOntologyNode? OccupationService { get; init; }
}

/// <summary>
/// Engagement duration returned by Upwork.
/// </summary>
public sealed record UpworkEngagementDuration
{
    /// <summary>
    /// Duration ID.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Duration label.
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Duration in weeks, when returned by the endpoint.
    /// </summary>
    public int? Weeks { get; init; }
}

/// <summary>
/// Client location returned by authenticated search.
/// </summary>
public sealed record UpworkClientLocation
{
    /// <summary>
    /// City.
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Country.
    /// </summary>
    public string? Country { get; init; }

    /// <summary>
    /// Timezone name.
    /// </summary>
    public string? Timezone { get; init; }

    /// <summary>
    /// State or region.
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// Offset to UTC in minutes.
    /// </summary>
    public int? OffsetToUTC { get; init; }
}

/// <summary>
/// Feed job location returned by public search.
/// </summary>
public sealed record UpworkFeedJobLocation
{
    /// <summary>
    /// Country.
    /// </summary>
    public string? Country { get; init; }

    /// <summary>
    /// City.
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// State or region.
    /// </summary>
    public string? State { get; init; }

    /// <summary>
    /// Country timezone.
    /// </summary>
    public string? CountryTimezone { get; init; }

    /// <summary>
    /// World region.
    /// </summary>
    public string? WorldRegion { get; init; }
}

/// <summary>
/// Free-form key-value field returned on marketplace job content.
/// </summary>
public sealed record UpworkCustomField
{
    /// <summary>
    /// Field key.
    /// </summary>
    public string? Key { get; init; }

    /// <summary>
    /// Field value.
    /// </summary>
    public JsonElement? Value { get; init; }
}
