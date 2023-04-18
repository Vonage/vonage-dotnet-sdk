using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Vonage.Voice;

public class CallCommandConverter : JsonConverter
{
    public override bool CanWrite => true;

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(CallCommand);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        const string NCCO = "ncco";
        const string NCCO_OBJ = "NccoObj";
        const string PROPERTY_NAME = "PropertyName";
        const string NCCO_PASCAL = "Ncco";
        writer.WriteStartObject();
        var nccoUsed = false;
        foreach (var property in value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.GetValue(value) == null || property.GetValue(value) is decimal &&
                (decimal) property.GetValue(value) == (decimal) 0.0)
            {
                continue;
            }

            var propertyName = property.Name;
            foreach (var att in property.CustomAttributes)
            {
                if (att.AttributeType.Name == "JsonPropertyAttribute")
                {
                    if (att.ConstructorArguments.Count > 0)
                        propertyName = att.ConstructorArguments[0].Value.ToString();
                    else
                        propertyName = (string) att.NamedArguments.First(x => x.MemberName == PROPERTY_NAME).TypedValue
                            .Value;
                    break;
                }
            }

            if ((propertyName == NCCO || propertyName == NCCO_OBJ || propertyName == NCCO_PASCAL) && nccoUsed)
            {
                continue;
            }

            if (propertyName == NCCO || propertyName == NCCO_OBJ || propertyName == NCCO_PASCAL)
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