using Newtonsoft.Json;

namespace Vonage.Verify;

/// <summary>
///     Represents a PSD2-compliant verification request for Strong Customer Authentication (SCA), displaying the payment recipient and amount to the user.
/// </summary>
public class Psd2Request : VerifyRequestBase
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
    ///     The name of the payment recipient displayed to the user in the verification message (e.g., "Acme Corp").
    /// </summary>
    [JsonProperty("payee")]
    public string Payee { get; set; }

    /// <summary>
    ///     The payment amount to be confirmed, in Euros. Displayed to the user in the verification message.
    /// </summary>
    [JsonProperty("amount")]
    public double? Amount { get; set; }

    /// <summary>
    ///     The delivery workflow that defines the sequence of SMS and voice calls used to deliver the verification code. Defaults to <see cref="Workflow.SMS_TTS_TTS"/> if not specified.
    /// </summary>
    [JsonProperty("workflow_id")]
    public Workflow? WorkflowId { get; set; }
}