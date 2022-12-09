using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Common
{
    public static class FluentAssertionExtensions
    {
        public static MaybeAssertions<T> Should<T>(this Maybe<T> instance) => new MaybeAssertions<T>(instance);
    }

    public class MaybeAssertions<T> : ReferenceTypeAssertions<Maybe<T>, MaybeAssertions<T>>
    {
        public MaybeAssertions(Maybe<T> subject) : base(subject)
        {
        }

        protected override string Identifier => "maybe";

        public AndConstraint<MaybeAssertions<T>> BeNone(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be None{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsNone)
                .FailWith("but found to be Some.");
            return new AndConstraint<MaybeAssertions<T>>(this);
        }

        public AndConstraint<MaybeAssertions<T>> BeSome(Action<T> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be Some{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSome)
                .FailWith("but found to be None.");
            this.Subject.IfSome(action);
            return new AndConstraint<MaybeAssertions<T>>(this);
        }

        public AndConstraint<MaybeAssertions<T>> Be(T expected, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:option} to be Some {0}{reason}, ", expected)
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSome)
                .FailWith("but found to be None.")
                .Then
                .ForCondition(subject => subject.Equals(expected))
                .FailWith("but found Some {0}.", this.Subject);
            return new AndConstraint<MaybeAssertions<T>>(this);
        }
    }
}