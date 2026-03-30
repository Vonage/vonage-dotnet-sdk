#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Represents the cryptographic keys for an application, used for JWT authentication.
/// </summary>
public class Keys
{
    /// <summary>
    ///     The public key in PEM format. Used to verify JWTs signed with the private key.
    /// </summary>
    [JsonProperty("public_key")]
    public string PublicKey { get; set; }

    /// <summary>
    ///     The private key in PEM format. Only returned when creating a new application.
    ///     Store this securely as it cannot be retrieved again.
    /// </summary>
    [JsonProperty("private_key")]
    public string PrivateKey { get; set; }
}