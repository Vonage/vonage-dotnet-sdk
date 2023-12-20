using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Test.Common.Extensions
{
    public static class SerializationExtensions
    {
        public static Result<string> GetStringContent<T>(this Result<T> result) where T : IVonageRequest =>
            result.Map(value => value.BuildRequestMessage())
                .Map(value => value.Content.ReadAsStringAsync().Result);
    }
}