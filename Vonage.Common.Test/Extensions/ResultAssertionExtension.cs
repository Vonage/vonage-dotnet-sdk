﻿using FluentAssertions;
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
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.");
            this.Subject.IfFailure(action);
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        public AndConstraint<ResultAssertionExtension<T>> BeFailure(IResultFailure expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Failure {0}, ", expected.GetFailureMessage())
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromFailure(expected)))
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
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
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            this.Subject.IfSuccess(action);
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        public AndConstraint<ResultAssertionExtension<T>> BeSuccess(T expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .WithExpectation("Expected {context:result} to be Success {0}, ", expected.ToString())
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromSuccess(expected)))
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.");
            return new AndConstraint<ResultAssertionExtension<T>>(this);
        }

        private string GetResultFailure() =>
            this.Subject.Match(_ => string.Empty, failure => failure.GetFailureMessage());

        private string GetResultSuccess() => this.Subject.Map(success => success.ToString()).IfFailure(string.Empty);

        protected override string Identifier => "result";
    }
}