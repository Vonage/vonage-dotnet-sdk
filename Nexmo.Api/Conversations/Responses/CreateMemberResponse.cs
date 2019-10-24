using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Conversations
{
    public class CreateMemberResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timestamp")]
        public MemberTimestamp Timestamp { get; set; }

        [JsonProperty("channel")]
        public MemberChannel Channel { get; set; }

        [JsonProperty("initiator")]
        public MemberInitiator Initiator { get; set; }

        [JsonProperty("media")]
        public MemberMedia Media { get; set; }

        public class MemberTimestamp
        {
            [JsonProperty("invited")]
            public string Invited { get; set; }

            [JsonProperty("joined")]
            public string Joined { get; set; }

            [JsonProperty("left")]
            public string Left { get; set; }
        }

        public class MemberChannel
        {
            public enum MemberChannelType
            {
                app = 1
            }

            [JsonProperty("type")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MemberChannelType Type { get; set; }
        }

        public class MemberInitiator
        {
            [JsonProperty("invited")]
            public InitiatorObject Invited { get; set; }

            [JsonProperty("joined")]
            public InitiatorObject Joined { get; set; }

            public class InitiatorObject
            {
                [JsonProperty("is_system")]
                public bool IsSystem { get; set; }
            }
        }

        public class MemberMedia
        {
            [JsonProperty("audio_settings")]
            public MediaAudioSettings AudioSettings { get; set; }
            public class MediaAudioSettings
            {
                [JsonProperty("enabled")]
                public bool Enabled { get; set; }

                [JsonProperty("earmuffed")]
                public bool Earmuffed { get; set; }

                [JsonProperty("muted")]
                public bool Muted { get; set; }
            }
        }
    }
}
