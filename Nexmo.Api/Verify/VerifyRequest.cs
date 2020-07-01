using Newtonsoft.Json;

namespace Nexmo.Api.Verify
{
    public class VerifyRequest : VerifyRequestBase
    {
        public enum Workflow
        {
            SMS_TTS_TTS=1,
            SMS_SMS_TTS=1,
            TTS_TTS=3,
            SMS_SMS=4,
            SMS_TTS=5,
            SMS=6,
            TTS=7
        }

        /// <summary>
        /// An 18-character alphanumeric string you can use to personalize the verification request SMS body, to help users identify your company or application name. For example: "Your Acme Inc PIN is ..."
        /// </summary>
        [JsonProperty("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// An 11-character alphanumeric string that represents the identity of the sender of the verification request: https://developer.nexmo.com/messaging/sms/guides/custom-sender-id. 
        /// Depending on the destination of the phone number you are sending the verification SMS to, restrictions might apply.
        /// </summary>
        [JsonProperty("sender_id")]
        public string SenderId { get; set; }

        /// <summary>
        /// Selects the predefined sequence of SMS and TTS (Text To Speech) actions to use in order to convey the PIN to your user. 
        /// For example, an id of 1 identifies the workflow SMS - TTS - TTS. 
        /// For a list of all workflows and their associated ids, please visit the developer portal.
        /// </summary>
        [JsonProperty("workflow_id")]
        public Workflow? WorkflowId { get; set; }

    }
}