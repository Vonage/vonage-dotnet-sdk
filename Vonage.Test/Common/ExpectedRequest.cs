using System;
using System.Net.Http;
using Vonage.Common.Monads;

namespace Vonage.Test.Common;

public struct ExpectedRequest
{
    public Maybe<string> Content { get; set; }
    public HttpMethod Method { get; set; }
    public Uri RequestUri { get; set; }
}