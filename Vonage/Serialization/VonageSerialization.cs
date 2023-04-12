using Newtonsoft.Json;

namespace Vonage.Serialization;

public class VonageSerialization
{
    public static JsonSerializerSettings SerializerSettings
    {
        get
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

            return settings;
        }
    }
}