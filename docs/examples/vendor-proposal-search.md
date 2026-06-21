# Vendor Proposal Search

List vendor proposals for a freelancer or agency account.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// List accepted vendor proposals with cursor pagination.
var response = await client.SearchVendorProposalsAsync(
    UpworkVendorProposalFilters.ByStatus(UpworkProposalStatuses.Accepted),
    pagination: new UpworkCursorPagination(first: 10));
```