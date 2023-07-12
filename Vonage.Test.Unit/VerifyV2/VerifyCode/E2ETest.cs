using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Test.Unit.TestHelpers;
using Vonage.VerifyV2.VerifyCode;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.VerifyCode
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;
        private readonly SerializationTestHelper serialization;

        public E2ETest()
        {
            this.helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api");
            this.serialization = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());
        }

        [Fact]
        public async Task VerifyCode()
        {
            var requestId = Guid.NewGuid();
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath($"/v2/verify/{requestId}")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result = await this.helper.VonageClient.VerifyV2Client.VerifyCodeAsync(VerifyCodeRequest.Build()
                .WithRequestId(requestId)
                .WithCode("123456789")
                .Create());
            result.Should().BeSuccess();
        }
    }
}