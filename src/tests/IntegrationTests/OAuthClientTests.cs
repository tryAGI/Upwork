using System.Net;

namespace Upwork.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void OAuthAuthorizationUri_UsesOfficialAuthorizationCodeParameters()
    {
        var config = new UpworkOAuthConfig(
            "client-id",
            "client-secret",
            new Uri("https://example.test/callback"));
        using var oauth = new UpworkOAuthClient(config);

        var uri = oauth.CreateAuthorizationUri(state: "state value");

        uri.Host.Should().Be("www.upwork.com");
        uri.AbsoluteUri.Should().Contain("response_type=code");
        uri.AbsoluteUri.Should().Contain("client_id=client-id");
        uri.AbsoluteUri.Should().Contain("redirect_uri=https%3A%2F%2Fexample.test%2Fcallback");
        uri.AbsoluteUri.Should().Contain("state=state%20value");
        uri.AbsoluteUri.Should().NotContain("scope=");
    }

    [TestMethod]
    public async Task ExchangeAuthorizationCodeAsync_SendsOfficialFormFields()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "access_token": "access-token",
              "refresh_token": "refresh-token",
              "token_type": "Bearer",
              "expires_in": 86400
            }
            """));
        var config = new UpworkOAuthConfig(
            "client-id",
            "client-secret",
            new Uri("https://example.test/callback"));

        using var oauth = new UpworkOAuthClient(config, new HttpClient(handler));
        var response = await oauth.ExchangeAuthorizationCodeAsync("authorization-code");

        response.AccessToken.Should().Be("access-token");
        response.RefreshToken.Should().Be("refresh-token");
        response.TokenType.Should().Be("Bearer");
        response.ExpiresIn.Should().Be(86400);
        handler.Requests.Should().ContainSingle();
        var request = handler.Requests[0];
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be(UpworkOAuthClient.DefaultTokenEndpoint);
        request.Content.Should().Contain("grant_type=authorization_code");
        request.Content.Should().Contain("client_id=client-id");
        request.Content.Should().Contain("client_secret=client-secret");
        request.Content.Should().Contain("code=authorization-code");
        request.Content.Should().Contain("redirect_uri=https%3A%2F%2Fexample.test%2Fcallback");
    }

    [TestMethod]
    public async Task RefreshTokenAsync_SendsOfficialFormFields()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {
              "access_token": "new-access-token",
              "refresh_token": "new-refresh-token",
              "token_type": "Bearer",
              "expires_in": 86400
            }
            """));
        var config = new UpworkOAuthConfig(
            "client-id",
            "client-secret",
            new Uri("https://example.test/callback"));

        using var oauth = new UpworkOAuthClient(config, new HttpClient(handler));
        var response = await oauth.RefreshTokenAsync("old-refresh-token");

        response.AccessToken.Should().Be("new-access-token");
        response.RefreshToken.Should().Be("new-refresh-token");
        handler.Requests.Should().ContainSingle();
        var request = handler.Requests[0];
        request.Content.Should().Contain("grant_type=refresh_token");
        request.Content.Should().Contain("client_id=client-id");
        request.Content.Should().Contain("client_secret=client-secret");
        request.Content.Should().Contain("refresh_token=old-refresh-token");
    }

    [TestMethod]
    public async Task TokenErrors_RedactSecretsFromExceptionBody()
    {
        var handler = new MockHttpMessageHandler();
        handler.Enqueue(JsonResponse(
            """
            {"error":"invalid_grant","debug":"client-secret authorization-code"}
            """,
            HttpStatusCode.BadRequest));
        var config = new UpworkOAuthConfig(
            "client-id",
            "client-secret",
            new Uri("https://example.test/callback"));

        using var oauth = new UpworkOAuthClient(config, new HttpClient(handler));
        var act = async () => await oauth.ExchangeAuthorizationCodeAsync("authorization-code");

        var exception = await act.Should().ThrowAsync<UpworkHttpException>();
        exception.Which.ResponseBody.Should().NotContain("client-secret");
        exception.Which.ResponseBody.Should().NotContain("authorization-code");
        exception.Which.ResponseBody.Should().Contain("[REDACTED]");
    }
}
