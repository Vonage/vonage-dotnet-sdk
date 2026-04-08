#region
using System;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Exceptions;
using Vonage.Request;
#endregion

namespace Vonage.Voice;

/// <summary>
///     Exposes Voice API v1 features for creating outbound calls, controlling in-progress calls, streaming audio, playing text-to-speech, sending DTMF tones, and retrieving call history.
/// </summary>
public interface IVoiceClient
{
    /// <summary>
    ///     Creates an outbound call to a phone number, SIP endpoint, WebSocket, or VBC extension.
    /// </summary>
    /// <param name="command">The call configuration including destination endpoints, NCCO or answer URL, and optional settings such as machine detection and timers.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallResponse"/> containing the call UUID, status, direction, and conversation UUID.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var command = new CallCommand
    /// {
    ///     To = new[] { new PhoneEndpoint { Number = "447700900000" } },
    ///     From = new PhoneEndpoint { Number = "447700900001" },
    ///     Ncco = new Ncco(new TalkAction { Text = "Hello from Vonage!" })
    /// };
    /// var response = await client.CreateCallAsync(command);
    /// Console.WriteLine($"Call UUID: {response.Uuid}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallResponse> CreateCallAsync(CallCommand command, Credentials creds = null);

    /// <summary>
    ///     Retrieves detailed information about a single call by its UUID, including status, duration, price, and endpoints.
    /// </summary>
    /// <param name="id">The UUID of the call to retrieve.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallRecord"/> containing the full call details.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var call = await client.GetCallAsync("63f61863-4a51-4f6b-86e1-46edebcf9356");
    /// Console.WriteLine($"Status: {call.Status}, Duration: {call.Duration}s");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallRecord> GetCallAsync(string id, Credentials creds = null);

    /// <summary>
    ///     Retrieves a paginated list of calls, optionally filtered by status, date range, or conversation UUID.
    /// </summary>
    /// <param name="filter">The filter criteria for searching calls (status, date range, pagination, ordering).</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A paginated response containing matching <see cref="CallList"/> records.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var filter = new CallSearchFilter { Status = "completed", PageSize = 20 };
    /// var calls = await client.GetCallsAsync(filter);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<PageResponse<CallList>> GetCallsAsync(CallSearchFilter filter, Credentials creds = null);

    /// <summary>
    ///     Downloads a call recording as a byte array from the URL provided in the recording webhook.
    /// </summary>
    /// <param name="recordingUrl">The recording URL received in the recording webhook event.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="GetRecordingResponse"/> containing the recording file as a byte array.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var recording = await client.GetRecordingAsync("https://api.nexmo.com/v1/files/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    /// File.WriteAllBytes("recording.mp3", recording.ResultStream);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<GetRecordingResponse> GetRecordingAsync(string recordingUrl, Credentials creds = null);

    /// <summary>
    ///     Retrieves a transcription result from the URL provided in the transcription webhook.
    /// </summary>
    /// <param name="transcriptionUrl">
    ///     The <c>transcription_url</c> delivered by the transcription webhook
    ///     (e.g. <c>https://api.nexmo.com/v1/files/...</c>).
    /// </param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>The deserialized <see cref="TranscriptionResult"/> containing channels, transcripts, and word-level detail.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var transcription = await client.GetTranscriptionAsync("https://api.nexmo.com/v1/files/aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<TranscriptionResult> GetTranscriptionAsync(string transcriptionUrl, Credentials creds = null);

    /// <summary>
    ///     Sends DTMF (Dual-Tone Multi-Frequency) tones into an active call.
    /// </summary>
    /// <param name="id">The UUID of the call to send DTMF tones to.</param>
    /// <param name="cmd">The DTMF command containing the digit string to send.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallCommandResponse"/> confirming the action was performed.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var response = await client.StartDtmfAsync("63f61863-4a51-4f6b-86e1-46edebcf9356", new DtmfCommand { Digits = "1713" });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallCommandResponse> StartDtmfAsync(string id, DtmfCommand cmd, Credentials creds = null);

