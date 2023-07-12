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
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/verify/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result = await this.helper.VonageClient.VerifyV2Client.VerifyCodeAsync(VerifyCodeRequest.Build()
                .WithRequestId(Guid.Parse("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb"))
                .WithCode("123456789")
                .Create());
            result.Should().BeSuccess();
        }
    }
}