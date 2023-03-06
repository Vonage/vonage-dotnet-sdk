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
        Action act = () =>
            throw DeserializationFailure.From(typeof(DeserializationFailureTest), "serialized text").ToException();
        act.Should().ThrowExactly<VonageHttpRequestException>()
            .WithMessage("Unable to deserialize 'serialized text' into 'DeserializationFailureTest'.")
            .And.Json.Should().Be("serialized text");
    }
}