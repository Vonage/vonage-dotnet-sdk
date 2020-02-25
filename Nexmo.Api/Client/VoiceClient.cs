using Nexmo.Api.Request;
using Nexmo.Api.Voice;


namespace Nexmo.Api.ClientMethods
{
    public class VoiceClient
    {
        public Credentials Credentials { get; set; }

        public VoiceClient(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallResponse CreateCall(CallCommand command, Credentials creds = null)
        {
            return Voice.Voice.CreateCall(command, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public PaginatedResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null)
        {
            return Voice.Voice.GetCalls(filter, creds ?? Credentials);
        }

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallResponse Get(string id, Credentials creds = null)
        {
            return Voice.Voice.GetCall(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallResponse UpdateCall(string id, CallEditCommand cmd, Credentials creds = null)
        {
            return Voice.Voice.UpdateCall(id, cmd, creds ?? Credentials);
        }

        #region Stream

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallCommandResponse StartStream(string id, StreamCommand cmd, Credentials creds = null)
        {
            return Voice.Voice.StartStream(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallCommandResponse StopStream(string id, Credentials creds = null)
        {
            return Voice.Voice.StopStream(id, creds ?? Credentials);
        }

        #endregion

        #region Talk
        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null)
        {
            return Voice.Voice.StartTalk(id, cmd, creds ?? Credentials);
        }

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallCommandResponse StopTalk(string id, Credentials creds = null)
        {
            return Voice.Voice.StopTalk(id, creds ?? Credentials);
        }

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public CallCommandResponse SendDtmf(string id, DtmfCommand cmd, Credentials creds = null)
        {
            return Voice.Voice.StartDtmf(id, cmd, creds ?? Credentials);
        }
        #endregion

        /// <summary>
        /// Retrieves a Recording        
        /// </summary>
        /// <param name="recordingUrl">Url where the recording is stored</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null)
        {
            return Voice.Voice.GetRecording(recordingUrl, creds ?? Credentials);
        }
    }
}
