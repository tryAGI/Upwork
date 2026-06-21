/*
order: 30
title: Marketplace Job Content
slug: marketplace-job-content

Fetch full marketplace job content after finding a job ID from search.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_MarketplaceJobContent()
    {
        using var client = GetAuthenticatedClient();

        //// Find a job, then fetch its content by ID.
        var search = await client.SearchMarketplaceJobPostingsAsync(
            new UpworkMarketplaceJobFilter
            {
                SearchExpression = "dotnet",
                Pagination = new UpworkCursorPagination(first: 1),
            });

        var firstJob = search.Edges?.FirstOrDefault()?.Node;
        firstJob.Should().NotBeNull();
        firstJob!.Id.Should().NotBeNullOrWhiteSpace();

        var job = await client.GetMarketplaceJobPostingAsync(firstJob.Id!);

        job.Should().NotBeNull();
        job!.Content?.Title.Should().NotBeNullOrWhiteSpace();
    }
}
