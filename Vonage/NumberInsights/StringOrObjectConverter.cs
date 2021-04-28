using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vonage.NumberInsights
{
    class StringOrObjectConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Roaming));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Path.ToString() == "roaming" && reader.Value == null)
            {
                if (reader.TokenType == JsonToken.StartObject) {
                    var obj = JObject.Load(reader);

                    RoamingStatus status;
                    if(!Enum.TryParse(obj["status"].ToString(), out status))
                    {
                        status = RoamingStatus.unknown;
                    }

                    return new Roaming {
                        Status = status,
                        RoamingCountryCode = obj["roaming_country_code"].ToString(),
                        RoamingNetworkCode = obj["roaming_network_code"].ToString(),
                        RoamingNetworkName = obj["roaming_network_name"].ToString()
                    };
                }

                return null;
            }

            if (reader.Path.ToString() == "roaming" && reader.Value.ToString() == RoamingStatus.unknown.ToString())
            {
                return new Roaming
                {
                    Status = RoamingStatus.unknown
                };
            }

            throw new FormatException("Invalid Number Insights Roaming data detected");
        }

        public override bool CanWrite => base.CanWrite;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}