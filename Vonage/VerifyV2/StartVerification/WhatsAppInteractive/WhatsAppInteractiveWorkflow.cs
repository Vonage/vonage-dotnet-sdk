#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.WhatsAppInteractive;

/// <summary>
///     Represents a verification workflow that delivers the PIN code via WhatsApp interactive message with a one-tap button for automatic code submission.
/// </summary>
public readonly struct WhatsAppInteractiveWorkflow : IVerificationWorkflow
{
    private WhatsAppInteractiveWorkflow(PhoneNumber to) => this.To = to;

    /// <summary>
    ///     The recipient WhatsApp phone number in E.164 format without leading + or 00 (e.g., "447700900000").
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.WhatsAppInteractive.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new WhatsApp Interactive verification workflow.
    /// </summary>
    /// <param name="to">The recipient WhatsApp phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = WhatsAppInteractiveWorkflow.Parse("447700900000");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<WhatsAppInteractiveWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new WhatsAppInteractiveWorkflow(phoneNumber));
}