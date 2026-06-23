using System.Net;
using System.Net.Http.Headers;

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task GraphQLClient_SendsBearerAndTenantHeaders()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessToken = "Bearer access-token",
                TenantId = "tenant-id",
            });
        await client.GetCompanySelectorAsync();

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Authorization.Should().Be("Bearer access-token");
        handler.Requests[0].TenantId.Should().Be("tenant-id");
    }

    [TestMethod]
    public async Task GraphQLClient_UsesAccessTokenProvider()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessTokenProvider = new StaticAccessTokenProvider("provided-token"),
            });
        await client.GetCompanySelectorAsync();

        handler.Requests.Should().ContainSingle();
        handler.Requests[0].Authorization.Should().Be("Bearer provided-token");
    }

    [TestMethod]
    public async Task GetCompanySelectorAsync_DeserializesOrganizationContexts()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "companySelector": {
                  "items": [
                    {
                      "title": "Agency",
                      "photoUrl": "https://example.test/logo.png",
                      "organizationId": "org-uid",
                      "organizationRid": "org-rid",
                      "organizationType": "Business",
                      "organizationLegacyType": "Vendor",
                      "organizationEnterpriseType": "Standard",
                      "legacyEnterpriseOrganization": false,
                      "typeTitle": "Agency"
                    }
                  ]
                }
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var selector = await client.GetCompanySelectorAsync();

        var item = selector.Items.Should().ContainSingle().Subject;
        item.Title.Should().Be("Agency");
        item.PhotoUrl.Should().Be(new Uri("https://example.test/logo.png"));
        item.OrganizationId.Should().Be("org-uid");
        item.OrganizationLegacyType.Should().Be("Vendor");
        handler.Requests[0].Content.Should().Contain("companySelector");
    }

    [TestMethod]
    public async Task SearchMarketplaceJobPostingsAsync_UsesCurrentSearchQuery()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "marketplaceJobPostingsSearch": {
                  "totalCount": 1,
                  "edges": [
                    {
                      "cursor": "cursor-1",
                      "node": {
                        "id": "job-1",
                        "title": "Build a GraphQL SDK",
                        "description": "Use C#",
                        "ciphertext": "cipher",
                        "publishedDateTime": "2026-01-02T03:04:05Z",
                        "totalApplicants": 3,
                        "skills": [{"id": "skill-1", "name": "csharp", "prettyName": "C#", "highlighted": true}],
                        "client": {
                          "totalHires": 5,
                          "totalFeedback": 4.9,
                          "verificationStatus": "VERIFIED"
                        },
                        "amount": {"rawValue": 1000, "currency": "USD", "displayValue": "$1,000"},
                        "hourlyBudgetMin": {"rawValue": 50, "currency": "USD", "displayValue": "$50"},
                        "hourlyBudgetMax": {"rawValue": 100, "currency": "USD", "displayValue": "$100"}
                      }
                    }
                  ],
                  "pageInfo": {"endCursor": "cursor-1", "hasNextPage": false}
                }
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var result = await client.SearchMarketplaceJobPostingsAsync(
            UpworkMarketplaceJobFilters.Keywords("graphql", first: 10));

        result.TotalCount.Should().Be(1);
        result.PageInfo?.HasNextPage.Should().BeFalse();
        var job = result.Edges.Should().ContainSingle().Subject.Node;
        job?.Id.Should().Be("job-1");
        job?.Title.Should().Be("Build a GraphQL SDK");
        job?.TotalApplicants.Should().Be(3);
        job?.Client?.VerificationStatus.Should().Be("VERIFIED");
        job?.Skills.Should().ContainSingle();
        handler.Requests[0].Content.Should().Contain("marketplaceJobPostingsSearch");
        handler.Requests[0].Content.Should().NotContain("marketplaceJobPostings(");
    }

    [TestMethod]
    public async Task GetMarketplaceJobPostingsContentsAsync_DeserializesContentRecords()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "data": {
                "marketplaceJobPostingsContents": [
                  {
                    "id": "job-1",
                    "ciphertext": "cipher",
                    "title": "Build an SDK",
                    "description": "Detailed description",
                    "publishedDateTime": "2026-01-02T03:04:05Z",
                    "annotations": {"tags": ["remote"]}
                  }
                ]
              }
            }
            """));

        using var client = CreateMockClient(handler);
        var records = await client.GetMarketplaceJobPostingsContentsAsync(["job-1"]);

        var record = records.Should().ContainSingle().Subject;
        record.Id.Should().Be("job-1");
        record.Title.Should().Be("Build an SDK");
        record.Description.Should().Be("Detailed description");
        record.PublishedDateTime.Should().Be(DateTimeOffset.Parse("2026-01-02T03:04:05Z"));
        record.Annotations?.Tags.Should().ContainSingle().Which.Should().Be("remote");
        handler.Requests[0].Content.Should().Contain("marketplaceJobPostingsContents");
    }

    [TestMethod]
    public async Task GraphQLErrors_WithMissingScope_ThrowDedicatedException()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "errors": [
                {
                  "message": "User does not have required OAuth2 permission: Read marketplace Job Postings"
                }
              ],
              "data": null
            }
            """));

        using var client = CreateMockClient(handler);
        var act = async () => await client.SearchMarketplaceJobPostingsAsync();

        await act.Should().ThrowAsync<UpworkMissingScopeException>();
    }

    [TestMethod]
    public async Task HttpErrors_RedactAccessTokenFromExceptionBody()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {"debug":"access-token"}
            """,
            HttpStatusCode.InternalServerError));

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions { AccessToken = "access-token" });
        var act = async () => await client.GetCompanySelectorAsync();

        var exception = await act.Should().ThrowAsync<UpworkHttpException>();
        exception.Which.ResponseBody.Should().NotContain("access-token");
        exception.Which.ResponseBody.Should().Contain("[REDACTED]");
    }

    [TestMethod]
    public async Task RateLimitResponse_ThrowsRetryAfterException()
    {
        var response = JsonResponse("""{"error":"rate limit"}""", HttpStatusCode.TooManyRequests);
        response.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(3));
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(response);

        using var client = CreateMockClient(handler);
        var act = async () => await client.GetCompanySelectorAsync();

        var exception = await act.Should().ThrowAsync<UpworkRateLimitException>();
        exception.Which.RetryAfter.Should().Be(TimeSpan.FromSeconds(3));
    }

    [TestMethod]
    public async Task RateLimitHandler_CanRetryRequest()
    {
        var rateLimit = JsonResponse("""{"error":"rate limit"}""", HttpStatusCode.TooManyRequests);
        rateLimit.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.Zero);
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(rateLimit);
        handler.Enqueue(JsonResponse("""{"data":{"companySelector":{"items":[]}}}"""));
        var rateLimitHandler = new TestRateLimitHandler(TimeSpan.Zero);

        using var client = CreateMockClient(
            handler,
            new UpworkClientOptions
            {
                AccessToken = "access-token",
                RateLimitHandler = rateLimitHandler,
                MaxRateLimitRetries = 1,
            });
        var selector = await client.GetCompanySelectorAsync();

        selector.Items.Should().BeEmpty();
        handler.Requests.Should().HaveCount(2);
        rateLimitHandler.Contexts.Should().ContainSingle();
        rateLimitHandler.Contexts[0].RetryAfter.Should().Be(TimeSpan.Zero);
    }
}
