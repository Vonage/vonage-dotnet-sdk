using System.Net;

namespace Vonage.Voice;

public class GetRecordingResponse
{
    /// <summary>
    /// Stream of bytes containg the recording file's content
    /// </summary>
    public byte[] ResultStream { get; set; }

    /// <summary>
    /// Response Status of the HTTP Request
    /// </summary>
    public HttpStatusCode Status { get; set; }
}