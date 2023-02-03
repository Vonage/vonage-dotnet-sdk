using System.Collections.Generic;
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common.Serialization;
using Vonage.Meetings.Common;

namespace Vonage.Meetings.UpdateThemeLogo;

/// <summary>
/// </summary>
public struct UploadDetails
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("X-Amz-Algorithm")]
    public string AmazonAlgorithm { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("X-Amz-Credential")]
    public string AmazonCredential { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("X-Amz-Date")]
    public string AmazonDate { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("X-Amz-Security-Token")]
    public string AmazonSecurityToken { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("X-Amz-Signature")]
    public string AmazonSignature { get; set; }

    /// <summary>
    ///     Bucket name to upload to.
    /// </summary>
    public string Bucket { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("Content-Type")]
    public string ContentType { get; set; }

    /// <summary>
    ///     Logo's key in storage system.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("logoType")]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ThemeLogoType>))]
    public ThemeLogoType LogoType { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("Policy")]
    public string Policy { get; set; }

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