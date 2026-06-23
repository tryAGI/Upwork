using System.Net;
using System.Runtime.Serialization;

namespace Upwork;

/// <summary>
/// Base exception type for Upwork SDK failures.
/// </summary>
public class UpworkException : Exception
{
    /// <summary>
    /// Creates an empty Upwork exception.
    /// </summary>
    public UpworkException()
    {
    }

    /// <summary>
    /// Creates an Upwork exception with a message.
    /// </summary>
    public UpworkException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates an Upwork exception with a message and inner exception.
    /// </summary>
    public UpworkException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Represents a non-success HTTP response from Upwork.
/// </summary>
public class UpworkHttpException : UpworkException
{
    /// <summary>
    /// Creates an empty HTTP exception.
    /// </summary>
    public UpworkHttpException()
    {
    }

    /// <summary>
    /// Creates an HTTP exception with a message.
    /// </summary>
    public UpworkHttpException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates an HTTP exception with a message and inner exception.
    /// </summary>
    public UpworkHttpException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Creates an HTTP exception with status and response body details.
    /// </summary>
    public UpworkHttpException(HttpStatusCode statusCode, string? responseBody)
        : this(statusCode, responseBody, null)
    {
    }

    /// <summary>
    /// Creates an HTTP exception with status, response body, and sensitive values to redact.
    /// </summary>
    public UpworkHttpException(
        HttpStatusCode statusCode,
        string? responseBody,
        IEnumerable<string?>? sensitiveValues)
        : base($"Upwork returned HTTP {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = UpworkSecretRedactor.Redact(responseBody, sensitiveValues);
    }

    /// <summary>
    /// HTTP status returned by Upwork.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Raw response body returned by Upwork, when available.
    /// </summary>
    public string? ResponseBody { get; }
}

/// <summary>
/// Represents an HTTP 429 rate-limit response from Upwork.
/// </summary>
public sealed class UpworkRateLimitException : UpworkHttpException
{
    /// <summary>
    /// Creates an empty rate-limit exception.
    /// </summary>
    public UpworkRateLimitException()
    {
    }

    /// <summary>
    /// Creates a rate-limit exception with a message.
    /// </summary>
    public UpworkRateLimitException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a rate-limit exception with a message and inner exception.
    /// </summary>
    public UpworkRateLimitException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Creates a rate-limit exception.
    /// </summary>
    public UpworkRateLimitException(
        HttpStatusCode statusCode,
        string? responseBody,
        TimeSpan? retryAfter,
        IEnumerable<string?>? sensitiveValues)
        : base(statusCode, responseBody, sensitiveValues)
    {
        RetryAfter = retryAfter;
    }

    /// <summary>
    /// Delay parsed from the <c>Retry-After</c> header, when provided.
    /// </summary>
    public TimeSpan? RetryAfter { get; }
}

/// <summary>
/// Represents GraphQL errors returned in a successful HTTP response.
/// </summary>
public class UpworkGraphQLException : UpworkException
{
    /// <summary>
    /// Creates an empty GraphQL exception.
    /// </summary>
    public UpworkGraphQLException()
    {
        Errors = [];
    }

    /// <summary>
    /// Creates a GraphQL exception with a message.
    /// </summary>
    public UpworkGraphQLException(string? message)
        : base(message)
    {
        Errors = [];
    }

    /// <summary>
    /// Creates a GraphQL exception with a message and inner exception.
    /// </summary>
    public UpworkGraphQLException(string? message, Exception? innerException)
        : base(message, innerException)
    {
        Errors = [];
    }

    /// <summary>
    /// Creates a GraphQL exception with the returned error list.
    /// </summary>
    public UpworkGraphQLException(IReadOnlyList<UpworkGraphQLError> errors)
        : base(CreateMessage(errors))
    {
        Errors = errors;
    }

    /// <summary>
    /// GraphQL errors returned by Upwork.
    /// </summary>
    public IReadOnlyList<UpworkGraphQLError> Errors { get; }

    private static string CreateMessage(IReadOnlyList<UpworkGraphQLError> errors)
    {
        if (errors.Count == 0)
        {
            return "Upwork returned one or more GraphQL errors.";
        }

        return errors.Count == 1
            ? $"Upwork returned a GraphQL error: {errors[0].Message}"
            : $"Upwork returned {errors.Count} GraphQL errors: {errors[0].Message}";
    }
}

/// <summary>
/// Represents GraphQL errors caused by missing OAuth scopes or permissions.
/// </summary>
public sealed class UpworkMissingScopeException : UpworkGraphQLException
{
    /// <summary>
    /// Creates an empty missing-scope exception.
    /// </summary>
    public UpworkMissingScopeException()
    {
    }

    /// <summary>
    /// Creates a missing-scope exception with a message.
    /// </summary>
    public UpworkMissingScopeException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a missing-scope exception with a message and inner exception.
    /// </summary>
    public UpworkMissingScopeException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Creates a missing-scope exception with the returned GraphQL errors.
    /// </summary>
    public UpworkMissingScopeException(IReadOnlyList<UpworkGraphQLError> errors)
        : base(errors)
    {
    }
}

/// <summary>
/// Represents an unexpected Upwork response shape.
/// </summary>
public class UpworkInvalidResponseException : UpworkException
{
    /// <summary>
    /// Creates an empty invalid-response exception.
    /// </summary>
    public UpworkInvalidResponseException()
    {
    }

    /// <summary>
    /// Creates an invalid-response exception with a message.
    /// </summary>
    public UpworkInvalidResponseException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates an invalid-response exception with a message and inner exception.
    /// </summary>
    public UpworkInvalidResponseException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
