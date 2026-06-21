/*
order: 20
title: Authenticated Marketplace Job Search
slug: marketplace-job-search

Search marketplace jobs from an authenticated freelancer workflow.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_MarketplaceJobSearch()
    {
        using var client = GetAuthenticatedClient();

        //// Search marketplace jobs with cursor pagination and recency sorting.
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

        response.Edges.Should().NotBeNull();
        response.PageInfo.Should().NotBeNull();
    }
}
