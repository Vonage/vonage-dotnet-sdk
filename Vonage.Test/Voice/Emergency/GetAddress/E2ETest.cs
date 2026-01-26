#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetAddress;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetAddress;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private readonly SerializationTestHelper numberResponseHelper =
        new SerializationTestHelper(typeof(EmergencyNumberResponseTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task GetAddressAsync()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath($"/v1/addresses/{Constants.ValidAddressId}")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.numberResponseHelper.GetResponseJson(nameof(AddressTest.ShouldDeserializeAddress))));
        await this.Helper.VonageClient.EmergencyClient
            .GetAddressAsync(GetAddressRequest.Build().WithId(new Guid(Constants.ValidAddressId)).Create())
            .Should()
            .BeSuccessAsync(AddressTest.VerifyExpectedResponse);
    }
}