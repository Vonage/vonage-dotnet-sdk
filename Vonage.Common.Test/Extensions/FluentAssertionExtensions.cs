using Vonage.Common.Monads;

namespace Vonage.Common.Test.Extensions
{
    public static class FluentAssertionExtensions
    {
        public static MaybeAssertionExtensions<T> Should<T>(this Maybe<T> instance) => new(instance);

        public static ResultAssertionExtension<T> Should<T>(this Result<T> instance) => new(instance);
    }
}