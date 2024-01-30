using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;
using Xunit;

namespace Vonage.Test.Common.Failures;

[Trait("Category", "Unit")]
public class DeserializationFailureTest
{
    [Fact]
    public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCode() =>
        DeserializationFailure.From(typeof(DeserializationFailureTest), "serialized text").GetFailureMessage()
            .Should()
            .Be("Unable to deserialize 'serialized text' into 'DeserializationFailureTest'.");

    [Fact]
    public void ToException_ShouldReturnVonageException()
    {
        var exception = DeserializationFailure.From(typeof(DeserializationFailureTest), "serialized text")
            .ToException()
            .Should().BeOfType<VonageHttpRequestException>().Which;
        exception.Message.Should().Be("Unable to deserialize 'serialized text' into 'DeserializationFailureTest'.");
        exception.Json.Should().Be("serialized text");
    }

    [Fact]
    public void Type_ShouldReturnDeserializationFailure() => DeserializationFailure
        .From(typeof(DeserializationFailureTest), "serialized text")
        .Type
        .Should()
        .Be(typeof(DeserializationFailure));
}