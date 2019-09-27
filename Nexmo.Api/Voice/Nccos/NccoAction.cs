using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Voice.Nccos
{
    public abstract class NccoAction
    {
        public enum ActionType
        {
            unknown,
            record,
            conversation,
            connect,
            talk,
            stream,
            input,
            notify
        }

        [JsonProperty("action", Order = -1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActionType Action { get; protected set; }

        public override string ToString()
        {
            var JsonSerializerSettings = new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore

                };
            return JsonConvert.SerializeObject(this, JsonSerializerSettings);
        }
    }
}
