using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.Test.Unit.TestHelpers;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;
        private readonly SerializationTestHelper serialization;

        public E2ETest()
        {
            this.helper = new E2EHelper(
                "Vonage.Url.Api",
                Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(),
                    Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey")));
            this.serialization = new SerializationTestHelper(
                typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());
        }

        [Fact]
        public async Task StartEmailVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmailWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(EmailWorkflow.Parse("alice@company.com"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartSilentAuthVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeSilentAuthWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SilentAuthWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartSmsVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeSmsWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(SmsWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartVerificationWithFallbackWorkflows()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeFallbackWorkflows)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartVoiceVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeVoiceWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartWhatsAppInteractiveVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWhatsAppInteractiveWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }

        [Fact]
        public async Task StartWhatsAppVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.serialization.GetRequestJson(
                        nameof(SerializationTest.ShouldSerializeWhatsAppWorkflow)))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .Create());
            result.Should().BeSuccess(success =>
                success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));
        }
    }
}