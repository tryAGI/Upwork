using System.Text.Json;

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void PublicMarketplaceFilter_UsesUpworkSchemaFieldNames()
    {
        var filter = new UpworkPublicMarketplaceJobFilter
        {
            SearchExpression = "dotnet",
            JobType = UpworkJobTypes.Fixed,
            PaymentVerified = true,
            Pagination = new UpworkOffsetPagination(pageOffset: 0, pageSize: 5),
        };

        var json = JsonSerializer.Serialize(filter, UpworkJsonContext.Default.UpworkPublicMarketplaceJobFilter);

        json.Should().Contain("\"searchExpression_eq\":\"dotnet\"");
        json.Should().Contain("\"jobType_eq\":\"FIXED\"");
        json.Should().Contain("\"paymentVerified_eq\":true");
        json.Should().Contain("\"pageOffset\":0");
        json.Should().Contain("\"pageSize\":5");
    }

    [TestMethod]
    public void MarketplaceFilter_UsesCursorPaginationFieldName()
    {
        var filter = new UpworkMarketplaceJobFilter
        {
            SearchExpression = "graphql",
            VerifiedPaymentOnly = true,
            Pagination = new UpworkCursorPagination(after: "cursor-1", first: 10),
        };

        var json = JsonSerializer.Serialize(filter, UpworkJsonContext.Default.UpworkMarketplaceJobFilter);

        json.Should().Contain("\"searchExpression_eq\":\"graphql\"");
        json.Should().Contain("\"verifiedPaymentOnly_eq\":true");
        json.Should().Contain("\"pagination_eq\"");
        json.Should().Contain("\"after\":\"cursor-1\"");
        json.Should().Contain("\"first\":10");
    }

    [TestMethod]
    public void OAuthAuthorizationUri_IncludesExpectedParameters()
    {
        var uri = UpworkOAuthClient.CreateAuthorizationUri(
            "client-id",
            new Uri("https://example.test/callback"),
            state: "state value",
            scopes: ["jobs:read"]);

        uri.Host.Should().Be("www.upwork.com");
        uri.AbsoluteUri.Should().Contain("response_type=code");
        uri.AbsoluteUri.Should().Contain("client_id=client-id");
        uri.AbsoluteUri.Should().Contain("redirect_uri=https%3A%2F%2Fexample.test%2Fcallback");
        uri.AbsoluteUri.Should().Contain("state=state%20value");
        uri.AbsoluteUri.Should().Contain("scope=jobs%3Aread");
    }
}
