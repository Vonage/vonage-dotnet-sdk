#region
using System.ComponentModel;
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.WhatsApp;

/// <summary>
///     Represents a verification workflow that delivers the PIN code via WhatsApp message. Requires a WhatsApp Business Account (WABA) connected sender number.
/// </summary>
public readonly struct WhatsAppWorkflow : IVerificationWorkflow
{
    private WhatsAppWorkflow(PhoneNumber to, PhoneNumber from, WhatsAppMode mode)
    {
        this.To = to;
        this.From = from;
        this.Mode = mode;
    }

    /// <summary>
    ///     The WABA (WhatsApp Business Account) connected sender number in E.164 format without leading + or 00 (e.g., "447700400080").
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber From { get; }

    /// <summary>
    ///     The recipient WhatsApp phone number in E.164 format without leading + or 00 (e.g., "447700900000").
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.WhatsApp.AsString(EnumFormat.Description);

    /// <summary>
    /// Defines the WhatsApp verification experience. Use zero_tap for automatic verification on Android apps. Defaults to otp_code.
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<WhatsAppMode>))]
    public WhatsAppMode Mode { get; }

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new WhatsApp verification workflow with OTP Code.
    /// </summary>
    /// <param name="to">The recipient WhatsApp phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <param name="from">The WABA connected sender number in E.164 format without leading + or 00 (e.g., "447700400080").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if any phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = WhatsAppWorkflow.Parse("447700900000", "447700400080");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<WhatsAppWorkflow> Parse(string to, string from) =>
        PhoneNumber.Parse(to).Merge(PhoneNumber.Parse(from),
            (toNumber, fromNumber) => new WhatsAppWorkflow(toNumber, fromNumber, WhatsAppMode.OptCode));
    
    /// <summary>
    ///     Creates a new WhatsApp verification workflow with zero_tap for automatic verification on android apps.
    /// </summary>
    /// <param name="to">The recipient WhatsApp phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <param name="from">The WABA connected sender number in E.164 format without leading + or 00 (e.g., "447700400080").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if any phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = WhatsAppWorkflow.ParseWithZeroTap("447700900000", "447700400080");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<WhatsAppWorkflow> ParseWithZeroTap(string to, string from) =>
        PhoneNumber.Parse(to).Merge(PhoneNumber.Parse(from),
            (toNumber, fromNumber) => new WhatsAppWorkflow(toNumber, fromNumber, WhatsAppMode.ZeroTap));

    /// <summary>
    /// Defines the WhatsApp verification experience. Use zero_tap for automatic verification on Android apps.
    /// </summary>
    public enum WhatsAppMode
    {
        /// <summary>
        /// Default WhatsApp verification experience.
        /// </summary>
        [Description("otp_code")]
        OptCode,
        /// <summary>
        /// Automatic verification on Android apps.
        /// </summary>
        [Description("zero_tap")]
        ZeroTap,
    }
}