    /// <summary>
    ///     Streams an audio file into an active call. The file must be a single-channel 16-bit WAV at 8kHz or 16kHz, or an MP3 file.
    /// </summary>
    /// <param name="id">The UUID of the call to stream audio into.</param>
    /// <param name="command">The stream command containing the audio URL, loop count, and volume level.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallCommandResponse"/> confirming the stream was started.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var command = new StreamCommand { StreamUrl = new[] { "https://example.com/waiting.mp3" } };
    /// var response = await client.StartStreamAsync("63f61863-4a51-4f6b-86e1-46edebcf9356", command);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallCommandResponse> StartStreamAsync(string id, StreamCommand command, Credentials creds = null);

    /// <summary>
    ///     Plays a text-to-speech message into an active call using the specified language and voice style.
    /// </summary>
    /// <param name="id">The UUID of the call to play text-to-speech into.</param>
    /// <param name="cmd">The talk command containing the text, language, voice style, loop count, and volume level.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallCommandResponse"/> confirming the talk was started.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var cmd = new TalkCommand { Text = "Hello, how are you?", Language = "en-US", Style = 0 };
    /// var response = await client.StartTalkAsync("63f61863-4a51-4f6b-86e1-46edebcf9356", cmd);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallCommandResponse> StartTalkAsync(string id, TalkCommand cmd, Credentials creds = null);

    /// <summary>
    ///     Stops streaming an audio file into an active call.
    /// </summary>
    /// <param name="id">The UUID of the call to stop streaming audio into.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallCommandResponse"/> confirming the stream was stopped.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var response = await client.StopStreamAsync("63f61863-4a51-4f6b-86e1-46edebcf9356");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallCommandResponse> StopStreamAsync(string id, Credentials creds = null);

    /// <summary>
    ///     Stops playing a text-to-speech message in an active call.
    /// </summary>
    /// <param name="id">The UUID of the call to stop the text-to-speech in.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns>A <see cref="CallCommandResponse"/> confirming the talk was stopped.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var response = await client.StopTalkAsync("63f61863-4a51-4f6b-86e1-46edebcf9356");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<CallCommandResponse> StopTalkAsync(string id, Credentials creds = null);

    /// <summary>
    ///     Modifies an in-progress call. Supports hangup, mute, unmute, earmuff, unearmuff, and transfer actions.
    /// </summary>
    /// <param name="id">The UUID of the call to modify.</param>
    /// <param name="command">The edit command specifying the action to perform (e.g., hangup, mute, transfer).</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <returns><c>true</c> if the call was successfully modified.</returns>
    /// <exception cref="VonageHttpRequestException">Thrown if an error is encountered when communicating with the API.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var success = await client.UpdateCallAsync("63f61863-4a51-4f6b-86e1-46edebcf9356",
    ///     new CallEditCommand { Action = CallEditCommand.ActionType.hangup });
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<bool> UpdateCallAsync(string id, CallEditCommand command, Credentials creds = null);

    /// <summary>
    ///     Registers a listener to receive real-time asynchronous DTMF inputs from a call. This is only applicable to Input NCCO events
    ///     with the mode set to asynchronous. The payload delivered to the URL will be an Input webhook event with a single
    ///     DTMF digit every time the callee enters DTMF into the call.
    /// </summary>
    /// <param name="uuid">The UUID of the call leg to subscribe to.</param>
    /// <param name="eventUrl">The URL to send DTMF events to as POST requests.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <example>
    /// <code><![CDATA[
    /// await client.SubscribeRealTimeDtmf("63f61863-4a51-4f6b-86e1-46edebcf9356",
    ///     new Uri("https://example.com/dtmf-events"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task SubscribeRealTimeDtmf(string uuid, Uri eventUrl, Credentials creds = null);

    /// <summary>
    ///     Removes the registered DTMF listener from a call, stopping real-time DTMF event delivery.
    /// </summary>
    /// <param name="uuid">The UUID of the call leg to unsubscribe from.</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request.</param>
    /// <example>
    /// <code><![CDATA[
    /// await client.UnsubscribeRealTimeDtmf("63f61863-4a51-4f6b-86e1-46edebcf9356");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task UnsubscribeRealTimeDtmf(string uuid, Credentials creds = null);

    /// <summary>
    ///     Returns a new Voice client targeting a specific region. Use this to create and manage calls within a specific geographic region.
    /// </summary>
    /// <param name="targetedRegion">The region to target for API requests.</param>
    /// <returns>A new <see cref="IVoiceClient"/> instance configured for the specified region.</returns>
    IVoiceClient WithRegion(VonageUrls.Region targetedRegion);
}