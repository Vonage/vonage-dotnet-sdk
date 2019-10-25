using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexmo.Api.Conversations
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("custom_data")]
        public JObject CustomData { get; set; }

        [JsonProperty("_links")]
        public SingleRecordLink Links { get; set; }

        public T GetCustomDataAsType<T>()
        {
            return JsonConvert.DeserializeObject<T>(CustomData.ToString());
        }
    }
}
