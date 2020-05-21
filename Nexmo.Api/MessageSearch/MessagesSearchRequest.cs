using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class MessagesSearchRequest 
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }


        [JsonProperty("ids")]
        private string _ids;

        [JsonIgnore]
        public string[] Ids { 
            get 
            {
                return _ids?.Split(new[] { "&ids", "ids" }, System.StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            } 
            set 
            {
                var sb = new System.Text.StringBuilder();
                foreach(var s in value)
                {
                    sb.Append($"={s}&ids");
                }                
                _ids = sb.ToString().Substring(1,sb.Length-5);
            } 
        }
    }
}