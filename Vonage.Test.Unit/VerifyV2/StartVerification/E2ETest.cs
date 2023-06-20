using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    public class E2ETest
    {
        private readonly SerializationTestHelper helper;

        private readonly VonageClient vonageClient;
        private readonly WireMockServer server;

        public E2ETest()
        {
            this.server = WireMockServer.Start();
            this.vonageClient = new VonageClient(Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(),
                Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey")), new Configuration
            {
                Settings =
                {
                    ["appSettings:Vonage.Url.Api"] = this.server.Url,
                },
            });
            this.helper = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());
        }

        [Fact]
        public async Task StartEmailVerification()
        {
            this.server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.helper.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmailWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.helper.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.vonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartSilentAuthVerification()
        {
            this.server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.helper.GetRequestJson(nameof(SerializationTest.ShouldSerializeSilentAuthWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.helper.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.vonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SilentAuthWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartSmsVerification()
        {
            this.server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.helper.GetRequestJson(nameof(SerializationTest.ShouldSerializeSmsWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.helper.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.vonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest.Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }
    }
}