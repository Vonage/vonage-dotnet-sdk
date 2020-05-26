using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Nexmo.Api.Voice.EventWebhooks
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

            var statusProperty = data.Property("status");

            var typePropety = data.Property("type");

            var dtmfProperty = data.Property("dtmf");

            var recordingUrlProperty = data.Property("recording_url");

            var conversationUuidFromProperty = data.Property("conversation_uuid_from");
            
            var reasonProperty = data.Property("reason");
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            if (statusProperty != null)
            {
                var status = ((string)statusProperty.Value).ToLower();
                if (status == "started")
                {
                    return JsonConvert.DeserializeObject<Started>(json, settings);
                }
                else if (status == "disconnected")
                {
                    return JsonConvert.DeserializeObject<Disconnected>(json, settings);
                }
                else if (status == "ringing")
                {
                    return JsonConvert.DeserializeObject<Ringing>(json, settings);
                }
                else if (status == "answered")
                {
                    return JsonConvert.DeserializeObject<Answered>(json, settings);
                }
                else if (status == "busy")
                {
                    return JsonConvert.DeserializeObject<Busy>(json, settings);
                }
                else if (status == "cancelled")
                {
                    return JsonConvert.DeserializeObject<Cancelled>(json, settings);
                }
                else if (status == "unanswered")
                {
                    return JsonConvert.DeserializeObject<Unanswered>(json, settings);
                }
                else if (status == "rejected")
                {
                    return JsonConvert.DeserializeObject<Rejected>(json, settings);
                }
                else if (status == "failed")
                {
                    return JsonConvert.DeserializeObject<Failed>(json, settings);
                }
                else if (status == "human")
                {
                    return JsonConvert.DeserializeObject<HumanMachine>(json, settings);
                }
                else if (status == "machine")
                {
                    return JsonConvert.DeserializeObject<HumanMachine>(json, settings);
                }
                else if (status == "timeout")
                {
                    return JsonConvert.DeserializeObject<Timeout>(json, settings);
                }
                else if (status == "completed")
                {
                    return JsonConvert.DeserializeObject<Completed>(json, settings);
                }
            }
            else if (conversationUuidFromProperty != null)
            {
                return JsonConvert.DeserializeObject<Transfer>(json, settings);
            }
            else if (dtmfProperty != null)
            {
                return JsonConvert.DeserializeObject<Input>(json, settings);
            }
            else if (recordingUrlProperty != null)
            {
                return JsonConvert.DeserializeObject<Record>(json, settings);
            }
            else if (reasonProperty != null)
            {
                return JsonConvert.DeserializeObject<Error>(json, settings);
            }
            return null;
        }
    }
}
