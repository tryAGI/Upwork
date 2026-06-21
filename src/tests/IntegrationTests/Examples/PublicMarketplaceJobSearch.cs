/*
order: 10
title: Public Marketplace Job Search
slug: public-marketplace-job-search

Search public marketplace jobs with the Upwork GraphQL API.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_PublicMarketplaceJobSearch()
    {
        using var client = GetAuthenticatedClient();

        //// Search current public marketplace jobs.
        var response = await client.SearchPublicMarketplaceJobPostingsAsync(
            new UpworkPublicMarketplaceJobFilter
            {
                SearchExpression = "dotnet graphql",
                JobType = UpworkJobTypes.Fixed,
                PaymentVerified = true,
                Pagination = new UpworkOffsetPagination(pageOffset: 0, pageSize: 10),
            });

        response.Jobs.Should().NotBeNull();
        response.Paging.Should().NotBeNull();
    }
}
