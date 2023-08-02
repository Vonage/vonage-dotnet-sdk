using Vonage.Common.Monads;

namespace Vonage.Common.Test
{
    public struct ExpectedRequest
    {
        public Maybe<string> Content { get; set; }
        public HttpMethod Method { get; set; }
        public Uri RequestUri { get; set; }
    }
}