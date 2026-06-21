/*
order: 60
title: Vendor Proposal Details
slug: vendor-proposal-details

Read a vendor proposal after finding it in the freelancer proposal list.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_VendorProposalDetails()
    {
        using var client = GetAuthenticatedClient();

        //// Find one accepted proposal and then load its detail record.
        var response = await client.SearchVendorProposalsAsync(
            UpworkVendorProposalFilters.ByStatus(UpworkProposalStatuses.Accepted),
            pagination: new UpworkCursorPagination(first: 1));

        var proposalId = response.Edges?
            .Select(edge => edge.Node?.Id)
            .FirstOrDefault(id => id is { Length: > 0 });

        if (proposalId is not { Length: > 0 })
        {
            throw new AssertInconclusiveException("No accepted vendor proposals were returned for this account.");
        }

        var proposal = await client.GetVendorProposalAsync(proposalId);

        proposal.Should().NotBeNull();
        proposal?.Id.Should().Be(proposalId);
    }
}
