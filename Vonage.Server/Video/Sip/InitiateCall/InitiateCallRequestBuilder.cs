using System;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Server.Video.Sip.InitiateCall;

public class InitiateCallRequestBuilder : IVonageRequestBuilder<InitiateCallRequest>
{
    public Result<InitiateCallRequest> Create() => throw new NotImplementedException();
}