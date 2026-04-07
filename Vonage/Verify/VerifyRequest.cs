using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents a request to send a verification PIN code to a phone number via SMS or voice call.
/// </summary>
public class VerifyRequest : VerifyRequestBase
{
    /// <summary>
    ///     Defines the sequence of delivery methods used to send the verification code to the user.
    /// </summary>
    public enum Workflow
    {
        /// <summary>
        ///     First attempt via SMS, then two text-to-speech voice calls if unverified.
        /// </summary>
        SMS_TTS_TTS = 1,

        /// <summary>
        ///     First two attempts via SMS, then a text-to-speech voice call if unverified.
        /// </summary>
        SMS_SMS_TTS = 2,

        /// <summary>
        ///     Two text-to-speech voice calls only (no SMS).
        /// </summary>
        TTS_TTS = 3,

        /// <summary>
        ///     Two SMS attempts only (no voice calls).
        /// </summary>
        SMS_SMS = 4,

        /// <summary>
        ///     First attempt via SMS, then a text-to-speech voice call if unverified.
        /// </summary>
        SMS_TTS = 5,

        /// <summary>
        ///     Single SMS attempt only.
        /// </summary>
        SMS = 6,

        /// <summary>
        ///     Single text-to-speech voice call only.
        /// </summary>
        TTS = 7,
    }

    /// <summary>
    ///     Your company or application name displayed in the verification message. Limited to 18 alphanumeric characters. For example: "Your Acme Inc PIN is ...".
    /// </summary>
    [JsonProperty("brand")]
    public string Brand { get; set; }

    /// <summary>
    ///     The alphanumeric sender ID for the SMS message. Limited to 11 characters. Country-specific restrictions may apply.
    /// </summary>
    [JsonProperty("sender_id")]
    public string SenderId { get; set; }

    /// <summary>
    ///     The delivery workflow that defines the sequence of SMS and voice calls used to deliver the verification code. Defaults to <see cref="Workflow.SMS_TTS_TTS"/> if not specified.
    /// </summary>
    [JsonProperty("workflow_id")]
    public Workflow? WorkflowId { get; set; }
}