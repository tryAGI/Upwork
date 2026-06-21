using System.Text.Json;

namespace Upwork;

/// <summary>
/// GraphQL error returned by Upwork.
/// </summary>
public sealed record UpworkGraphQLError
{
    /// <summary>
    /// Error message.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Error locations.
    /// </summary>
    public IReadOnlyList<UpworkGraphQLErrorLocation>? Locations { get; init; }

    /// <summary>
    /// Error path.
    /// </summary>
    public IReadOnlyList<JsonElement>? Path { get; init; }

    /// <summary>
    /// Provider-specific error extensions.
    /// </summary>
    public JsonElement? Extensions { get; init; }
}

/// <summary>
/// Location of a GraphQL error.
/// </summary>
public sealed record UpworkGraphQLErrorLocation
{
    /// <summary>
    /// Source line.
    /// </summary>
    public int? Line { get; init; }

    /// <summary>
    /// Source column.
    /// </summary>
    public int? Column { get; init; }
}
