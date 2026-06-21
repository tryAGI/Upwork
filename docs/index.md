# Upwork

[![Nuget package](https://img.shields.io/nuget/vpre/Upwork)](https://www.nuget.org/packages/Upwork/)
[![dotnet](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/tryAGI/Upwork)](https://github.com/tryAGI/Upwork/blob/main/LICENSE)

C# SDK for the Upwork GraphQL API. The first supported surface is marketplace job search for freelancer workflows.

## Features

- Public marketplace job search
- Authenticated marketplace job search with cursor paging and sort attributes
- Marketplace job content lookup by job ID
- OAuth token exchange helpers for authorization-code, refresh-token, and enterprise client-credentials flows
- Raw GraphQL execution escape hatch for fields that are not yet wrapped
- Source-generated JSON serialization for trimming and NativeAOT compatibility

## Usage

```csharp
using Upwork;

using var client = new UpworkClient(accessToken);

var jobs = await client.SearchMarketplaceJobPostingsAsync(
    new UpworkMarketplaceJobFilter
    {
        SearchExpression = "dotnet graphql",
        VerifiedPaymentOnly = true,
        Pagination = new UpworkCursorPagination(first: 10),
    },
    sortAttributes:
    [
        new UpworkMarketplaceJobPostingSearchSort(
            UpworkMarketplaceJobPostingSearchSortFields.Recency),
    ]);
```

For public marketplace search, use `SearchPublicMarketplaceJobPostingsAsync` with `UpworkPublicMarketplaceJobFilter`.

<!-- EXAMPLES:START -->
### Public Marketplace Job Search
Search public marketplace jobs with the Upwork GraphQL API.

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

### Authenticated Marketplace Job Search
Search marketplace jobs from an authenticated freelancer workflow.

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
<!-- EXAMPLES:END -->

## Support

Priority place for bugs: https://github.com/tryAGI/Upwork/issues  
Priority place for ideas and general questions: https://github.com/tryAGI/Upwork/discussions
