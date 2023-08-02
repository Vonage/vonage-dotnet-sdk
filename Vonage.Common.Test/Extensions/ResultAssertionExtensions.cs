using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Test.Extensions
{
    public class ResultAssertionExtensions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertionExtensions<T>>
    {
        public ResultAssertionExtensions(Result<T> subject) : base(subject)
        {
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure(Action<IResultFailure> action)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.");
            this.Subject.IfFailure(action);
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure()
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure(IResultFailure expected)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure {0}, ", expected.GetFailureMessage())
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromFailure(expected)))
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeParsingFailure(params string[] failureMessages)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure {reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.")
                .Then
                .ForCondition(subject =>
                    subject.Equals(Result<T>.FromFailure(
                        ParsingFailure.FromFailures(failureMessages.Select(ResultFailure.FromErrorMessage).ToArray()))))
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeResultFailure(string expectedMessage)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure {reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.")
                .Then
                .ForCondition(subject =>
                    subject.Equals(Result<T>.FromFailure(ResultFailure.FromErrorMessage(expectedMessage))))
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess()
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Success{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess(Action<T> action)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Success{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.");
            this.Subject.IfSuccess(action);
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess(T expected)
        {
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Success {0}, ", expected.ToString())
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith($"but found to be Failure '{this.GetResultFailure()}'.")
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromSuccess(expected)))
                .FailWith($"but found to be Success '{this.GetResultSuccess()}'.");
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        private string GetResultFailure() =>
            this.Subject.Match(_ => string.Empty, failure => failure.GetFailureMessage());

        private string GetResultSuccess() => this.Subject.Map(success => success.ToString()).IfFailure(string.Empty);

        protected override string Identifier => "result";
    }

    public class
        ResultAsyncAssertionExtensions<T> : ReferenceTypeAssertions<Task<Result<T>>, ResultAsyncAssertionExtensions<T>>
    {
        public ResultAsyncAssertionExtensions(Task<Result<T>> subject) : base(subject)
        {
        }

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeFailureAsync(Action<IResultFailure> action) =>
            (await this.InitializeAssertion()).BeFailure(action);

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeFailureAsync() =>
            (await this.InitializeAssertion()).BeFailure();

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeFailureAsync(IResultFailure expected) =>
            (await this.InitializeAssertion()).BeFailure(expected);

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeParsingFailureAsync(
            params string[] failureMessages) =>
            (await this.InitializeAssertion()).BeParsingFailure(failureMessages);

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeResultFailureAsync(string expectedMessage) =>
            (await this.InitializeAssertion()).BeResultFailure(expectedMessage);

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeSuccessAsync() =>
            (await this.InitializeAssertion()).BeSuccess();

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeSuccessAsync(Action<T> action) =>
            (await this.InitializeAssertion()).BeSuccess(action);

        public async Task<AndConstraint<ResultAssertionExtensions<T>>> BeSuccessAsync(T expected) =>
            (await this.InitializeAssertion()).BeSuccess(expected);

        private async Task<ResultAssertionExtensions<T>> InitializeAssertion() => new(await this.Subject);

        protected override string Identifier => "resultAsync";
    }
}