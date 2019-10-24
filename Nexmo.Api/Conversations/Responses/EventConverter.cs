using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nexmo.Api.Conversations
{
    public class EventConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(EventBase));
        }

        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType == JsonToken.Null)
            {
                return new GetCustomEventResponse();
            }
            else
            {
                JObject obj = JObject.Load(reader);
                if (obj["type"].ToString() == "text")
                {
                    return serializer.Deserialize(reader, typeof(GetTextEventResponse));
                }
                else if (obj["type"].ToString().Split(':')[0] == "custom")
                {
                    return serializer.Deserialize(reader, typeof(GetCustomEventResponse));
                }
                else if (obj["type"].ToString().Split(':')[0] == "member")
                {
                    return serializer.Deserialize(reader, typeof(GetMemberEventResponse));
                }
                else
                {
                    return new GetCustomEventResponse();
                }
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
