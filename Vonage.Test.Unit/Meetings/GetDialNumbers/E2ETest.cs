using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetDialNumbers
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetDialNumbers()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/dial-in-numbers")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.GetDialNumbersAsync()
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyNumbers);
        }
    }
}