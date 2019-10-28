using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexmo.Api.Conversations
{
    public class Properties
    {
        public Properties(object data)
        {
            CustomData = JObject.FromObject(data);
        }
        public Properties()
        {

        }

        [JsonProperty("custom_data")]
        public JObject CustomData { get; set; }

        public T GetCustomDataAsType<T>()
        {
            return JsonConvert.DeserializeObject<T>(CustomData.ToString());
        }
    }
}
