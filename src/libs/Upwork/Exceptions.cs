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
        : base($"Upwork returned HTTP {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
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
