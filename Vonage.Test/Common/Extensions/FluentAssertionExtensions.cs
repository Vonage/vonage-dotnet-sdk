using System.Threading.Tasks;
using Vonage.Common.Monads;

namespace Vonage.Test.Common.Extensions
{
    public static class FluentAssertionExtensions
    {
        public static MaybeAssertionExtensions<T> Should<T>(this Maybe<T> instance) => new(instance);

        public static ResultAssertionExtensions<T> Should<T>(this Result<T> instance) => new(instance);

        public static ResultAsyncAssertionExtensions<T> Should<T>(this Task<Result<T>> instance) => new(instance);
    }
}