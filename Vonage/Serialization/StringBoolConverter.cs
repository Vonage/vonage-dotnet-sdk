using System;
using Newtonsoft.Json;

namespace Vonage.Serialization
{
    internal class StringBoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            
            if(bool.TryParse(value.ToString(), out var boolValue))
            {
                writer.WriteValue(boolValue ? "true" : "false");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize<bool>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}