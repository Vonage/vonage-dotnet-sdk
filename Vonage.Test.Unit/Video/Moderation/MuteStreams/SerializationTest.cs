using System;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Test.Unit.Video.Moderation.MuteStreams
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper =
                new SerializationTestHelper(typeof(SerializationTest).Namespace,
                    JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer.DeserializeObject<MuteStreamsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyResponse);

        [Fact]
        public void ShouldSerialize() =>
            MuteStreamsRequest.Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                .WithConfiguration(
                    new MuteStreamsRequest.MuteStreamsConfiguration(true, new[] {"excludedStream1", "excludedStream2"}))
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        internal static void VerifyResponse(MuteStreamsResponse success)
        {
            success.ApplicationId.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
            success.Status.Should().Be("ACTIVE");
            success.Name.Should().Be("Joe Montana");
            success.Environment.Should().Be("standard");
            success.CreatedAt.Should().Be(1414642898000);
        }
    }
}