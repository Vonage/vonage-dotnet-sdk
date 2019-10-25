using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexmo.Api.Conversations
{
    public class Properties
    {
        [JsonProperty("custom_data")]
        public JObject CustomData { get; set; }

        public T GetCustomDataAsType<T>()
        {
            return JsonConvert.DeserializeObject<T>(CustomData.ToString());
        }
    }

    public class Properties<T>
    {
        [JsonProperty("custom_data")]
        public T CustomData { get; set; }
    }
}
