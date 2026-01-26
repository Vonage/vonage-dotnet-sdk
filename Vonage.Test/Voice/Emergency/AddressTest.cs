#region
using System;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency;

[Trait("Category", "Serialization")]
public class AddressTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(AddressTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserializeAddress() =>
        this.helper.Serializer
            .DeserializeObject<Address>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(Address response) =>
        response.Should().Be(new Address(new Guid("c49f3586-9b3b-458b-89fc-3c8beb58865c"),
            "MyAddress",
            "1 REGAL CT",
            "Merchant's House 205",
            "New York",
            "NJ",
            Address.AddressType.Emergency,
            Address.AddressLocationType.Business,
            "10002",
            "US"));
}