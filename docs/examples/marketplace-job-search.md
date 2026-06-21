# Authenticated Marketplace Job Search

Search marketplace jobs from an authenticated freelancer workflow.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork OAuth access token.

```csharp
using var client = new UpworkClient(accessToken);

// Search marketplace jobs with cursor pagination and recency sorting.
var response = await client.SearchMarketplaceJobPostingsAsync(
    new UpworkMarketplaceJobFilter
    {
        SearchExpression = "dotnet graphql",
        VerifiedPaymentOnly = true,
        IncludeApplied = false,
        Pagination = new UpworkCursorPagination(first: 10),
    },
    sortAttributes:
    [
        new UpworkMarketplaceJobPostingSearchSort(
            UpworkMarketplaceJobPostingSearchSortFields.Recency),
    ]);
```
