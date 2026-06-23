namespace Upwork;

internal static class UpworkSecretRedactor
{
    private const string Redacted = "[REDACTED]";

    public static string? Redact(string? value, IEnumerable<string?>? secrets)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var redacted = value;
        if (secrets is null)
        {
            return redacted;
        }

        foreach (var secret in secrets)
        {
            if (string.IsNullOrWhiteSpace(secret))
            {
                continue;
            }

            redacted = redacted.Replace(secret, Redacted, StringComparison.Ordinal);
        }

        return redacted;
    }
}
