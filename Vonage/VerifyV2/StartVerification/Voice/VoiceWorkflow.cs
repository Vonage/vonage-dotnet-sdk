#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.Voice;

/// <summary>
///     Represents a verification workflow that delivers the PIN code via text-to-speech voice call.
/// </summary>
public readonly struct VoiceWorkflow : IVerificationWorkflow
{
    private VoiceWorkflow(PhoneNumber to) => this.To = to;

    /// <summary>
    ///     The recipient phone number in E.164 format without leading + or 00 (e.g., "447700900000").
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.Voice.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new Voice verification workflow.
    /// </summary>
    /// <param name="to">The recipient phone number in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = VoiceWorkflow.Parse("447700900000");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<VoiceWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new VoiceWorkflow(phoneNumber));
}