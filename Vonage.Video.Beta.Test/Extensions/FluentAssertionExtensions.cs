using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Test.Extensions
{
    public static class FluentAssertionExtensions
    {
        public static MaybeAssertionExtensions<T> Should<T>(this Maybe<T> instance) =>
            new MaybeAssertionExtensions<T>(instance);

        public static ResultAssertionExtension<T> Should<T>(this Result<T> instance) =>
            new ResultAssertionExtension<T>(instance);
    }
}