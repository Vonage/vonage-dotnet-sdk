#region
using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Request;
#endregion

namespace Vonage.Voice;

/// <summary>
///     Represents a client to expose Voice capabilities.
/// </summary>
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
    /// GET /v1/calls/{uuid} - retrieve information about a single Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallRecord> GetCallAsync(string id, Credentials creds = null);

    /// <summary>
    /// GET /v1/calls - retrieve information about all your Calls
    /// <param name="filter">Filter to search calls on</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// </summary>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null);

    /// <summary>
    /// GET - retrieves the recording from a call based off of the input url
    /// </summary>
    /// <param name="recordingUrl">Url where the recorded call lives</param>
    /// <param name="creds">Overridden credentials</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    /// <returns>A response containing a byte array representing the file stream</returns>
    Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null);

    /// <summary>
    /// PUT /v1/calls/{uuid}/dtmf - send Dual-tone multi-frequency(DTMF) tones to an active Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="cmd">Command to execute against call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null);

    /// <summary>
    /// PUT /v1/calls/{uuid}/stream - stream an audio file to an active Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="cmd">Command to execute against call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null);

    /// <summary>
    /// PUT /v1/calls/{uuid}/talk - send a synthesized speech message to an active Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="cmd">Command to execute against call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null);

    /// <summary>
    /// DELETE /v1/calls/{uuid}/stream - stop streaming an audio file to an active Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null);

    /// <summary>
    /// DELETE /v1/calls/{uuid}/talk - stop sending a synthesized speech message to an active Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null);

    /// <summary>
    /// PUT /v1/calls/{uuid} - modify an existing Call
    /// </summary>
    /// <param name="id">id of call</param>
    /// <param name="cmd">Command to execute against call</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageHttpRequestException">thrown if an error is encountered when talking to the API</exception>
    Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null);

    /// <summary>
    ///     Register a listener to receive asynchronous DTMF inputs from a call. This is only applicable to Input NCCO events
    ///     with the mode set to asynchronous. The payload delivered to this URL will be an Input webhook event with a single
    ///     DTMF digit every time the callee enters DTMF into the call.
    /// </summary>
    /// <param name="uuid">UUID of the Call Leg</param>
    /// <param name="eventUrl">The URL (wrapped in an array) to send DTMF events to.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <returns></returns>
    Task SubscribeRealTimeDtmf(string uuid, Uri eventUrl, Credentials creds = null);

    /// <summary>
    ///     Removes the registered DTMF listener.
    /// </summary>
    /// <param name="uuid">UUID of the Call Leg</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <returns></returns>
    Task UnsubscribeRealTimeDtmf(string uuid, Credentials creds = null);

    /// <summary>
    ///     Returns a new client, targeting Voice API in the specified region.
    /// </summary>
    /// <param name="targetedRegion">The region.</param>
    /// <returns>A Voice client.</returns>
    IVoiceClient WithRegion(VonageUrls.Region targetedRegion);
}