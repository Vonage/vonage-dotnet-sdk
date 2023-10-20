using System;
using Newtonsoft.Json;

namespace Vonage.NumberInsights;

/// <summary>
///     Real time data about the number.
/// </summary>
public class RealTimeData
{
    /// <summary>
    ///     Whether the end-user's phone number is active within an operator's network.
    /// </summary>
    [JsonProperty("active_status")]
    [JsonConverter(typeof(StatusConverter))]
    public bool ActiveStatus { get; set; }

    /// <summary>
    ///     Whether the end-user's handset is turned on or off.
    /// </summary>
    [JsonProperty("handset_status")]
    public string HandsetStatus { get; set; }
}

internal class StatusConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(bool);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer) =>
        (string) reader.Value == "active";

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (bool.TryParse(value?.ToString(), out var boolValue))
        {
            writer.WriteValue(boolValue.ToString().ToLowerInvariant());
        }
        else writer.WriteNull();
    }
}