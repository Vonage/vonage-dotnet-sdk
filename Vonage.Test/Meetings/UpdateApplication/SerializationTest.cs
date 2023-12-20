using System;
using FluentAssertions;
using Vonage.Meetings.UpdateApplication;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.UpdateApplication
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<UpdateApplicationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyApplication);

        [Fact]
        public void ShouldSerialize() =>
            UpdateApplicationRequest
                .Parse(new Guid("e86a7335-35fe-45e1-b961-5777d4748022"))
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        internal static void VerifyApplication(UpdateApplicationResponse success)
        {
            success.AccountId.Should().Be("string");
            success.ApplicationId.Should().Be(new Guid("48ac72d0-a829-4896-a067-dcb1c2b0f30c"));
            success.DefaultThemeId.Should().Be(new Guid("e86a7335-35fe-45e1-b961-5777d4748022"));
        }
    }
}