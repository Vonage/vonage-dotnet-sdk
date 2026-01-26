#region
using System.Linq;
using FluentAssertions;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddresses;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddresses;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<GetAddressesResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(GetAddressesResponse response)
    {
        response.Page.Should().Be(3);
        response.PageSize.Should().Be(100);
        response.TotalItems.Should().Be(1450);
        response.TotalPages.Should().Be(15);
        response.Embedded.Addresses.Should().HaveCount(1);
        AddressTest.VerifyExpectedResponse(response.Embedded.Addresses.First());
    }
}