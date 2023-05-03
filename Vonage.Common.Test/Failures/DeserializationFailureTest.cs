using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;

namespace Vonage.Common.Test.Failures;

public class DeserializationFailureTest
{
    [Fact]
    public void GetFailureMessage_ShouldReturnMessage_GivenFailureIsCreatedFromCode() =>
        DeserializationFailure.From(typeof(DeserializationFailureTest), "serialized text").GetFailureMessage().Should()
            .Be("Unable to deserialize 'serialized text' into 'DeserializationFailureTest'.");

    [Fact]
    public void ToException_ShouldReturnVonageException()
    {
        var exception = DeserializationFailure.From(typeof(DeserializationFailureTest), "serialized text").ToException()
            .Should().BeOfType<VonageHttpRequestException>().Which;
        exception.Message.Should().Be("Unable to deserialize 'serialized text' into 'DeserializationFailureTest'.");
        exception.Json.Should().Be("serialized text");
    }
}