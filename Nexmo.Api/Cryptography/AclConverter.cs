using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Cryptography 
{
    public class AclConverter : JsonConverter
    {
        const string FORMATTED_NAME = @"/{0}/{1}/{2}";
        public override bool CanRead => false;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Acl);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Converter does not read ACL JSON");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            const string FILTERS = "filters";
            const string METHODS = "methods";
            writer.WriteStartObject();
            ACLs acls = (ACLs)value;
            writer.WritePropertyName("paths");
            writer.WriteStartObject();
            foreach(var acl in acls.Paths)
            {
                var depth = "**";
                if (acl.AllowedDepth == Acl.PathDepth.NoDepth)
                {
                    depth = "";
                }
                else if (acl.AllowedDepth == Acl.PathDepth.FirstChild)
                {
                    depth = $"/{acl.FirstDepthAllowance}/";
                }
                else if (acl.AllowedDepth == Acl.PathDepth.FullDepth)
                {
                    if(acl.FirstDepthAllowance == "*" && acl.SecondDepthAllowance == "*")
                    {
                        depth = @"/**";
                    }
                    else
                    {
                        depth = $"/{acl.FirstDepthAllowance}/{acl.SecondDepthAllowance}";
                    }
                }
                
                writer.WritePropertyName($"/{acl.ApiVersion}/{acl.Service}{depth}");
                writer.WriteStartObject();

                foreach (var prop in acl.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (prop.Name == FILTERS)
                    {
                        if (prop.GetValue(acl) != null)
                        {
                            writer.WritePropertyName(FILTERS);
                            serializer.Serialize(writer, prop.GetValue(acl));
                        }
                    }
                }
                if (acl.BlockedHttpVerbs.Count > 0)
                {
                    var unblockedVerbs = Enum.GetValues(typeof(Acl.Verbs)).Cast<Acl.Verbs>();
                    foreach (var verb in acl.BlockedHttpVerbs)
                    {
                        unblockedVerbs = unblockedVerbs.Where(v => v != verb);
                    }
                    writer.WritePropertyName(METHODS);
                    writer.WriteStartArray();
                    foreach (var verb in unblockedVerbs)
                    {
                        writer.WriteValue(verb.ToString());
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
            writer.WriteEndObject();

        }
    }
}
