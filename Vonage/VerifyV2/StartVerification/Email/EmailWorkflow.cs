using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.VerifyV2.StartVerification.Email;

/// <summary>
///     Represents a verification workflow for Email.
/// </summary>
public readonly struct EmailWorkflow : IVerificationWorkflow
{
    private EmailWorkflow(MailAddress to, Maybe<MailAddress> from)
    {
        this.To = to;
        this.From = from;
    }

    /// <inheritdoc />
    [JsonPropertyOrder(0)]
    public string Channel => "email";

    /// <summary>
    ///     The email address to send the verification request from.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<MailAddress>))]
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<MailAddress> From { get; }

    /// <summary>
    ///     The email address to send the verification request to.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(EmailJsonConverter))]
    public MailAddress To { get; }

    /// <summary>
    ///     Parses the input into a EmailWorkflow.
    /// </summary>
    /// <param name="to">The email address to send the verification request to.</param>
    /// <returns>Success or failure.</returns>
    public static Result<EmailWorkflow> Parse(string to) =>
        MailAddress.Parse(to).Map(phoneNumber => new EmailWorkflow(phoneNumber, Maybe<MailAddress>.None));

    /// <summary>
    ///     Parses the input into a EmailWorkflow.
    /// </summary>
    /// <param name="to">The email address to send the verification request to.</param>
    /// <param name="from">The email address to send the verification request from.</param>
    /// <returns>Success or failure.</returns>
    public static Result<EmailWorkflow> Parse(string to, string from) =>
        MailAddress.Parse(to)
            .Merge(MailAddress.Parse(from), (toNumber, fromNumber) => new EmailWorkflow(toNumber, fromNumber));

    /// <inheritdoc />
    public string Serialize(IJsonSerializer serializer) => serializer.SerializeObject(this);
}