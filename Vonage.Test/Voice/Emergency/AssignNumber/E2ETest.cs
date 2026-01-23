#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Voice.Emergency.AssignNumber;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.AssignNumber;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    private const string ValidNumber = "+33601020304";

    [Fact]
    public async Task AssignNumberAsync()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/emergency/numbers/33601020304")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBodyAsJson(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPatch())
            .RespondWith(Response.Create()
                .WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.EmergencyClient
            .AssignNumberAsync(AssignNumberRequest.Build()
                .WithNumber(ValidNumber)
                .WithAddressId(new Guid("c49f3586-9b3b-458b-89fc-3c8beb58865c"))
                .WithContactName("John Smith")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyExpectedResponse);
    }
}