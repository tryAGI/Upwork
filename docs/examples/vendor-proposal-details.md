# Vendor Proposal Details

Read a vendor proposal after finding it in the freelancer proposal list.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// Find one accepted proposal and then load its detail record.
var response = await client.SearchVendorProposalsAsync(
    UpworkVendorProposalFilters.ByStatus(UpworkProposalStatuses.Accepted),
    pagination: new UpworkCursorPagination(first: 1));

var proposalId = response.Edges?
    .Select(edge => edge.Node?.Id)
    .FirstOrDefault(id => id is { Length: > 0 });

if (proposalId is not { Length: > 0 })
{
    throw new AssertInconclusiveException("No accepted vendor proposals were returned for this account.");
}

var proposal = await client.GetVendorProposalAsync(proposalId);
```