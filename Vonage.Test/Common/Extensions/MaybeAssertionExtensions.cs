#region
using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Test.Common.Extensions;

public class MaybeAssertionExtensions<T>(Maybe<T> subject)
    : ReferenceTypeAssertions<Maybe<T>, MaybeAssertionExtensions<T>>(subject)
{
    protected override string Identifier => "maybe";

    public AndConstraint<MaybeAssertionExtensions<T>> Be(Maybe<T> expected)
    {
        Execute.Assertion
            .WithExpectation("Expected {context:option} to be {0}{reason}, ", expected)
            .Given(() => this.Subject)
            .ForCondition(subject => subject.Equals(expected))
            .FailWith("but found {0}.", this.Subject);
        return new AndConstraint<MaybeAssertionExtensions<T>>(this);
    }

    public AndConstraint<MaybeAssertionExtensions<T>> BeNone()
    {
        Execute.Assertion
            .WithExpectation("Expected {context:option} to be None{reason}, ")
            .Given(() => this.Subject)
            .ForCondition(subject => subject.IsNone)
            .FailWith("but found to be Some.");
        return new AndConstraint<MaybeAssertionExtensions<T>>(this);
    }

    public AndConstraint<MaybeAssertionExtensions<T>> BeSome(Action<T> action)
    {
        Execute.Assertion
            .WithExpectation("Expected {context:option} to be Some{reason}, ")
            .Given(() => this.Subject)
            .ForCondition(subject => subject.IsSome)
            .FailWith("but found to be None.");
        this.Subject.IfSome(action);
        return new AndConstraint<MaybeAssertionExtensions<T>>(this);
    }

    public AndConstraint<MaybeAssertionExtensions<T>> BeSome(T expected)
    {
        Execute.Assertion
            .WithExpectation($"Expected {this.Subject} to be equivalent to {expected}, ")
            .Given(() => new {this.Subject, Expected = expected})
            .ForCondition(data => data.Subject.IsSome)
            .FailWith("but found to be None.")
            .Then
            .Given(data => new
            {
                Subject = data.Subject.GetUnsafe(),
                data.Expected,
            })
            .ForCondition(data => EvaluateValueEquality(data.Subject, data.Expected))
            .FailWith($"but value equality failed between {this.Subject} and {expected}.");
        return new AndConstraint<MaybeAssertionExtensions<T>>(this);
    }

    private static bool EvaluateValueEquality(T subject, T expected)
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
}