using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Nexmo.Api.Voice
{
    public class CallCommandConverter : JsonConverter
    {
        public override bool CanWrite => true;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Call.CallCommand);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            const string NCCO = "ncco";
            const string NCCO_OBJ = "NccoObj";
            writer.WriteStartObject();
            var nccoUsed = false;
            foreach(var property in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if(property.GetValue(value) == null || property.GetValue(value) is decimal && (decimal)property.GetValue(value) == (decimal)0.0)
                {
                    continue;
                }
                var propertyName = property.Name;
                foreach(CustomAttributeData att in property.CustomAttributes)
                {
                    if(att.AttributeType.Name == "JsonPropertyAttribute")
                    {
                        propertyName = att.ConstructorArguments[0].Value.ToString();
                        break;
                    }
                }
                if ((propertyName == NCCO  || propertyName == NCCO_OBJ) && nccoUsed)
                {
                    continue;
                }
                else if(propertyName == NCCO || propertyName == NCCO_OBJ)
                {
                    nccoUsed = true;
                    propertyName = "ncco";
                }
                writer.WritePropertyName(propertyName);
                serializer.Serialize(writer, property.GetValue(value));
            }
            writer.WriteEndObject();
        }
    }
}
