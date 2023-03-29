using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.StartVerification;
using Vonage.VerifyV2.StartVerification.WhatsAppInteractive;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.StartVerification.WhatsAppInteractive
{
    public class StartWhatsAppInteractiveVerificationSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public StartWhatsAppInteractiveVerificationSerializationTest() =>
            this.helper = new SerializationTestHelper(
                typeof(StartWhatsAppInteractiveVerificationSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<StartVerificationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success => success.RequestId.Should().Be(new Guid("c11236f4-00bf-4b89-84ba-88b25df97315")));

        [Fact]
        public void ShouldSerialize() =>
            StartVerificationRequestBuilder.ForWhatsAppInteractive()
                .WithBrand("ACME, Inc")
                .WithWorkflow(WhatsAppInteractiveWorkflow.Parse("447700900000"))
                .WithLocale(Locale.EsEs)
                .WithChannelTimeout(300)
                .WithClientReference("my-personal-reference")
                .WithCodeLength(4)
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}