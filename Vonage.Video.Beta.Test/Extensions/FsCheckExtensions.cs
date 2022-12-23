using System.Net;
using FsCheck;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Extensions
{
    /// <summary>
    ///     Extensions for FsCheck.
    /// </summary>
    public static class FsCheckExtensions
    {
        /// <summary>
        ///     Retrieves a HttpStatusCode generator that produces only invalid codes.
        /// </summary>
        /// <returns>An Arbitrary of status codes.</returns>
        public static Arbitrary<HttpStatusCode> GetInvalidStatusCodes() => GetAny<HttpStatusCode>()
            .MapFilter(_ => _, code => (int) code >= 400 && (int) code < 600);

        /// <summary>
        ///     Retrieves a string generator that produces only non-null/non-empty string.
        /// </summary>
        /// <returns>An Arbitrary of strings.</returns>
        public static Arbitrary<string> GetNonEmptyStrings() =>
            GetAny<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value));

        /// <summary>
        ///     Retrieves a generator that produces any value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <returns>An Arbitrary.</returns>
        public static Arbitrary<T> GetAny<T>() => Arb.From<T>();

        /// <summary>
        ///     Retrieves a generator that produces error responses with invalid status codes.
        /// </summary>
        /// <returns>An Arbitrary of ErrorResponse.</returns>
        public static Arbitrary<ErrorResponse> GetErrorResponses() =>
            Arb.From(from message in GetAny<string>().Generator
                from code in GetInvalidStatusCodes().Generator
                select new ErrorResponse(code, message));
    }
}