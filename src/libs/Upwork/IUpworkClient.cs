using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace Upwork;

/// <summary>
/// Client abstraction for the Upwork GraphQL API.
/// </summary>
public interface IUpworkClient
{
    /// <summary>
    /// Executes a GraphQL request and returns the raw GraphQL JSON envelope.
    /// </summary>
    Task<JsonDocument> ExecuteRawAsync(
        UpworkGraphQLRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a GraphQL query and deserializes the <c>data</c> node.
    /// </summary>
    Task<TData?> ExecuteAsync<TData>(
        string query,
        JsonTypeInfo<TData> dataTypeInfo,
        JsonObject? variables = null,
        string? operationName = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches public marketplace job postings.
    /// </summary>
    Task<UpworkPublicMarketplaceJobPostingsSearch> SearchPublicMarketplaceJobPostingsAsync(
        UpworkPublicMarketplaceJobFilter filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches authenticated marketplace job postings.
    /// </summary>
    Task<UpworkMarketplaceJobPostingSearchConnection> SearchMarketplaceJobPostingsAsync(
        UpworkMarketplaceJobFilter? filter = null,
        string searchType = UpworkMarketplaceJobPostingSearchTypes.UserJobsSearch,
        IReadOnlyList<UpworkMarketplaceJobPostingSearchSort>? sortAttributes = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches a marketplace job posting aggregate by ID.
    /// </summary>
    Task<UpworkMarketplaceJobPosting?> GetMarketplaceJobPostingAsync(
        string id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches marketplace job posting contents by ID.
    /// </summary>
    Task<IReadOnlyList<UpworkMarketplaceJobPostingContentRecord>> GetMarketplaceJobPostingsContentsAsync(
        IReadOnlyList<string> ids,
        CancellationToken cancellationToken = default);
}
