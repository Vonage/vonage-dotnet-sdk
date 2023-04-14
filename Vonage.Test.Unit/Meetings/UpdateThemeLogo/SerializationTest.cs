using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.Common;
using Vonage.Meetings.UpdateThemeLogo;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateThemeLogo
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void FinalizeLogo_ShouldSerialize() =>
            Result<FinalizeLogoRequest>
                .FromSuccess(new FinalizeLogoRequest(new Guid("ca63a155-d5f0-4131-9903-c59907e53df0"), "logo-key1"))
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void GetLogos_ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetUploadLogosUrlResponse[]>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Should().HaveCount(1);
                    success[0].Url.Should().Be(new Uri("https://storage-url.com"));
                    success[0].Fields.ContentType.Should().Be("image/png");
                    success[0].Fields.Key.Should()
                        .Be("auto-expiring-temp/logos/white/ca63a155-d5f0-4131-9903-c59907e53df0");
                    success[0].Fields.LogoType.Should().Be(ThemeLogoType.White);
                    success[0].Fields.Bucket.Should().Be("string");
                    success[0].Fields.Policy.Should().Be("string");
                    success[0].Fields.AmazonCredential.Should().Be("string");
                    success[0].Fields.AmazonAlgorithm.Should().Be("string");
                    success[0].Fields.AmazonDate.Should().Be("string");
                    success[0].Fields.AmazonSignature.Should().Be("string");
                    success[0].Fields.AmazonSecurityToken.Should().Be("string");
                });
    }
}