#region
using System.Text.Json.Serialization;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.VerifyV2.StartVerification.Email;

/// <summary>
///     Represents a verification workflow that delivers the PIN code via email.
/// </summary>
public readonly struct EmailWorkflow : IVerificationWorkflow
{
    private EmailWorkflow(MailAddress to, Maybe<MailAddress> from)
    {
        this.To = to;
        this.From = from;
    }

    /// <summary>
    ///     The sender email address. If not specified, a default Vonage sender address is used.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<MailAddress>))]
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<MailAddress> From { get; }

    /// <summary>
    ///     The recipient email address where the verification code will be sent.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EmailJsonConverter))]
    public MailAddress To { get; }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => VerificationChannel.Email.AsString(EnumFormat.Description);

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);

    /// <summary>
    ///     Creates a new Email verification workflow with a default sender address.
    /// </summary>
    /// <param name="to">The recipient email address (e.g., "user@example.com").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if the email is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = EmailWorkflow.Parse("user@example.com");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<EmailWorkflow> Parse(string to) =>
        MailAddress.Parse(to).Map(phoneNumber => new EmailWorkflow(phoneNumber, Maybe<MailAddress>.None));

    /// <summary>
    ///     Creates a new Email verification workflow with a custom sender address.
    /// </summary>
    /// <param name="to">The recipient email address (e.g., "user@example.com").</param>
    /// <param name="from">The sender email address (e.g., "noreply@yourcompany.com").</param>
    /// <returns>A <see cref="Result{T}"/> containing the workflow if successful, or validation errors if any email is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var workflow = EmailWorkflow.Parse("user@example.com", "noreply@yourcompany.com");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/VerifyV2">More examples in the snippets repository</seealso>
    public static Result<EmailWorkflow> Parse(string to, string from) =>
        MailAddress.Parse(to)
            .Merge(MailAddress.Parse(from), (toNumber, fromNumber) => new EmailWorkflow(toNumber, fromNumber));
}