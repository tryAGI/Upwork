# Upwork

[![Nuget package](https://img.shields.io/nuget/vpre/Upwork)](https://www.nuget.org/packages/Upwork/)
[![dotnet](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/tryAGI/Upwork)](https://github.com/tryAGI/Upwork/blob/main/LICENSE)

C# SDK for the Upwork GraphQL API. The first supported surface is marketplace job search for freelancer workflows.

## Features

- Public marketplace job search
- Authenticated marketplace job search with cursor paging, sort attributes, and typed filter builders
- Marketplace job content lookup by job ID
- Proposal metadata lookup and vendor proposal list/detail workflows for freelancers and agencies
- Constants for marketplace job types, durations, workloads, experience levels, proposal statuses, and proposal sort fields
- OAuth token exchange helpers for authorization-code, refresh-token, and enterprise client-credentials flows
- Raw GraphQL execution escape hatch for fields that are not yet wrapped
- Source-generated JSON serialization for trimming and NativeAOT compatibility

## Usage

```csharp
using Upwork;

using var client = new UpworkClient(accessToken);

var jobs = await client.SearchMarketplaceJobPostingsAsync(
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

For public marketplace search, use `SearchPublicMarketplaceJobPostingsAsync` with `UpworkPublicMarketplaceJobFilter`.
For freelancer proposal workflows, use `GetProposalMetadataAsync`, `SearchVendorProposalsAsync`, and `GetVendorProposalAsync`.

<!-- EXAMPLES:START -->
### Public Marketplace Job Search
Search public marketplace jobs with the Upwork GraphQL API.

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

### Authenticated Marketplace Job Search
Search marketplace jobs from an authenticated freelancer workflow.

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

### Marketplace Job Content
Fetch full marketplace job content after finding a job ID from search.

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

### Proposal Metadata
Read proposal metadata for freelancer proposal workflows.

```csharp
using var client = new UpworkClient(accessToken);

// Fetch proposal reference metadata, including engagement durations and decline reasons.
var metadata = await client.GetProposalMetadataAsync(UpworkReasonTypes.ProposalDecline);
```

### Vendor Proposal Search
List vendor proposals for a freelancer or agency account.

```csharp
using var client = new UpworkClient(accessToken);

// List accepted vendor proposals with cursor pagination.
var response = await client.SearchVendorProposalsAsync(
    UpworkVendorProposalFilters.ByStatus(UpworkProposalStatuses.Accepted),
    pagination: new UpworkCursorPagination(first: 10));
```

### Vendor Proposal Details
Read a vendor proposal after finding it in the freelancer proposal list.

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
<!-- EXAMPLES:END -->

## Support

Priority place for bugs: https://github.com/tryAGI/Upwork/issues  
Priority place for ideas and general questions: https://github.com/tryAGI/Upwork/discussions
