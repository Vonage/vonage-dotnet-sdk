using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Extensions
{
    public static class FluentAssertionExtensions
    {
        public static MaybeAssertions<T> Should<T>(this Maybe<T> instance) => new MaybeAssertions<T>(instance);

        public static ResultAssertions<T> Should<T>(this Result<T> instance) => new ResultAssertions<T>(instance);
    }
}