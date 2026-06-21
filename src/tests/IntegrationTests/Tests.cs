namespace Upwork.IntegrationTests;

[TestClass]
public partial class Tests
{
    private static UpworkClient GetAuthenticatedClient()
    {
        var accessToken =
            Environment.GetEnvironmentVariable("UPWORK_ACCESS_TOKEN") is { Length: > 0 } accessTokenValue
                ? accessTokenValue
                : throw new AssertInconclusiveException("UPWORK_ACCESS_TOKEN environment variable is not found.");

        var tenantId = Environment.GetEnvironmentVariable("UPWORK_TENANT_ID") is { Length: > 0 } tenantIdValue
            ? tenantIdValue
            : null;

        return new UpworkClient(accessToken, tenantId);
    }
}
