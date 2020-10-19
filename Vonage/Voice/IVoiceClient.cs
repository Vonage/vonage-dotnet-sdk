using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Voice
{
    public interface IVoiceClient
    {
        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="command"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null);

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null);
        
        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallRecord> GetCallAsync(string id, Credentials creds = null);
        
        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null);
        
        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null);
        
        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null);
        
        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null);
        
        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null);
        
        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null);
        
        /// <summary>
        /// GET - retrieves the recording from a call based off of the input url
        /// </summary>
        /// <param name="recordingUrl">Url where the recorded call lives</param>
        /// <param name="creds">Overridden credentials</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns>A response containing a byte array representing the file stream</returns>
        Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null);

        /// <summary>
        /// POST /v1/calls - create an outbound SIP or PSTN Call
        /// </summary>
        /// <param name="command"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallResponse CreateCall(CallCommand command, Credentials creds = null);

        /// <summary>
        /// GET /v1/calls - retrieve information about all your Calls
        /// <param name="filter">Filter to search calls on</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// </summary>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        PageResponse<CallList> GetCalls(CallSearchFilter filter, Credentials creds = null);

        /// <summary>
        /// GET /v1/calls/{uuid} - retrieve information about a single Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallRecord GetCall(string id, Credentials creds = null);

        /// <summary>
        /// PUT /v1/calls/{uuid} - modify an existing Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        bool UpdateCall(string id, CallEditCommand command, Credentials creds = null);

        /// <summary>
        /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallCommandResponse StartStream(string id, StreamCommand command, Credentials creds = null);

        /// <summary>
        /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallCommandResponse StopStream(string id, Credentials creds = null);

        /// <summary>
        /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallCommandResponse StartTalk(string id, TalkCommand cmd, Credentials creds = null);

        /// <summary>
        /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallCommandResponse StopTalk(string id, Credentials creds = null);

        /// <summary>
        /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
        /// </summary>
        /// <param name="id">id of call</param>
        /// <param name="cmd">Command to execute against call</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        CallCommandResponse StartDtmf(string id, DtmfCommand cmd, Credentials creds = null);

        /// <summary>
        /// GET - retrieves the recording from a call based off of the input url
        /// </summary>
        /// <param name="recordingUrl">Url where the recorded call lives</param>
        /// <param name="creds">Overridden credentials</param>
        /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <returns>A response containing a byte array representing the file stream</returns>
        GetRecordingResponse GetRecording(string recordingUrl, Credentials creds = null);
    }
}