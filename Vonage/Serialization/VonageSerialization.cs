using Newtonsoft.Json;

namespace Vonage.Serialization;

/// <summary>
/// Exposes serialization settings
/// </summary>
public static class VonageSerialization
{
    /// <summary>
    /// The custom serialization settings.
    /// </summary>
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