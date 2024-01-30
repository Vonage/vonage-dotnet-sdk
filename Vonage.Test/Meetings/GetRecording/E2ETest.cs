using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Meetings.GetRecording;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Meetings.GetRecording;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetRecording()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/meetings/recordings/48a355bf-924d-4d4d-8e98-78575cf212dd")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.MeetingsClient.GetRecordingAsync(
                GetRecordingRequest.Parse(new Guid("48a355bf-924d-4d4d-8e98-78575cf212dd")))
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyRecording);
    }
}