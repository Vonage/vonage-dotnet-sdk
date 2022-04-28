using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Vonage.Serialization;

namespace Vonage.Voice.EventWebhooks
{
    public class EventBase
    {
        /// <summary>
        /// Timestamp (ISO 8601 format)
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }

        public static EventBase ParseEvent(string json)
        {
            var data = (JObject)JsonConvert.DeserializeObject(json);

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
                    return JsonConvert.DeserializeObject<Input>(json, VonageSerialization.SerializerSettings);
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
            var status = ((string)statusProperty.Value).ToLower();
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
            }

            return null;
        }
    }
}
