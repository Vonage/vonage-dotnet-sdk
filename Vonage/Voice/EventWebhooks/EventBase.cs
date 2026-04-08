#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vonage.Serialization;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Base class for all Voice API webhook events. Provides a common timestamp and a factory method to deserialize incoming webhook JSON into the appropriate event type.
/// </summary>
public class EventBase
{
    /// <summary>
    ///     The timestamp of the event in ISO 8601 format.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonPropertyName("timestamp")]
    public DateTime TimeStamp { get; set; }

    /// <summary>
    ///     Parses a Voice API webhook JSON payload into the appropriate event type based on its content (status, DTMF, speech, recording, error, or transfer).
    /// </summary>
    /// <param name="json">The raw JSON string from the webhook request body.</param>
    /// <returns>The deserialized event as the appropriate subclass of <see cref="EventBase"/>, or <c>null</c> if the event type cannot be determined.</returns>
    public static EventBase ParseEvent(string json)
    {
        var data = (JObject) JsonConvert.DeserializeObject(json);
        if (data.Property("status") != null)
        {
            return DeserializeStatus(json, data.Property("status"));
        }

        if (data.Property("conversation_uuid_from") != null)
        {
            return JsonConvert.DeserializeObject<Transfer>(json, VonageSerialization.SerializerSettings);
        }

        if (data.Property("speech") != null)
        {
            return JsonConvert.DeserializeObject<MultiInput>(json, VonageSerialization.SerializerSettings);
        }

        if (data.Property("dtmf") != null)
        {
            if (data["dtmf"].Type == JTokenType.String)
            {
                return JsonConvert.DeserializeObject<MultiInput>(json, VonageSerialization.SerializerSettings);
            }

            return JsonConvert.DeserializeObject<MultiInput>(json, VonageSerialization.SerializerSettings);
        }

        if (data.Property("recording_url") != null)
        {
            return JsonConvert.DeserializeObject<Record>(json, VonageSerialization.SerializerSettings);
        }

        if (data.Property("reason") != null)
        {
            return JsonConvert.DeserializeObject<Error>(json, VonageSerialization.SerializerSettings);
        }

        return null;
    }

    private static EventBase DeserializeStatus(string json, JProperty statusProperty)
    {
        var status = ((string) statusProperty.Value).ToLower();
        switch (status)
        {
            case "started":
                return JsonConvert.DeserializeObject<Started>(json, VonageSerialization.SerializerSettings);
            case "disconnected":
                return JsonConvert.DeserializeObject<Disconnected>(json, VonageSerialization.SerializerSettings);
            case "ringing":
                return JsonConvert.DeserializeObject<Ringing>(json, VonageSerialization.SerializerSettings);
            case "answered":
                return JsonConvert.DeserializeObject<Answered>(json, VonageSerialization.SerializerSettings);
            case "busy":
                return JsonConvert.DeserializeObject<Busy>(json, VonageSerialization.SerializerSettings);
            case "cancelled":
                return JsonConvert.DeserializeObject<Cancelled>(json, VonageSerialization.SerializerSettings);
            case "unanswered":
                return JsonConvert.DeserializeObject<Unanswered>(json, VonageSerialization.SerializerSettings);
            case "rejected":
                return JsonConvert.DeserializeObject<Rejected>(json, VonageSerialization.SerializerSettings);
            case "failed":
                return JsonConvert.DeserializeObject<Failed>(json, VonageSerialization.SerializerSettings);
            case "human":
                return JsonConvert.DeserializeObject<HumanMachine>(json, VonageSerialization.SerializerSettings);
            case "machine":
                return JsonConvert.DeserializeObject<HumanMachine>(json, VonageSerialization.SerializerSettings);
            case "timeout":
                return JsonConvert.DeserializeObject<Timeout>(json, VonageSerialization.SerializerSettings);
            case "completed":
                return JsonConvert.DeserializeObject<Completed>(json, VonageSerialization.SerializerSettings);
            case "transcribed":
                return JsonConvert.DeserializeObject<TranscriptionWebhook>(json, VonageSerialization.SerializerSettings);
        }

        return null;
    }
}