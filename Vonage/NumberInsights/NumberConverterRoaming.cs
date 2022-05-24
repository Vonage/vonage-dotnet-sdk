using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vonage.NumberInsights
{
    internal class NumberRoamingConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Roaming);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Path == "roaming" && reader.Value == null)
            {
                if (reader.TokenType == JsonToken.StartObject) {
                    var obj = JObject.Load(reader);

                    RoamingStatus status = obj["status"].ToString().ToEnum<RoamingStatus>();
                    
                    return new Roaming {
                        Status = status,
                        RoamingCountryCode = obj["roaming_country_code"]?.ToString(),
                        RoamingNetworkCode = obj["roaming_network_code"]?.ToString(),
                        RoamingNetworkName = obj["roaming_network_name"]?.ToString()
                    };
                }

                return null;
            }

            if (reader.Path == "roaming" && reader.Value.ToString() == "unknown")
            {
                return new Roaming
                {
                    Status = RoamingStatus.Unknown
                };
            }

            throw new FormatException("Invalid Number Insights Roaming data detected");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}