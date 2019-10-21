using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Nexmo.Api.Voice.Nccos
{
    public class NccoConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanWrite => true;
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Ncco))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unecessary as CanRead is set to false");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {            
            var ncco = (Ncco)value;

            writer.WriteStartArray();
            foreach (var action in ncco.Actions)
            {
                writer.WriteStartObject();
                var propertiers = action.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in propertiers)
                {
                    if (prop.GetValue(action) == null)
                    {
                        continue;
                    }
                    var propertyName = prop.Name;
                    foreach (CustomAttributeData att in prop.CustomAttributes)
                    {
                        if (att.AttributeType.Name == "JsonPropertyAttribute")
                        {
                            propertyName = att.ConstructorArguments[0].Value.ToString();
                            break;
                        }
                    }
                    writer.WritePropertyName(propertyName);
                    writer.WriteValue(prop.GetValue(action).ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
