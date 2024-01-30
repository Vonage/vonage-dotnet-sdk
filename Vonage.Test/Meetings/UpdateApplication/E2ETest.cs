using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.UpdateApplication;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.UpdateApplication;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task CreateRoomWithDefaultValues()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/meetings/applications")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPatch())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.MeetingsClient.UpdateApplicationAsync(
                UpdateApplicationRequest.Parse(new Guid("e86a7335-35fe-45e1-b961-5777d4748022")))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyApplication);
    }
}