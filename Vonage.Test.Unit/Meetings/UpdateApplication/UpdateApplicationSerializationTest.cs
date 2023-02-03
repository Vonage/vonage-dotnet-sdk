using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateApplication;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateApplication
{
    public class UpdateApplicationSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public UpdateApplicationSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(UpdateApplicationSerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<UpdateApplicationResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.AccountId.Should().Be("string");
                    success.ApplicationId.Should().Be(new Guid("48ac72d0-a829-4896-a067-dcb1c2b0f30c"));
                    success.DefaultThemeId.Should().Be(new Guid("e86a7335-35fe-45e1-b961-5777d4748022"));
                });

        [Fact]
        public void ShouldSerialize() =>
            UpdateApplicationRequest
                .Parse("e86a7335-35fe-45e1-b961-5777d4748022")
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}