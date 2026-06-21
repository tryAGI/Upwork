/*
order: 50
title: Vendor Proposal Search
slug: vendor-proposal-search

List vendor proposals for a freelancer or agency account.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_VendorProposalSearch()
    {
        using var client = GetAuthenticatedClient();

        //// List accepted vendor proposals with cursor pagination.
        var response = await client.SearchVendorProposalsAsync(
            UpworkVendorProposalFilters.ByStatus(UpworkProposalStatuses.Accepted),
            pagination: new UpworkCursorPagination(first: 10));

        response.Edges.Should().NotBeNull();
        response.PageInfo.Should().NotBeNull();
    }
}
