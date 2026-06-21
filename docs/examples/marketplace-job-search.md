# Authenticated Marketplace Job Search

Search marketplace jobs from an authenticated freelancer workflow.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// Search marketplace jobs with cursor pagination and recency sorting.
var response = await client.SearchMarketplaceJobPostingsAsync(
    UpworkMarketplaceJobFilters.Keywords("dotnet graphql", first: 10) with
    {
        VerifiedPaymentOnly = true,
    },
    sortAttributes:
    [
        new UpworkMarketplaceJobPostingSearchSort(
            UpworkMarketplaceJobPostingSearchSortFields.Recency),
    ]);
```