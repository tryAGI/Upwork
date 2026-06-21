/*
order: 40
title: Proposal Metadata
slug: proposal-metadata

Read proposal metadata for freelancer proposal workflows.
*/

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Example_ProposalMetadata()
    {
        using var client = GetAuthenticatedClient();

        //// Fetch proposal reference metadata, including engagement durations and decline reasons.
        var metadata = await client.GetProposalMetadataAsync(UpworkReasonTypes.ProposalDecline);

        metadata.EngagementDurationValues.Should().NotBeNull();
        metadata.Reasons.Should().NotBeNull();
    }
}
