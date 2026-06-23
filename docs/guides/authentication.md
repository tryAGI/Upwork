# Authentication

Upwork GraphQL calls use OAuth2 bearer tokens.

## Authorization Code

```csharp
using Upwork;

var config = new UpworkOAuthConfig(
    Environment.GetEnvironmentVariable("UPWORK_CLIENT_ID")
        ?? throw new InvalidOperationException("UPWORK_CLIENT_ID is required."),
    Environment.GetEnvironmentVariable("UPWORK_CLIENT_SECRET")
        ?? throw new InvalidOperationException("UPWORK_CLIENT_SECRET is required."),
    new Uri(Environment.GetEnvironmentVariable("UPWORK_REDIRECT_URI")
        ?? throw new InvalidOperationException("UPWORK_REDIRECT_URI is required.")));

using var oauth = new UpworkOAuthClient(config);

var authorizationUrl = oauth.CreateAuthorizationUri(
    state: Environment.GetEnvironmentVariable("UPWORK_OAUTH_STATE"));
```

After the user authorizes the app and your backend receives the authorization code:

```csharp
var token = await oauth.ExchangeAuthorizationCodeAsync(
    Environment.GetEnvironmentVariable("UPWORK_AUTHORIZATION_CODE")
        ?? throw new InvalidOperationException("UPWORK_AUTHORIZATION_CODE is required."));
```

## Refresh Token

```csharp
var refreshed = await oauth.RefreshTokenAsync(
    Environment.GetEnvironmentVariable("UPWORK_REFRESH_TOKEN")
        ?? throw new InvalidOperationException("UPWORK_REFRESH_TOKEN is required."));
```

## GraphQL Client

```csharp
using var client = new UpworkClient(
    new UpworkClientOptions
    {
        AccessToken = Environment.GetEnvironmentVariable("UPWORK_ACCESS_TOKEN"),
        TenantId = Environment.GetEnvironmentVariable("UPWORK_TENANT_ID"),
    });
```

The SDK does not persist tokens. Use your backend storage and pass tokens through `AccessToken` or `IUpworkAccessTokenProvider`.
