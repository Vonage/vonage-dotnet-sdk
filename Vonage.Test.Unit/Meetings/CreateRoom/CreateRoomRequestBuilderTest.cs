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
        private readonly Room.Callback callback;
        private readonly Room.Features features;
        private readonly Room.JoinOptions joinOptions;
        private readonly Room.RecordingOptions recordingOptions;
        private readonly RoomApprovalLevel approvalLevel;
        private readonly RoomType roomType;
        private readonly string displayName;
        private readonly string metadata;
        private readonly string expiresAt;
        private readonly string themeId;

        public CreateRoomRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.displayName = fixture.Create<string>();
            this.metadata = fixture.Create<string>();
            this.roomType = fixture.Create<RoomType>();
            this.expiresAt = fixture.Create<string>();
            this.approvalLevel = fixture.Create<RoomApprovalLevel>();
            this.recordingOptions = fixture.Create<Room.RecordingOptions>();
            this.joinOptions = fixture.Create<Room.JoinOptions>();
            this.features = fixture.Create<Room.Features>();
            this.callback = fixture.Create<Room.Callback>();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            CreateRoomRequestBuilder
                .Build(this.displayName)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.Metadata.Should().BeNull();
                    success.Type.Should().BeNull();
                    success.ExpiresAt.Should().BeNull();
                    success.JoinApprovalLevel.Should().Be(RoomApprovalLevel.None);
                    success.RecordingOptions.Should().BeNull();
                    success.ExpireAfterUse.Should().BeFalse();
                    success.ThemeId.Should().BeNull();
                    success.CallbackUrls.Should().BeNull();
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.InitialJoinOptions.MicrophoneState.Should().Be(RoomMicrophoneState.Default);
                });

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

        [Fact]
        public void Build_ShouldReturnSuccess_() =>
            CreateRoomRequestBuilder
                .Build(this.displayName)
                .WithMetadata(this.metadata)
                .WithRoomType(this.roomType)
                .WithExpiresAt(this.expiresAt)
                .ExpireAfterUse()
                .WithThemeId(this.themeId)
                .WithApprovalLevel(this.approvalLevel)
                .WithRecordingOptions(this.recordingOptions)
                .WithInitialJoinOptions(this.joinOptions)
                .WithFeatures(this.features)
                .WithCallback(this.callback)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.DisplayName.Should().Be(this.displayName);
                    success.Metadata.Should().Be(this.metadata);
                    success.Type.Should().Be(this.roomType);
                    success.ExpiresAt.Should().Be(this.expiresAt);
                    success.ExpireAfterUse.Should().BeTrue();
                    success.ThemeId.Should().Be(this.themeId);
                    success.JoinApprovalLevel.Should().Be(this.approvalLevel);
                    success.RecordingOptions.Should().Be(this.recordingOptions);
                    success.InitialJoinOptions.Should().Be(this.joinOptions);
                    success.AvailableFeatures.Should().Be(this.features);
                    success.CallbackUrls.Should().Be(this.callback);
                });
    }
}