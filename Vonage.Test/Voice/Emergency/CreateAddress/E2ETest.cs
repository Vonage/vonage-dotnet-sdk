#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.CreateAddress;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private readonly SerializationTestHelper numberResponseHelper =
        new SerializationTestHelper(typeof(EmergencyNumberResponseTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task CreateAddressAsync_GivenEmpty()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/addresses")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize_GivenEmpty)))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.numberResponseHelper.GetResponseJson(nameof(AddressTest.ShouldDeserializeAddress))));
        await this.Helper.VonageClient.EmergencyClient
            .CreateAddressAsync(SerializationTest.GetEmptyRequest())
            .Should()
            .BeSuccessAsync(AddressTest.VerifyExpectedResponse);
    }

    [Fact]
    public async Task CreateAddressAsync_GivenFull()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/addresses")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize_GivenFull)))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.numberResponseHelper.GetResponseJson(nameof(AddressTest.ShouldDeserializeAddress))));
        await this.Helper.VonageClient.EmergencyClient
            .CreateAddressAsync(SerializationTest.GetFullRequest())
            .Should()
            .BeSuccessAsync(AddressTest.VerifyExpectedResponse);
    }
}