using System.Net;
using FsCheck;

namespace Vonage.Common.Test.Extensions
{
    /// <summary>
    ///     Extensions for FsCheck.
    /// </summary>
    public static class FsCheckExtensions
    {
        /// <summary>
        ///     Retrieves a generator that produces error responses with invalid status codes.
        /// </summary>
        /// <returns>An Arbitrary of ErrorResponse.</returns>
        public static Arbitrary<ErrorResponse> GetErrorResponses() =>
            Arb.From(from message in GetAny<string>().Generator
                from code in GetInvalidStatusCodes().Generator
                select new ErrorResponse(code, message));

        /// <summary>
        ///     Retrieves a HttpStatusCode generator that produces only invalid codes.
        /// </summary>
        /// <returns>An Arbitrary of status codes.</returns>
        public static Arbitrary<HttpStatusCode> GetInvalidStatusCodes() =>
            Gen.Choose(400, 599).Select(value => (HttpStatusCode) value).ToArbitrary();

        /// <summary>
        ///     Retrieves a generator that produces negative decimals.
        /// </summary>
        /// <returns>An Arbitrary of decimal.</returns>
        public static Arbitrary<int> GetNegativeNumbers() =>
            Gen.Choose(-1, -int.MaxValue).ToArbitrary();

        /// <summary>
        ///     Retrieves a string generator that produces only non-null/non-empty string.
        /// </summary>
        /// <returns>An Arbitrary of strings.</returns>
        public static Arbitrary<string> GetNonDeserializableStrings() =>
            GetAny<string>().MapFilter(_ => _,
                value => !string.IsNullOrWhiteSpace(value) && !value.Contains('{') && !value.Contains('}'));

        /// <summary>
        ///     Retrieves a generator that produces any value.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <returns>An Arbitrary.</returns>
        private static Arbitrary<T> GetAny<T>() => Arb.From<T>();
    }
}