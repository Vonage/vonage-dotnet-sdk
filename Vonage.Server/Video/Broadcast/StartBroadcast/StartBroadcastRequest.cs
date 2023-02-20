using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Archives.Common;

namespace Vonage.Server.Video.Broadcast.StartBroadcast;

public readonly struct StartBroadcastRequest : IVonageRequest
{
    public Guid ApplicationId { get; internal init; }
    public string Layout { get; internal init; }
    public int MaxBitrate { get; internal init; }
    public int MaxDuration { get; internal init; }
    public Maybe<string> MultiBroadcastTag { get; internal init; }
    public string Outputs { get; internal init; }
    public RenderResolution Resolution { get; internal init; }
    public string SessionId { get; internal init; }
    public StreamMode StreamMode { get; internal init; }
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast";
}