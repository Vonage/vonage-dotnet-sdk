using System;
using System.Net;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class Voice
    {
        public class CallCommand
        {
            /// <summary>
            /// Required. A single phone number in international format, that is E.164. For example, to=447525856424. You can set one recipient only for each request.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Required. A URL pointing to the VoiceXML file on your HTTP server that controls your Call. If the VoiceXML at answer_url triggers a transfer, you receive separate Call Return Parameters for each leg of the call.
            /// </summary>
            public string answer_url { get; set; }
            /// <summary>
            /// Optional. A voice-enabled virtual number associated with your Nexmo account. This number is displayed on your user's handset during this Text-To-Speech or Text-To-Speech Prompt. Check the features and restrictions to see if this is possible in your destination country.
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// Optional. If the Call is picked up by an answering machine, set to:
            ///   true - play your message after the beep. If there is no beep the Call is closed without leaving your message.
            ///   hangup - the Call hangs up immediately.
            /// The value of status in the Call Return Parameters is set to machine when we close the Call.
            /// </summary>
            public string machine_detection { get; set; }
            /// <summary>
            /// Optional. The time in milliseconds used to distinguish between human and machine events. Possible values are from 400ms to 10s. The default value is 10s.
            /// </summary>
            public string machine_timeout { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send a response to your answer_url. Must be GET (default) or POST.
            /// </summary>
            public string answer_method { get; set; }
            /// <summary>
            /// Optional. Send the VoiceXML error message to this URL if there's a problem requesting or executing the VoiceXML referenced in the answer_url
            /// </summary>
            public string error_url { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send an error message to your error_url. Must be GET (default) or POST.
            /// </summary>
            public string error_method { get; set; }
            /// <summary>
            /// Optional. If you set this parameter, Nexmo sends the Call Return Parameters to this Callback URL in order to notify your App that the Call has ended.
            /// </summary>
            public string status_url { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send the status message to your status_url. Must be GET (default) or POST.
            /// </summary>
            public string status_method { get; set; }
        }

        public class CallRequestResponse
        {
            /// <summary>
            /// An alphanumeric unique call identifier of up to 40 characters.
            /// </summary>
            [JsonProperty("call-id")]
            public string CallId { get; set; }
            /// <summary>
            /// The phone number the call was made to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Shows if your request has been sent successfully to Nexmo, or the reason why it could not be processed. See Response codes for more information.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// If status is not 0 this message explains the issue encountered.
            /// </summary>
            [JsonProperty("error-text")]
            public string ErrorText { get; set; }
        }

        public class CallReturn
        {
            /// <summary>
            /// The Nexmo ID for this Call.
            /// </summary>
            [JsonProperty("call-id")]
            public string CallId { get; set; }
            /// <summary>
            /// The phone number this Call was sent to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// One of the following:
            ///   ok - Call terminated normally.
            ///   failed - Call failed.
            ///   error - an error occurred during the Call.
            ///   vxml_error - an error occurred running your VoiceXML script.
            ///   blocked - the Call was blocked.
            ///   machine - Call was stopped because you set machine_detection to hangup.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// Total price charged to your account in EUR.
            /// </summary>
            [JsonProperty("call-price")]
            public string CallPrice { get; set; }
            /// <summary>
            /// Price per minute in EUR.
            /// </summary>
            [JsonProperty("call-rate")]
            public string CallRate { get; set; }
            /// <summary>
            /// Duration of the Call in seconds.
            /// </summary>
            [JsonProperty("call-duration")]
            public string CallDuration { get; set; }
            /// <summary>
            /// The time you sent the Call request. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            [JsonProperty("call-request")]
            public string CallRequest { get; set; }
            /// <summary>
            /// The time the Call started. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            [JsonProperty("call-start")]
            public string CallStart { get; set; }
            /// <summary>
            /// The time the Call ended. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            [JsonProperty("call-end")]
            public string CallEnd { get; set; }
            /// <summary>
            /// The ID of the parent Call if this Call is a transfer.
            /// </summary>
            [JsonProperty("call-parent")]
            public string CallParent { get; set; }
            /// <summary>
            /// One of the following:
            ///   in - an inbound Call to your voice-enabled number.
            ///   out - an outbound Call.
            /// </summary>
            [JsonProperty("call-direction")]
            public string CallDirection { get; set; }
            /// <summary>
            /// The Mobile Country Code Mobile Network Code (MCCMNC) for the carrier network this phone number is registered with.
            /// </summary>
            [JsonProperty("network-code")]
            public string NetworkCode { get; set; }
        }

        public class TextToSpeechCallCommand
        {
            /// <summary>
            /// Required. The single phone number to call for each request. This number must be in international format, that is E.164. For example, when sending to the uk: to=447525856424.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Optional. A voice-enabled virtual number associated with your Nexmo account. This number is displayed on your user's handset during this Text-To-Speech or Text-To-Speech Prompt. Check the features and restrictions to see if this is possible in your destination country.
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// Required. A UTF-8 and URL encoded message that is sent to your user. This message can be up to 1500 characters). Text-To-Speech Messages explains how to control the pacing of your message.
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// Optional. The language used to synthesize the message. Combine this parameter with voice to control the language, accent and gender used to deliver this TTS. The default language is en-us. Note: ensure that lg matches the language you have written in text.
            /// </summary>
            public string lg { get; set; }
            /// <summary>
            /// Optional. The gender of the voice used for the TTS. Possible values are female or male. The default value is female.
            /// </summary>
            public string voice { get; set; }
            /// <summary>
            /// Optional. Define how many times you want to repeat the text message. You can set a message to be repeated up to 10 times. The default is 1.
            /// </summary>
            public string repeat { get; set; }
            /// <summary>
            /// Optional. Control the way this TTS is handled if it is picked up by an answering machine. Set to:
            ///   true - play the message after the beep. If the answer phone does not issue a beep the call is closed.
            ///   hangup - hang up. The value of the status sent in the Text-To-Speech Return Parameters is set to machine.
            /// </summary>
            public string machine_detection { get; set; }
            /// <summary>
            /// Optional. The time to check if this TTS has been answered by a machine. Possible values range from 400ms to the default value of 10000ms. If you set a number outside this range the machine_timeout is set to the default value.
            /// </summary>
            public string machine_timeout { get; set; }
            /// <summary>
            /// Optional. Nexmo sends the Text-To-Speech Return Parameters to this URL to tell your App how the call was executed.
            /// </summary>
            public string callback { get; set; }
            /// <summary>
            /// Optional. The HTTP method used to send the status message to your callback. Must be GET (default) or POST.
            /// </summary>
            public string callback_method { get; set; }
        }

        public class TextToSpeechRequestResponse
        {
            /// <summary>
            /// An alphanumeric unique call identifier of up to 40 characters.
            /// </summary>
            public string call_id { get; set; }
            /// <summary>
            /// The phone number the call was made to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// Shows if your request has been sent successfully to Nexmo, or the reason why it could not be processed. See Response codes for more information.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// If status is not 0 this message explains the issue encountered.
            /// </summary>
            public string error_text { get; set; }
        }

        public class TextToSpeechReturn
        {
            /// <summary>
            /// The Nexmo ID for this TTS or TTS Prompt.
            /// </summary>
            public string call_id { get; set; }
            /// <summary>
            /// The phone number this TTS or TTS Prompt was sent to.
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// One of the following:
            ///   ok - TTS terminated normally.
            ///   failed - TTS failed.
            ///   error - an error occurred during the TTS.
            ///   blocked - TTS has been blocked.
            ///   machine - TTS has been stopped because you set machine_detection to hangup.
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// The expected value is out for a TTS or TTS Prompt.
            /// </summary>
            public string call_direction { get; set; }
            /// <summary>
            /// The total cost charged to your account in EUR.
            /// </summary>
            public string call_price { get; set; }
            /// <summary>
            /// Price-per-minute in EUR.
            /// </summary>
            public string call_rate { get; set; }
            /// <summary>
            ///	Duration of the call in seconds.
            /// </summary>
            public string call_duration { get; set; }
            /// <summary>
            /// The time you sent the TTS or TTS Prompt request. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            public string call_request { get; set; }
            /// <summary>
            /// The time the TTS or TTS Prompt started. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            public string call_start { get; set; }
            /// <summary>
            /// The time the TTS or TTS Prompt ended. Printed in the following format: YYYY/MM/DD HH:MM:SS.
            /// </summary>
            public string call_end { get; set; }
            /// <summary>
            /// An identifier of the carrier network used by the recipient. This value is optional.
            /// </summary>
            public string network_code { get; set; }
            /// <summary>
            /// The values entered by the end user in response to a TTS Prompt.
            /// Possible values are:
            ///   digits - Either:
            ///     Capture - the digits entered by the user.
            ///     Confirm - if the user entered the PIN correctly, this is the pin_code you set in the request.
            ///   nothing - the user did not press any digits.
            ///   undefined - Either:
            ///     The user had 3 failed attempts to enter the pin_code you set in the request.
            ///     There was a DTMF issue during this TTS Prompt.
            /// </summary>
            public string digits { get; set; }
        }

        public class TextToSpeechPromptCallCommand
        {
            /// <summary>
            /// The single phone number to call for each request. This number must be in international format, that is E.164. For example, when sending to the uk: to=447525856424
            /// </summary>
            public string to { get; set; }
            /// <summary>
            /// A UTF-8 and URL encoded message for your user. This message can be up to 1500 characters. Text-To-Speech Messages explains how to control the pacing of your message.
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// The language used to read the message. Combine this parameter with voice to control the language, accent and gender used to deliver this Text-To-Speech Prompt. The default language is en-us. Note: ensure that lg matches the language you have written in text.
            /// </summary>
            public string lg { get; set; }
            /// <summary>
            /// The gender of the voice used for the Text-To-Speech. Possible values are female or male. The default value is female
            /// </summary>
            public string voice { get; set; }
            /// <summary>
            /// Nexmo sends the Text-To-Speech Return Parameters to this URL to tell your App how the call was executed.
            /// </summary>
            public string callback { get; set; }
            /// <summary>
            /// The HTTP method used to send the status message to your callback. Must be GET (default) or POST.
            /// </summary>
            public string callback_method { get; set; }
            /// <summary>
            /// The number of digits in pin_code. The maximum number is 16, the default is 4. The pin_code must have the number of digits you set here. Extra digits entered by the user are ignored.
            /// </summary>
            public string max_digits { get; set; }
            /// <summary>
            /// The message played after your user has:
            ///   Capture - entered some digits.
            ///   Confirm - entered pin_code correctly.
            /// This string is up to 500 characters of UTF-8 and URL encoded text. For example, write D%c3%a9j%c3%a0+vu for Déjà vu.
            /// </summary>
            public string bye_text { get; set; }
        }

        public class TextToSpeechPromptConfirmCommand : TextToSpeechPromptCallCommand
        {
            /// <summary>
            /// The digits your user should enter in a Confirm Text-To-Speech Prompt. For example: 1234. The number of digits in this PIN must be the same as max_digits.
            /// </summary>
            public string pin_code { get; set; }
            /// <summary>
            /// The message played if your user fails to enter pin_code correctly. If your user has more attempts to enter pin_code, text is played immediately after failed_text. Leave a pause at the end of this message for a more natural sound. This string is up to 500 characters of UTF-8 and URL encoded text. For example, write D%c3%a9j%c3%a0+vu for Déjà vu.
            /// </summary>
            public string failed_text { get; set; }
        }

        public class TextToSpeechPromptCaptureCommand : TextToSpeechPromptCallCommand
        {
            /// <summary>
            /// A voice-enabled virtual number associated with your Nexmo account. This number is displayed on your user's handset during this Text-To-Speech or Text-To-Speech Prompt. Check the features and restrictions to see if this is possible in your destination country.
            /// </summary>
            public string from { get; set; }
            /// <summary>
            /// The number of times text can be repeated after your user picks up the Text-To-Speech Prompt. You can set text to be repeated up to 10 times. The default is 1.
            /// </summary>
            public string repeat { get; set; }
            /// <summary>
            /// Control the way this Text-To-Speech Prompt is handled if it is picked up by an answering machine. Set to:
            ///  true — play text after the beep. If the answerphone does not issue a beep the Text-To-Speech Prompt is closed.
            ///  hangup — hang up. The value of the status status code send to the callback url is set to machine.
            /// </summary>
            public string machine_detection { get; set; }
            /// <summary>
            /// The time to check if this Text-To-Speech Prompt has been answered by a machine. Possible values range from 400ms to the default value of 10000ms. If you set a number outside this range, machine_timeout is set to the default value.
            /// </summary>
            public string machine_timeout { get; set; }
        }

        public static CallRequestResponse Call(CallCommand cmd)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Voice), "/call/json"), cmd);

            return JsonConvert.DeserializeObject<CallRequestResponse>(response.JsonResponse);
        }

        public static CallReturn ParseCallReturn(string json)
        {
            return JsonConvert.DeserializeObject<CallReturn>(json);
        }

        public static TextToSpeechRequestResponse TextToSpeech(TextToSpeechCallCommand cmd)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/tts/json"), cmd);

            return JsonConvert.DeserializeObject<TextToSpeechRequestResponse>(response.JsonResponse);
        }

        public static TextToSpeechReturn ParseTextToSpeechReturn(string json)
        {
            return JsonConvert.DeserializeObject<TextToSpeechReturn>(json);
        }

        public static TextToSpeechRequestResponse TextToSpeechPrompt(TextToSpeechPromptCallCommand cmd)
        {
            var confirm = cmd as TextToSpeechPromptConfirmCommand;
            if (confirm != null)
            {
                var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/tts-prompt/json"), confirm);
                return JsonConvert.DeserializeObject<TextToSpeechRequestResponse>(response.JsonResponse);
            }
            var capture = cmd as TextToSpeechPromptCaptureCommand;
            if (capture != null)
            {
                var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/tts-prompt/json"), capture);
                return JsonConvert.DeserializeObject<TextToSpeechRequestResponse>(response.JsonResponse);
            }
            throw new ArgumentException("cmd must be either confirm or capture");
        }
    }
}