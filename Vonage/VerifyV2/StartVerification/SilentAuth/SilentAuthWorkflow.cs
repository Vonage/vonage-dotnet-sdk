#region
using System;
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a verification workflow that uses Silent Authentication to verify a user's phone number without requiring them to enter a PIN code. The device must be connected via cellular data for this to work.
/// </summary>
public readonly struct SilentAuthWorkflow : IVerificationWorkflow
{
    private SilentAuthWorkflow(PhoneNumber to, Uri redirectUrl = null)
    {
        this.To = to;
        this.RedirectUrl = redirectUrl ?? Maybe<Uri>.None;
    }

    /// <summary>
    ///     An optional final redirect URL appended to the end of the check_url request/response lifecycle. The request_id and code will be included as URL fragment parameters (e.g., https://example.com/callback#request_id=xxx&amp;code=yyy).
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> RedirectUrl { get; }

    /// <summary>
    ///     The phone number to authenticate in E.164 format without leading + or 00 (e.g., "447700900000"). This must match the SIM card in the device making the cellular request.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(PhoneNumberJsonConverter))]
    public PhoneNumber To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.SilentAuth.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new Silent Authentication workflow without a redirect URL.
    /// </summary>
    /// <param name="to">The phone number to authenticate in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = SilentAuthWorkflow.Parse("447700900000");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<SilentAuthWorkflow> Parse(string to) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber));

    /// <summary>
    ///     Creates a new Silent Authentication workflow with a custom redirect URL for client SDK integrations.
    /// </summary>
    /// <param name="to">The phone number to authenticate in E.164 format without leading + or 00 (e.g., "447700900000").</param>
    /// <param name="redirectUrl">The URL to redirect to after Silent Auth completion. The request_id and code are appended as URL fragments.</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the phone number is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = SilentAuthWorkflow.Parse("447700900000", new Uri("https://example.com/callback"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<SilentAuthWorkflow> Parse(string to, Uri redirectUrl) =>
        PhoneNumber.Parse(to).Map(phoneNumber => new SilentAuthWorkflow(phoneNumber, redirectUrl));
}