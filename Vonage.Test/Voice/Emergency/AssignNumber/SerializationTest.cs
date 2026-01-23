#region
using System;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency;
using Vonage.Voice.Emergency.AssignNumber;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.AssignNumber;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private const string ValidNumber = "+33601020304";

    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize() =>
        AssignNumberRequest.Build()
            .WithNumber(ValidNumber)
            .WithAddressId(new Guid("c49f3586-9b3b-458b-89fc-3c8beb58865c"))
            .WithContactName("John Smith")
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<AssignNumberResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(AssignNumberResponse response)
    {
        response.Number.Number.Should().Be("15500900000");
        response.ContactName.Should().Be("John Smith");
        response.Address.Should().Be(new Address(new Guid("c49f3586-9b3b-458b-89fc-3c8beb58865c"),
            "myaddress",
            "1 REGAL CT",
            "Merchant's House 205",
            "New York",
            "NJ",
            Address.AddressType.Emergency,
            Address.AddressLocationType.Business,
            "10002",
            "US"));
    }
}