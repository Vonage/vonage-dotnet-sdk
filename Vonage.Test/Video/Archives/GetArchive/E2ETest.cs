#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Archives.GetArchive;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.Archives.GetArchive;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetArchive()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/archive/97425ae1-4722-4dbf-b395-6169f08ebab3")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.ArchiveClient.GetArchiveAsync(GetArchiveRequest.Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithArchiveId(Guid.Parse("97425ae1-4722-4dbf-b395-6169f08ebab3"))
                .Create())
            .Should()
            .BeSuccessAsync(ArchiveTest.VerifyArchive);
    }
}