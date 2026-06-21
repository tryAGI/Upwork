# Public Marketplace Job Search

Search public marketplace jobs with the Upwork GraphQL API.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// Search current public marketplace jobs.
var response = await client.SearchPublicMarketplaceJobPostingsAsync(
    UpworkPublicMarketplaceJobFilters.Keywords("dotnet graphql", pageSize: 10) with
    {
        JobType = UpworkJobTypes.Fixed,
        VerifiedPaymentOnly = true,
    });
```