#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.GetSessions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.ExperienceComposer.GetSessions;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task GetSessions()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render")
                .WithParam("offset", "150")
                .WithParam("count", "150")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.ExperienceComposerClient
            .GetSessionsAsync(GetSessionsRequest
                .Build()
                .WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"))
                .WithCount(150)
                .WithOffset(150)
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.BuildExpectedResponse());
    }

    [Fact]
    public async Task GetSessionsWithDefaultParameters()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/e3e78a75-221d-41ec-8846-25ae3db1943a/render")
                .WithParam("offset", "0")
                .WithParam("count", "50")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.ExperienceComposerClient
            .GetSessionsAsync(GetSessionsRequest
                .Build()
                .WithApplicationId(new Guid("e3e78a75-221d-41ec-8846-25ae3db1943a"))
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.BuildExpectedResponse());
    }
}