# Authentication

Upwork GraphQL calls use OAuth2 bearer tokens. Pass an access token to `UpworkClient` for authenticated queries:

```csharp
using var client = new UpworkClient(accessToken);
```

Some organization-scoped calls require the tenant header documented by Upwork. Pass the tenant ID in the constructor when needed:

```csharp
using var client = new UpworkClient(accessToken, tenantId);
```

Use `UpworkOAuthClient` when your application needs to exchange an authorization code, refresh token, or enterprise client-credentials key for an access token.
