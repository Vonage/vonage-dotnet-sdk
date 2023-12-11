using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Sip.InitiateCall;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.InitiateCall
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task InitiateCall()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerialize));
            await this.Helper.VonageClient.SipClient.InitiateCallAsync(InitiateCallRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("b40ef09b-3811-4726-b508-e41a0f96c68f")
                    .WithToken("78d335fa-323d-0114-9c3d-d6f0d48968cf")
                    .WithSipUri(new Uri("sip:user@sip.partner.com;transport=tls"))
                    .EnableVideo()
                    .EnableForceMute()
                    .EnableEncryptedMedia()
                    .WithAuthentication(new SipElement.SipAuthentication("username", "p@ssw0rd"))
                    .WithFrom("from@example.com")
                    .WithHeader("headerKey", "some-value")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyCall);
        }

        [Fact]
        public async Task InitiateDefaultCall()
        {
            this.SetUpServer(nameof(SerializationTest.ShouldSerializeWithDefaultValues));
            await this.Helper.VonageClient.SipClient.InitiateCallAsync(InitiateCallRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("b40ef09b-3811-4726-b508-e41a0f96c68f")
                    .WithToken("78d335fa-323d-0114-9c3d-d6f0d48968cf")
                    .WithSipUri(new Uri("sip:user@sip.partner.com;transport=tls"))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyCall);
        }

        private void SetUpServer(string requestNamespace) =>
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/dial")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(requestNamespace))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
    }
}