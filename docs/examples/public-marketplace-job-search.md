# Public Marketplace Job Search

Search public marketplace jobs with the Upwork GraphQL API.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork OAuth access token.

```csharp
using var client = new UpworkClient(accessToken);

// Search current public marketplace jobs.
var response = await client.SearchPublicMarketplaceJobPostingsAsync(
    new UpworkPublicMarketplaceJobFilter
    {
        SearchExpression = "dotnet graphql",
        JobType = UpworkJobTypes.Fixed,
        PaymentVerified = true,
        Pagination = new UpworkOffsetPagination(pageOffset: 0, pageSize: 10),
    });
```
