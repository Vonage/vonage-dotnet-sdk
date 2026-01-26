#region
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency;

[Trait("Category", "Serialization")]
public class EmergencyNumberResponseTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(EmergencyNumberResponseTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserializeEmergencyNumber() =>
        this.helper.Serializer
            .DeserializeObject<EmergencyNumberResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(EmergencyNumberResponse response)
    {
        response.Number.Number.Should().Be("15500900000");
        response.ContactName.Should().Be("John Smith");
        AddressTest.VerifyExpectedResponse(response.Address);
    }
}