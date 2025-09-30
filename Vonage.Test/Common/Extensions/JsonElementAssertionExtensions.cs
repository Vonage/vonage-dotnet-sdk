#region
using System.Text.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
#endregion

namespace Vonage.Test.Common.Extensions;

public class JsonElementAssertionExtensions(JsonElement subject)
    : ReferenceTypeAssertions<JsonElement, JsonElementAssertionExtensions>(subject)
{
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