# Upwork

[![Nuget package](https://img.shields.io/nuget/vpre/Upwork)](https://www.nuget.org/packages/Upwork/)
[![dotnet](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tryAGI/Upwork/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/tryAGI/Upwork)](https://github.com/tryAGI/Upwork/blob/main/LICENSE)

C# SDK for the official Upwork GraphQL API, focused on OAuth2 authorization and read-only marketplace job search.

## Features

- OAuth2 Authorization Code Grant and Refresh Token Grant
- Authorization URL builder for `https://www.upwork.com/ab/account-security/oauth2/authorize`
- Token exchange against `https://www.upwork.com/api/v3/oauth2/token`
- GraphQL client for `https://api.upwork.com/graphql` using `Authorization: Bearer`
- Optional `X-Upwork-API-TenantId` header support
- `companySelector` helper for organization context discovery
- `marketplaceJobPostingsSearch` wrapper with cursor pagination and sort attributes
- `marketplaceJobPosting` wrapper for expanded job details
- `marketplaceJobPostingsContents` wrapper for job detail/content lookup
- Typed DTOs for marketplace job IDs, title, description, published date, ciphertext, annotations, skills, budget/hourly/client/proposal fields when returned by Upwork
- GraphQL missing-scope exception handling and HTTP 429 rate-limit hooks
- Source-generated JSON serialization for trimming and NativeAOT compatibility

No mutation helpers are exposed for this OAuth/job-search workflow.

## Required Scopes

Select these scopes in the Upwork app/key settings for this read-only job-search use case:

- `Common Entities - Read-Only Access`
- `Read marketplace Job Postings`

## OAuth

Build an authorization URL from environment variables:

```csharp
using Upwork;

var clientId =
    Environment.GetEnvironmentVariable("UPWORK_CLIENT_ID") is { Length: > 0 } clientIdValue
        ? clientIdValue
        : throw new InvalidOperationException("UPWORK_CLIENT_ID is required.");
var clientSecret =
    Environment.GetEnvironmentVariable("UPWORK_CLIENT_SECRET") is { Length: > 0 } clientSecretValue
        ? clientSecretValue
        : throw new InvalidOperationException("UPWORK_CLIENT_SECRET is required.");
var redirectUri =
    Environment.GetEnvironmentVariable("UPWORK_REDIRECT_URI") is { Length: > 0 } redirectValue
        ? new Uri(redirectValue)
        : throw new InvalidOperationException("UPWORK_REDIRECT_URI is required.");

var oauthConfig = new UpworkOAuthConfig(clientId, clientSecret, redirectUri);
using var oauth = new UpworkOAuthClient(oauthConfig);

var authorizationUrl = oauth.CreateAuthorizationUri(
    state: Environment.GetEnvironmentVariable("UPWORK_OAUTH_STATE"));
```

Exchange an authorization code:

```csharp
var authorizationCode =
    Environment.GetEnvironmentVariable("UPWORK_AUTHORIZATION_CODE") is { Length: > 0 } codeValue
        ? codeValue
        : throw new InvalidOperationException("UPWORK_AUTHORIZATION_CODE is required.");

var token = await oauth.ExchangeAuthorizationCodeAsync(authorizationCode);
```

Refresh an access token:

```csharp
var refreshToken =
    Environment.GetEnvironmentVariable("UPWORK_REFRESH_TOKEN") is { Length: > 0 } refreshValue
        ? refreshValue
        : throw new InvalidOperationException("UPWORK_REFRESH_TOKEN is required.");

var refreshedToken = await oauth.RefreshTokenAsync(refreshToken);
```

The SDK returns tokens but does not persist them. Store `accessToken`, `refreshToken`, and expiry metadata in your application.

## Job Search

Use a bearer token from your own token store:

```csharp
using Upwork;

var accessToken =
    Environment.GetEnvironmentVariable("UPWORK_ACCESS_TOKEN") is { Length: > 0 } accessTokenValue
        ? accessTokenValue
        : throw new InvalidOperationException("UPWORK_ACCESS_TOKEN is required.");
var tenantId = Environment.GetEnvironmentVariable("UPWORK_TENANT_ID");

using var client = new UpworkClient(
    new UpworkClientOptions
    {
        AccessToken = accessToken,
        TenantId = string.IsNullOrWhiteSpace(tenantId) ? null : tenantId,
    });

var selector = await client.GetCompanySelectorAsync();

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

var firstJobId = jobs.Edges?
    .Select(edge => edge.Node?.Id)
    .FirstOrDefault(id => id is { Length: > 0 });

if (firstJobId is { Length: > 0 })
{
    var contents = await client.GetMarketplaceJobPostingsContentsAsync([firstJobId]);
}
```

Read expanded job fields from search results or direct detail lookup:

```csharp
var search = await client.SearchMarketplaceJobPostingsAsync(
    UpworkMarketplaceJobFilters.Keywords("csharp backend", first: 5));

foreach (var edge in search.Edges ?? [])
{
    var result = edge.Node;
    var posting = result?.Job;
    var hourlyTerms = posting?.ContractTerms?.HourlyContractTerms;
    var fixedPriceTerms = posting?.ContractTerms?.FixedPriceContractTerms;
    var skills = posting?.Classification?.Skills?
        .Select(skill => skill.PreferredLabel)
        .Where(skill => skill is { Length: > 0 });

    Console.WriteLine(result?.Title ?? posting?.Content?.Title);
    Console.WriteLine($"Hourly: {hourlyTerms?.HourlyBudgetMin}-{hourlyTerms?.HourlyBudgetMax}");
    Console.WriteLine($"Fixed: {fixedPriceTerms?.Amount?.DisplayValue}");
    Console.WriteLine($"Client: {posting?.ClientCompanyPublic?.City}, {posting?.ClientCompanyPublic?.State}");
    Console.WriteLine($"Activity: {posting?.ActivityStat?.JobActivity?.TotalHired} hires");
    Console.WriteLine($"Skills: {string.Join(", ", skills ?? [])}");
}

var detailJobId = search.Edges?
    .Select(edge => edge.Node?.Id)
    .FirstOrDefault(id => id is { Length: > 0 });

if (detailJobId is { Length: > 0 })
{
    var posting = await client.GetMarketplaceJobPostingAsync(detailJobId);
    var questions = posting?.ContractorSelection?.ProposalRequirement?.ScreeningQuestions;

    foreach (var question in questions ?? [])
    {
        Console.WriteLine(question.Question);
    }
}
```

For backend-owned token storage, implement `IUpworkAccessTokenProvider` and pass it through `UpworkClientOptions.AccessTokenProvider`.

<!-- EXAMPLES:START -->

<!-- EXAMPLES:END -->

## Rate Limits

HTTP 429 responses throw `UpworkRateLimitException` with `RetryAfter` when Upwork sends that header. To retry, provide an `IUpworkRateLimitHandler` and set `MaxRateLimitRetries`.

## Migration Notes

- Replace direct REST/OAuth code with `UpworkOAuthConfig` and `UpworkOAuthClient`.
- Store tokens in your backend database or secret store; the SDK does not persist secrets.
- Refresh tokens with `RefreshTokenAsync` before access-token expiry and update stored tokens atomically.
- Use `GetCompanySelectorAsync` to discover organization context, then set `TenantId` when your workflow requires `X-Upwork-API-TenantId`.
- Use `SearchMarketplaceJobPostingsAsync`; do not use deprecated `marketplaceJobPostings`.
- Use `GetMarketplaceJobPostingAsync` for expanded job details, or `GetMarketplaceJobPostingsContentsAsync` for bulk content lookup.
- Handle `UpworkMissingScopeException` by asking the user/admin to grant `Common Entities - Read-Only Access` and `Read marketplace Job Postings`.
- Handle `UpworkRateLimitException` or configure `IUpworkRateLimitHandler` for retry/backoff.
- Do not log or persist authorization codes, access tokens, refresh tokens, client secrets, or full `Authorization` headers.

## Support

Priority place for bugs: https://github.com/tryAGI/Upwork/issues  
Priority place for ideas and general questions: https://github.com/tryAGI/Upwork/discussions
