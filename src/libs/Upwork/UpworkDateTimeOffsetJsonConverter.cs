using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Upwork;

/// <summary>
/// Reads Upwork timestamp strings, including compact UTC offsets such as +0000.
/// </summary>
public sealed class UpworkDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset?>
{
    /// <inheritdoc />
    public override bool HandleNull => true;

    /// <inheritdoc />
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType == JsonTokenType.String &&
            DateTimeOffset.TryParse(reader.GetString(), CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out var timestamp))
        {
            return timestamp;
        }

        throw new JsonException("Expected a valid Upwork timestamp string.");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);

        if (value is { } timestamp)
        {
            writer.WriteStringValue(timestamp);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
