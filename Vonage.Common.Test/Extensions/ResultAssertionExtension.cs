using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Test.Extensions
{
    public class ResultAssertionExtension<T> : ReferenceTypeAssertions<Result<T>, ResultAssertionExtension<T>>
    {
        public ResultAssertionExtension(Result<T> subject) : base(subject)
        {
        }

        public AndConstraint<ResultAssertionExtension<T>> BeFailure(Action<IResultFailure> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Failure{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith("but found to be Success.");
            this.Subject.IfFailure(action);
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        public AndConstraint<ResultAssertionExtension<T>> BeFailure(IResultFailure expected, string because = "",
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
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        public AndConstraint<ResultAssertionExtension<T>> BeSuccess(Action<T> action, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Success{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith("but found to be Failure.");
            this.Subject.IfSuccess(action);
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        public AndConstraint<ResultAssertionExtension<T>> BeSuccess(T expected, string because = "",
            params object[] becauseArgs)
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
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        protected override string Identifier => "result";
    }
}