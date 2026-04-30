using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Vonage.ApplicationsNew.Capabilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;

namespace Vonage.ApplicationsNew.CreateApplication;

/// <summary>
///     Represents a request to create a new Vonage application.
/// </summary>
[Builder]
public readonly partial struct CreateApplicationRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the application name. This does not need to be unique.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("My Voice Application")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string Name { get; internal init; }

    /// <summary>
    ///     Configures Voice call handling, including answer URL, event URL, and fallback settings.
    /// </summary>
    [Optional]
    public Maybe<VoiceCapability> Voice { get; internal init; }

    /// <summary>
    ///     Configures Messages webhooks for inbound messages and delivery status updates.
    /// </summary>
    [Optional]
    public Maybe<MessagesCapability> Messages { get; internal init; }

    /// <summary>
    ///     Configures RTC / Client SDK event webhooks.
    /// </summary>
    [Optional]
    public Maybe<RtcCapability> Rtc { get; internal init; }

    /// <summary>
    ///     Enables zero-rated VBC calls. Pass an empty <see cref="VbcCapability"/> instance to activate.
    /// </summary>
    [Optional]
    public Maybe<VbcCapability> Vbc { get; internal init; }

    /// <summary>
    ///     Configures Network APIs integration with a network application ID and redirect URI.
    /// </summary>
    [Optional]
    public Maybe<NetworkApisCapability> NetworkApis { get; internal init; }

    /// <summary>
    ///     Configures Meetings webhooks for recording, room, and session events.
    /// </summary>
    [Optional]
    public Maybe<MeetingsCapability> Meetings { get; internal init; }

    /// <summary>
    ///     Configures Verify v2 status webhooks.
    /// </summary>
    [Optional]
    public Maybe<VerifyCapability> Verify { get; internal init; }

    /// <summary>
    ///     Configures Video webhooks for session, stream, and archive events.
    /// </summary>
    [Optional]
    public Maybe<VideoCapability> Video { get; internal init; }

    /// <summary>
    ///     Sets the public key used for JWT authentication.
    /// </summary>
    [Optional]
    public Maybe<ApplicationKeys> Keys { get; internal init; }

    /// <summary>
    ///     Sets the privacy settings for the application.
    /// </summary>
    [Optional]
    public Maybe<ApplicationPrivacy> Privacy { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, "/v2/applications")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent()
    {
        var serializer = JsonSerializerBuilder.BuildWithSnakeCase();
        var body = new Dictionary<string, object> {["name"] = this.Name};
        var capabilities = new ApplicationCapabilities();
        this.Voice.IfSome(voice => capabilities = capabilities with {Voice = voice});
        this.Messages.IfSome(messages => capabilities = capabilities with {Messages = messages});
        this.Rtc.IfSome(rtc => capabilities = capabilities with {Rtc = rtc});
        this.Vbc.IfSome(vbc => capabilities = capabilities with {Vbc = vbc});
        this.NetworkApis.IfSome(networkApis => capabilities = capabilities with {NetworkApis = networkApis});
        this.Meetings.IfSome(meetings => capabilities = capabilities with {Meetings = meetings});
        this.Verify.IfSome(verify => capabilities = capabilities with {Verify = verify});
        this.Video.IfSome(video => capabilities = capabilities with {Video = video});
        if (capabilities.HasCapabilities) body["capabilities"] = capabilities;
        this.Keys.IfSome(keys => body["keys"] = keys);
        this.Privacy.IfSome(privacy => body["privacy"] = privacy);
        return new StringContent(serializer.SerializeObject(body), Encoding.UTF8, "application/json");
    }

    [ValidationRule]
    internal static Result<CreateApplicationRequest> VerifyName(CreateApplicationRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name));
}
