using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Common.Monads;

namespace Vonage.Common.Test.Extensions
{
    public class MaybeAssertionExtensions<T> : ReferenceTypeAssertions<Maybe<T>, MaybeAssertionExtensions<T>>
    {
        public MaybeAssertionExtensions(Maybe<T> subject) : base(subject)
        {
        }

        public AndConstraint<MaybeAssertionExtensions<T>> BeNone(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be None{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsNone)
                .FailWith("but found to be Some.");
            return new AndConstraint<MaybeAssertionExtensions<T>>(this);
        }

        public AndConstraint<MaybeAssertionExtensions<T>> BeSome(Action<T> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be Some{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSome)
                .FailWith("but found to be None.");
            this.Subject.IfSome(action);
            return new AndConstraint<MaybeAssertionExtensions<T>>(this);
        }

        public AndConstraint<MaybeAssertionExtensions<T>> BeSome(T expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be Some {0}{reason}, ", expected)
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSome)
                .FailWith("but found to be None.")
                .Then
                .ForCondition(subject => subject.Equals(Maybe<T>.Some(expected)))
                .FailWith("but found Some {0}.", this.Subject);
            return new AndConstraint<MaybeAssertionExtensions<T>>(this);
        }

        protected override string Identifier => "maybe";
    }
}