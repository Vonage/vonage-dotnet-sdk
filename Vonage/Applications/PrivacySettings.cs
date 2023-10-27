using Newtonsoft.Json;

namespace Vonage.Applications;

/// <summary>
///     Application privacy config
/// </summary>
/// <param name="ImproveAi">
///     If set to true, Vonage may store and use your content and data for the improvement of Vonage's
///     AI based services and technologies.
/// </param>
public record PrivacySettings([property: JsonProperty("improve_ai")] bool ImproveAi);