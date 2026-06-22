# Proposal Mutations

Upwork's public GraphQL documentation currently exposes proposal attachment helper mutations, but does not document a freelancer/vendor proposal submit or update mutation.

Use `CreateJobApplicationProposalUploadLinkAsync` to create a direct upload target for a job-application proposal attachment:

```csharp
using var client = new UpworkClient(accessToken);

var fileInfo = await client.CreateJobApplicationProposalUploadLinkAsync(
    new UpworkCreateDirectUploadLinkInput("proposal.pdf")
    {
        ContentType = "application/pdf",
        MaxFileSize = 1_048_576,
        SslEnabled = true,
    });
```

After uploading the file to `fileInfo.UploadUrl` with the returned `FormKeyValues`, confirm uploaded file IDs:

```csharp
var confirmed = await client.ConfirmFilesAsync([fileInfo.Id!]);
```

For undocumented submit/update operations, use `ExecuteRawAsync` or `ExecuteAsync` only if Upwork provides a supported GraphQL document for your account.
