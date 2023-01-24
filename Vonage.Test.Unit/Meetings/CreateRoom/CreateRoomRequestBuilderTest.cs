using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class CreateRoomRequestBuilderTest
    {
        private readonly string displayName;

        public CreateRoomRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.displayName = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultAvailableFeature_GivenFeaturesWereNotProvided() =>
            CreateRoomRequestBuilder
                .Build(this.displayName)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                });

        [Fact]
        public void Build_ShouldHaveDefaultMicrophoneState_GivenMicrophoneStateWasNotProvided() =>
            CreateRoomRequestBuilder
                .Build(this.displayName)
                .Create()
                .Should()
                .BeSuccess(success =>
                    success.InitialJoinOptions.MicrophoneState.Should().Be(RoomMicrophoneState.Default));

        [Fact]
        public void Build_ShouldReturnFailure_GivenDisplayNameExceeds200Length() =>
            CreateRoomRequestBuilder
                .Build(StringHelper.GenerateString(201))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("DisplayName cannot be higher than 200."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenDisplayNameIsNullOrWhitespace(string invalidDisplayName) =>
            CreateRoomRequestBuilder
                .Build(invalidDisplayName)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("DisplayName cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenMetadataExceeds500Length() =>
            CreateRoomRequestBuilder
                .Build(this.displayName)
                .WithMetadata(StringHelper.GenerateString(501))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Metadata cannot be higher than 500."));
    }
}