using System.Text.Json.Nodes;

namespace Upwork;

/// <summary>
/// GraphQL request envelope sent to Upwork.
/// </summary>
public sealed record UpworkGraphQLRequest
{
    /// <summary>
    /// GraphQL query or mutation text.
    /// </summary>
    public required string Query { get; init; }

    /// <summary>
    /// Optional operation name.
    /// </summary>
    public string? OperationName { get; init; }

    /// <summary>
    /// Optional GraphQL variables object.
    /// </summary>
    public JsonObject? Variables { get; init; }
}
