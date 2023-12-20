using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Test.Unit.Common.Extensions
{
    public class ResultAssertionExtensions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertionExtensions<T>>
    {
        public ResultAssertionExtensions(Result<T> subject) : base(subject)
        {
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure(Action<IResultFailure> action)
        {
            this.BuildFailureExpectation();
            this.Subject.IfFailure(action);
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure()
        {
            this.BuildFailureExpectation();
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeFailure(IResultFailure expected)
        {
            this.BuildFailureExpectation()
                .Then
                .ForCondition(subject => subject.Equals(Result<T>.FromFailure(expected)))
                .FailWith(this.BuildResultFailureMessage());
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeParsingFailure(params string[] failureMessages)
        {
            this.BuildFailureExpectation()
                .Then
                .ForCondition(subject =>
                    subject.Equals(Result<T>.FromFailure(
                        ParsingFailure.FromFailures(failureMessages.Select(ResultFailure.FromErrorMessage).ToArray()))))
                .FailWith(this.BuildResultFailureMessage());
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeResultFailure(string expectedMessage)
        {
            this.BuildFailureExpectation()
                .Then
                .ForCondition(subject =>
                    subject.Equals(Result<T>.FromFailure(ResultFailure.FromErrorMessage(expectedMessage))))
                .FailWith(this.BuildResultFailureMessage());
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess()
        {
            this.BuildSuccessExpectation();
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess(Action<T> action)
        {
            this.BuildSuccessExpectation();
            this.Subject.IfSuccess(action);
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        public AndConstraint<ResultAssertionExtensions<T>> BeSuccess(T expected)
        {
            Execute.Assertion
                .WithExpectation("Expected to be Success {0}, ", expected)
                .Given(() => new {this.Subject, Expected = expected})
                .ForCondition(data => data.Subject.IsSuccess)
                .FailWith(this.BuildResultFailureMessage())
                .Then
                .ForCondition(data => EvaluateValueEquality(data.Subject.GetSuccessUnsafe(), expected))
                .FailWith("but found to be Success {0}.", this.Subject.GetSuccessUnsafe());
            return new AndConstraint<ResultAssertionExtensions<T>>(this);
        }

        private ContinuationOfGiven<Result<T>> BuildFailureExpectation() =>
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Failure{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsFailure)
                .FailWith(this.BuildResultSuccessMessage());

        private string BuildResultFailureMessage() => $"but found to be Failure '{this.GetResultFailure()}'.";

        private string BuildResultSuccessMessage() => $"but found to be Success '{this.GetResultSuccess()}'.";

        private ContinuationOfGiven<Result<T>> BuildSuccessExpectation() =>
            Execute.Assertion
                .WithExpectation("Expected {context:result} to be Success{reason}, ")
                .Given(() => this.Subject)
                .ForCondition(subject => subject.IsSuccess)
                .FailWith(this.BuildResultFailureMessage());

        private static bool EvaluateValueEquality<TA>(TA subject, TA expected)
        {
            try
            {
                subject.Should().BeEquivalentTo(expected);
                return true;
            }
            catch
            {
                return false;
            }
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

        private async Task<ResultAssertionExtensions<T>> InitializeAssertion() =>
            new ResultAssertionExtensions<T>(await this.Subject);

        protected override string Identifier => "resultAsync";
    }
}