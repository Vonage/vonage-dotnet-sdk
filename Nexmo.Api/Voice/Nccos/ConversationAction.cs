using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    public class ConversationAction : NccoAction
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("musicOnHoldUrl")]
        public string[] MusicOnHoldUrl { get; set; }

        [JsonProperty("startOnEnter")]
        public string StartOnEnter { get; set; }

        [JsonProperty("endOnExit")]
        public string EndOnExit { get; set; }

        [JsonProperty("record")]
        public string Record { get; set; }

        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        [JsonProperty("canSpeak")]
        public string[] CanSpeak { get; set; }

        [JsonProperty("canHear")]
        public string[] CanHear { get; set; }

        public ConversationAction()
        {
            Action = ActionType.conversation;
        }
    }
}
