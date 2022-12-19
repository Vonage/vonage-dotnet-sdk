using System.Net;
using FsCheck;

namespace Vonage.Video.Beta.Test.Extensions
{
    public static class FsCheckExtensions
    {
        public static Arbitrary<HttpStatusCode> GetInvalidStatusCodes() => Arb.From<HttpStatusCode>()
            .MapFilter(_ => _, code => (int) code >= 400 && (int) code < 600);
    }
}