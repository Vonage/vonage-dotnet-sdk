using System.Net;

namespace Vonage.Voice;

/// <summary>
///     Represents the response from downloading a call recording, containing the raw audio data.
/// </summary>
public class GetRecordingResponse
{
    /// <summary>
    ///     The recording file content as a byte array. Write this to a file or stream to play back the recording.
    /// </summary>
    public byte[] ResultStream { get; set; }

    /// <summary>
    ///     The HTTP status code returned when downloading the recording.
    /// </summary>
    public HttpStatusCode Status { get; set; }
}