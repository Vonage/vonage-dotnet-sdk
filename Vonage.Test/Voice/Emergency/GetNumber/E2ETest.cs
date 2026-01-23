#region
using System.Net;
using System.Threading.Tasks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.GetNumber;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.GetNumber;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private readonly SerializationTestHelper numberResponseHelper =
        new SerializationTestHelper(typeof(EmergencyNumberResponseTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task GetNumberAsync()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/emergency/numbers/33601020304")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.numberResponseHelper.GetResponseJson(nameof(EmergencyNumberResponseTest
                    .ShouldDeserialize200))));
        await this.Helper.VonageClient.EmergencyClient
            .GetNumberAsync(GetNumberRequest.Parse("+33601020304"))
            .Should()
            .BeSuccessAsync(EmergencyNumberResponseTest.VerifyExpectedResponse);
    }
}