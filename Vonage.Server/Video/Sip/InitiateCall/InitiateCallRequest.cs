using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a request to initiate an outbound Sip call.
/// </summary>
public readonly struct InitiateCallRequest : IVonageRequest
{
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    public string GetEndpointPath() => throw new NotImplementedException();
}