using System.Net;
using System.Text;

namespace Upwork.IntegrationTests;

[TestClass]
public partial class Tests
{
    private static HttpResponseMessage JsonResponse(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
        };
    }

    private sealed class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> _responses = new();

        public List<CapturedRequest> Requests { get; } = [];

        public void Enqueue(HttpResponseMessage response)
        {
            _responses.Enqueue(response);
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var content = request.Content is null
                ? null
                : await request.Content.ReadAsStringAsync(cancellationToken);

            Requests.Add(new CapturedRequest(
                request.Method,
                request.RequestUri,
                content,
                request.Headers.Authorization?.ToString(),
                request.Headers.TryGetValues("X-Upwork-API-TenantId", out var tenantValues)
                    ? tenantValues.SingleOrDefault()
                    : null));

            return _responses.Dequeue();
        }
    }

    private sealed record CapturedRequest(
        HttpMethod Method,
        Uri? RequestUri,
        string? Content,
        string? Authorization,
        string? TenantId);

    private sealed class StaticAccessTokenProvider(string accessToken) : IUpworkAccessTokenProvider
    {
        public ValueTask<string?> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<string?>(accessToken);
        }
    }

    private sealed class TestRateLimitHandler(TimeSpan? delay) : IUpworkRateLimitHandler
    {
        public List<UpworkRateLimitContext> Contexts { get; } = [];

        public ValueTask<TimeSpan?> GetDelayAsync(
            UpworkRateLimitContext context,
            CancellationToken cancellationToken = default)
        {
            Contexts.Add(context);
            return ValueTask.FromResult(delay);
        }
    }

    private static UpworkClient CreateMockClient(
        MockHttpMessageHandler handler,
        UpworkClientOptions? options = null)
    {
        return new UpworkClient(
            options ?? new UpworkClientOptions { AccessToken = "access-token" },
            new HttpClient(handler));
    }
}
