using Newtonsoft.Json;

namespace Vonage.Serialization
{
    internal class VonageSerialization
    {
        internal static JsonSerializerSettings SerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Formatting = Formatting.None,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return settings;
            }
        }
    }
}
