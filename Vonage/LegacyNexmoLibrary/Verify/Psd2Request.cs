using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    [System.Obsolete("The Nexmo.Api.Verify.Psd2Request class is obsolete. " +
        "References to it should be switched to the new Vonage.Verify.Psd2Request class.")]
    public class Psd2Request : VerifyRequestBase
    {
        public enum Workflow
        {
            SMS_TTS_TTS = 1,
            SMS_SMS_TTS = 1,
            TTS_TTS = 3,
            SMS_SMS = 4,
            SMS_TTS = 5,
            SMS = 6,
            TTS = 7
        }

        /// <summary>
        /// An alphanumeric string to indicate to the user the 
        /// name of the recipient that they are confirming a payment to.
        /// </summary>
        [JsonProperty("payee")]
        public string Payee { get; set; }

        /// <summary>
        /// The Decimal amount of the payment to be confirmed, in Euros
        /// </summary>
        [JsonProperty("amount")]
        public double? Amount { get; set; }

        /// <summary>
        /// Selects the predefined sequence of SMS and TTS (Text To Speech) actions to use in order to convey the PIN to your user. 
        /// For example, an id of 1 identifies the workflow SMS - TTS - TTS. 
        /// For a list of all workflows and their associated ids, please visit the developer portal.
        /// </summary>
        [JsonProperty("workflow_id")]
        public Workflow? WorkflowId { get; set; }
    }
}
