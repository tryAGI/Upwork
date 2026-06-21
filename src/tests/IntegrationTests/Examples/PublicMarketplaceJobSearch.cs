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
            UpworkPublicMarketplaceJobFilters.Keywords("dotnet graphql", pageSize: 10) with
            {
                JobType = UpworkJobTypes.Fixed,
                VerifiedPaymentOnly = true,
            });

        response.Jobs.Should().NotBeNull();
        response.Paging.Should().NotBeNull();
    }
}
