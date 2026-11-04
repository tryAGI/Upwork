using System.Text.Json;
using System.Text.Json.Serialization;

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    [DataRow("2026-11-04T08:02:36+0000", 0, 0)]
    [DataRow("2026-11-04T08:02:36.781+0000", 781, 0)]
    [DataRow("2026-11-04T08:02:36Z", 0, 0)]
    [DataRow("2026-11-04T12:02:36+04:00", 0, 4)]
    public async Task SearchMarketplaceJobPostingsAsync_ParsesTimestampOffsets(
        string timestamp, int milliseconds, int offsetHours)
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse("""
            {"data":{"marketplaceJobPostingsSearch":{"edges":[{"node":{
              "id":"job-1","publishedDateTime":"TIMESTAMP","createdDateTime":"TIMESTAMP",
              "renewedDateTime":"TIMESTAMP","client":{"memberSinceDateTime":"TIMESTAMP"}
            }}]}}}
            """.Replace("TIMESTAMP", timestamp, StringComparison.Ordinal)));
        using var client = CreateMockClient(handler);

        var page = await client.SearchMarketplaceJobPostingsAsync();
        var job = page.Edges?.Single().Node ?? throw new InvalidOperationException("Missing search result.");
        var expected = new DateTimeOffset(2026, 11, 4, 8, 2, 36, milliseconds, TimeSpan.Zero);
        job.PublishedDateTime.Should().Be(expected);
        job.PublishedDateTime?.Offset.Should().Be(TimeSpan.FromHours(offsetHours));
        job.CreatedDateTime.Should().Be(expected);
        job.RenewedDateTime.Should().Be(expected);
        job.Client?.MemberSinceDateTime.Should().Be(expected);
    }

    [TestMethod]
    public void TimestampModels_SupportExternalSourceGeneratedMetadataAndRoundTrips()
    {
        const string json = """{"publishedDateTime":"2026-11-04T12:02:36.781+0400"}""";
        var metadata = TimestampJsonContext.Default.UpworkMarketplaceJobPostingSearchResult;
        var job = JsonSerializer.Deserialize(json, metadata) ?? throw new InvalidOperationException("Missing result.");
        var expected = new DateTimeOffset(2026, 11, 4, 12, 2, 36, 781, TimeSpan.FromHours(4));
        job.PublishedDateTime.Should().Be(expected);
        job.PublishedDateTime?.Offset.Should().Be(TimeSpan.FromHours(4));

        var roundTrip = JsonSerializer.Deserialize(JsonSerializer.Serialize(job, metadata), metadata)
            ?? throw new InvalidOperationException("Missing round-trip result.");
        roundTrip.PublishedDateTime.Should().Be(expected);
        roundTrip.PublishedDateTime?.Offset.Should().Be(TimeSpan.FromHours(4));

        var content = JsonSerializer.Deserialize(json, TimestampJsonContext.Default.UpworkMarketplaceJobPostingContentRecord);
        content?.PublishedDateTime.Should().Be(expected);
        var proposal = JsonSerializer.Deserialize(
            """{"createdDateTime":"2026-11-04T12:02:36.781+0400","modifiedDateTime":"2026-11-04T12:02:36.781+0400"}""",
            TimestampJsonContext.Default.UpworkProposalAuditDetails);
        proposal?.CreatedDateTime.Should().Be(expected);
        proposal?.ModifiedDateTime.Should().Be(expected);
    }

    [TestMethod]
    public void TimestampModels_PreserveNullValues()
    {
        var metadata = TimestampJsonContext.Default.UpworkMarketplaceJobPostingSearchResult;
        var job = JsonSerializer.Deserialize("""{"publishedDateTime":null}""", metadata)
            ?? throw new InvalidOperationException("Missing result.");
        job.PublishedDateTime.Should().BeNull();
        var roundTrip = JsonSerializer.Deserialize(JsonSerializer.Serialize(job, metadata), metadata);
        roundTrip?.PublishedDateTime.Should().BeNull();
    }

    [TestMethod]
    [DataRow("\"not-a-timestamp\"")]
    [DataRow("\"\"")]
    [DataRow("123")]
    [DataRow("{}")]
    public void TimestampModels_RejectMalformedValues(string valueJson)
    {
        var json = "{\"publishedDateTime\":" + valueJson + "}";
        Action deserialize = () => JsonSerializer.Deserialize(
            json, TimestampJsonContext.Default.UpworkMarketplaceJobPostingSearchResult);
        deserialize.Should().Throw<JsonException>();
    }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(UpworkMarketplaceJobPostingSearchResult))]
[JsonSerializable(typeof(UpworkMarketplaceJobPostingContentRecord))]
[JsonSerializable(typeof(UpworkProposalAuditDetails))]
internal sealed partial class TimestampJsonContext : JsonSerializerContext;
