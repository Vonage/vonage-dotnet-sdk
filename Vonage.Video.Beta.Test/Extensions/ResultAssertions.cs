using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Extensions
{
    public class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
    {
        public ResultAssertions(Result<T> subject) : base(subject)
        {
        }

        protected override string Identifier => "result";

        public AndConstraint<ResultAssertions<T>> BeFailure(Action<ResultFailure> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Failure{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith("but found to be Success.");
            this.Subject.IfFailure(action);
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeSuccess(Action<T> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Success{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith("but found to be Failure.");
            this.Subject.IfSuccess(action);
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> Be(T expected, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Success {0}{reason}, ", expected)
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith("but found to be Failure.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromSuccess(expected)))
                .FailWith("but found Success {0}.", this.Subject);
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> Be(ResultFailure expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Failure {0}{reason}, ", expected)
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith("but found to be Success.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromFailure(expected)))
                .FailWith("but found Success {0}.", this.Subject);
            return new AndConstraint<ResultAssertions<T>>(this);
        }
    }
}