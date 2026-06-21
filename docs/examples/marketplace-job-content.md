# Marketplace Job Content

Fetch full marketplace job content after finding a job ID from search.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// Find a job, then fetch its content by ID.
var search = await client.SearchMarketplaceJobPostingsAsync(
    new UpworkMarketplaceJobFilter
    {
        SearchExpression = "dotnet",
        Pagination = new UpworkCursorPagination(first: 1),
    });

var firstJob = search.Edges?.FirstOrDefault()?.Node;

var job = await client.GetMarketplaceJobPostingAsync(firstJob.Id!);
```