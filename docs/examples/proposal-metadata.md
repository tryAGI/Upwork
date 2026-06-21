# Proposal Metadata

Read proposal metadata for freelancer proposal workflows.

This example assumes `using Upwork;` is in scope and `accessToken` contains your Upwork access token.

```csharp
using var client = new UpworkClient(accessToken);

// Fetch proposal reference metadata, including engagement durations and decline reasons.
var metadata = await client.GetProposalMetadataAsync(UpworkReasonTypes.ProposalDecline);
```