using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Vonage.Test.Common.Extensions;

public class JsonElementAssertionExtensions : ReferenceTypeAssertions<JsonElement, JsonElementAssertionExtensions>
{
    public JsonElementAssertionExtensions(JsonElement subject) : base(subject)
    {
    }

    protected override string Identifier => "JsonElement";

    public AndConstraint<JsonElementAssertionExtensions> Be(JsonElement expected)
    {
        Execute.Assertion
            .WithExpectation("Expected {context:option} to be {0}{reason}, ", expected)
            .Given(() => this.Subject)
            .ForCondition(subject => subject.GetRawText().Equals(expected.GetRawText()))
            .FailWith("but found {0}.", this.Subject);
        return new AndConstraint<JsonElementAssertionExtensions>(this);
    }
}