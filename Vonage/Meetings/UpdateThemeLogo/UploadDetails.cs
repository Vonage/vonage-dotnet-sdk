using System.Collections.Generic;
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateThemeLogo;

internal struct UploadDetails
{
    [JsonPropertyName("X-Amz-Algorithm")] public string AmazonAlgorithm { get; set; }

    [JsonPropertyName("X-Amz-Credential")] public string AmazonCredential { get; set; }

    [JsonPropertyName("X-Amz-Date")] public string AmazonDate { get; set; }

    [JsonPropertyName("X-Amz-Security-Token")]
    public string AmazonSecurityToken { get; set; }

    [JsonPropertyName("X-Amz-Signature")] public string AmazonSignature { get; set; }

    public string Bucket { get; set; }

    [JsonPropertyName("Content-Type")] public string ContentType { get; set; }

    public string Key { get; set; }

    [JsonPropertyName("logoType")]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ThemeLogoType>))]
    public ThemeLogoType LogoType { get; set; }

    [JsonPropertyName("Policy")] public string Policy { get; set; }

    /// <summary>
    ///     Converts data to a dictionary.
    /// </summary>
    /// <returns>The data dictionary.</returns>
    public Dictionary<string, string> ToDictionary() =>
        new()
        {
            {"Content-Type", this.ContentType},
            {"key", this.Key},
            {"logoType", this.LogoType.AsString(EnumFormat.Description)},
            {"bucket", this.Bucket},
            {"X-Amz-Algorithm", this.AmazonAlgorithm},
            {"X-Amz-Credential", this.AmazonCredential},
            {"X-Amz-Date", this.AmazonDate},
            {"X-Amz-Security-Token", this.AmazonSecurityToken},
            {"Policy", this.Policy},
            {"X-Amz-Signature", this.AmazonSignature},
        };
}