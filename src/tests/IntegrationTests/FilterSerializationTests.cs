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
            VerifiedPaymentOnly = true,
            Workload = UpworkWorkloads.FullTime,
            OccupationIdsAny = ["dev-1"],
            Pagination = new UpworkOffsetPagination(pageOffset: 0, pageSize: 5),
        };

        var json = JsonSerializer.Serialize(filter, UpworkJsonContext.Default.UpworkPublicMarketplaceJobFilter);

        json.Should().Contain("\"searchExpression_eq\":\"dotnet\"");
        json.Should().Contain("\"jobType_eq\":\"FIXED\"");
        json.Should().Contain("\"verifiedPaymentOnly_eq\":true");
        json.Should().Contain("\"workload_eq\":\"FULL_TIME\"");
        json.Should().Contain("\"occupationIds_any\":[\"dev-1\"]");
        json.Should().Contain("\"pageOffset\":0");
        json.Should().Contain("\"pageSize\":5");
    }

    [TestMethod]
    public void MarketplaceFilter_UsesUpworkSchemaFieldNames()
    {
        var filter = new UpworkMarketplaceJobFilter
        {
            SearchExpression = "graphql",
            CategoryIdsAny = ["web-mobile-software-dev"],
            OntologySkillIdsAll = ["csharp"],
            JobType = UpworkJobTypes.Hourly,
            ExperienceLevel = UpworkExperienceLevels.Expert,
            DurationAny = [UpworkJobDurations.Month],
            VerifiedPaymentOnly = true,
            Pagination = new UpworkCursorPagination(after: "cursor-1", first: 10),
        };

        var json = JsonSerializer.Serialize(filter, UpworkJsonContext.Default.UpworkMarketplaceJobFilter);

        json.Should().Contain("\"searchExpression_eq\":\"graphql\"");
        json.Should().Contain("\"categoryIds_any\":[\"web-mobile-software-dev\"]");
        json.Should().Contain("\"ontologySkillIds_all\":[\"csharp\"]");
        json.Should().Contain("\"jobType_eq\":\"HOURLY\"");
        json.Should().Contain("\"experienceLevel_eq\":\"EXPERT\"");
        json.Should().Contain("\"duration_any\":[\"MONTH\"]");
        json.Should().Contain("\"verifiedPaymentOnly_eq\":true");
        json.Should().Contain("\"pagination_eq\"");
        json.Should().Contain("\"after\":\"cursor-1\"");
        json.Should().Contain("\"first\":10");
    }

    [TestMethod]
    public void VendorProposalFilter_UsesUpworkSchemaFieldNames()
    {
        var filter = new UpworkVendorProposalFilter
        {
            Status = UpworkProposalStatuses.Accepted,
            Annotation = UpworkVendorProposalAnnotations.Hidden,
            FreelancerId = "freelancer-1",
            OrganizationId = "organization-1",
            JobPostingIdsAny = ["job-1"],
        };

        var json = JsonSerializer.Serialize(filter, UpworkJsonContext.Default.UpworkVendorProposalFilter);

        json.Should().Contain("\"status_eq\":\"Accepted\"");
        json.Should().Contain("\"annotations_eq\":\"Hidden\"");
        json.Should().Contain("\"freelancerId_eq\":\"freelancer-1\"");
        json.Should().Contain("\"organizationId_eq\":\"organization-1\"");
        json.Should().Contain("\"jobPostingIds_any\":[\"job-1\"]");
    }

    [TestMethod]
    public void VendorProposalSort_UsesUpworkSchemaFieldNames()
    {
        var sort = new UpworkVendorProposalSort(
            UpworkVendorProposalSortFields.ModifiedDateTime,
            UpworkSortOrders.Ascending);

        var json = JsonSerializer.Serialize(sort, UpworkJsonContext.Default.UpworkVendorProposalSort);

        json.Should().Contain("\"field\":\"MODIFIEDDATETIME\"");
        json.Should().Contain("\"sortOrder\":\"ASC\"");
    }

    [TestMethod]
    public void FilterBuilders_CreateCommonFreelancerFilters()
    {
        var publicFilter = UpworkPublicMarketplaceJobFilters.Durations([UpworkJobDurations.Week], pageSize: 5);
        var marketplaceFilter = UpworkMarketplaceJobFilters.Skills(["ontology-skill-1"], first: 15);
        var proposalFilter = UpworkVendorProposalFilters.ForJobPosting("job-1");

        publicFilter.DurationAny.Should().BeEquivalentTo([UpworkJobDurations.Week]);
        publicFilter.Pagination?.PageSize.Should().Be(5);
        marketplaceFilter.OntologySkillIdsAll.Should().BeEquivalentTo(["ontology-skill-1"]);
        marketplaceFilter.Pagination?.First.Should().Be(15);
        proposalFilter.Status.Should().Be(UpworkProposalStatuses.Accepted);
        proposalFilter.JobPostingIdsAny.Should().BeEquivalentTo(["job-1"]);
    }

    [TestMethod]
    public void DirectUploadLinkInput_UsesUpworkSchemaFieldNames()
    {
        var input = new UpworkCreateDirectUploadLinkInput("proposal.pdf")
        {
            ContentType = "application/pdf",
            MaxFileSize = 1_048_576,
            SslEnabled = true,
            MetaData = "proposal-attachment",
        };

        var json = JsonSerializer.Serialize(input, UpworkJsonContext.Default.UpworkCreateDirectUploadLinkInput);

        json.Should().Contain("\"fileName\":\"proposal.pdf\"");
        json.Should().Contain("\"contentType\":\"application/pdf\"");
        json.Should().Contain("\"maxFileSize\":1048576");
        json.Should().Contain("\"sslEnabled\":true");
        json.Should().Contain("\"metaData\":\"proposal-attachment\"");
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
