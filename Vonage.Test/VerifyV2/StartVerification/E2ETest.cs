using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.Email;
using Vonage.VerifyV2.StartVerification.SilentAuth;
using Vonage.VerifyV2.StartVerification.Sms;
using Vonage.VerifyV2.StartVerification.Voice;
using Vonage.VerifyV2.StartVerification.WhatsApp;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.VerifyV2.StartVerification
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task StartEmailVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeEmailWorkflow));
            var result = await this.StartVerificationAsyncWithWorkflow(EmailWorkflow.Parse("alice@company.com"));
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartSilentAuthVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeSilentAuthWorkflow));
            var result = await this.StartVerificationAsyncWithWorkflow(SilentAuthWorkflow.Parse("447700900000"));
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartSmsVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeSmsWorkflow));
            var result = await this.StartVerificationAsyncWithWorkflow(SmsWorkflow.Parse("447700900000"));
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartVerificationWithFallbackWorkflows()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeFallbackWorkflows));
            var result = await this.Helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(WhatsAppWorkflow.Parse("447700900000"))
                .WithFallbackWorkflow(VoiceWorkflow.Parse("447700900000"))
                .Create());
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartVoiceVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeVoiceWorkflow));
            var result = await this.StartVerificationAsyncWithWorkflow(VoiceWorkflow.Parse("447700900000"));
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartWhatsAppInteractiveVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeWhatsAppInteractiveWorkflow));
            var result =
                await this.StartVerificationAsyncWithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"));
            VerifyResponseBody(result);
        }

        [Fact]
        public async Task StartWhatsAppVerification()
        {
            this.InitializeWireMock(nameof(SerializationTest.ShouldSerializeWhatsAppWorkflow));
            var result = await this.StartVerificationAsyncWithWorkflow(WhatsAppWorkflow.Parse("447700900000"));
            VerifyResponseBody(result);
        }

        private void InitializeWireMock(string bodyScenario) =>
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/verify")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(bodyScenario))
                    .UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));

        private Task<Result<StartVerificationResponse>> StartVerificationAsyncWithWorkflow<T>(Result<T> workflow)
            where T : IVerificationWorkflow =>
            this.Helper.VonageClient.VerifyV2Client.StartVerificationAsync(StartVerificationRequest
                .Build()
                .WithBrand("ACME, Inc")
                .WithWorkflow(workflow)
                .Create());

        private static void VerifyResponseBody(Result<StartVerificationResponse> response) =>
            response.Should()
                .BeSuccess(new StartVerificationResponse(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315"),
                    Maybe<Uri>.None));
    }
}