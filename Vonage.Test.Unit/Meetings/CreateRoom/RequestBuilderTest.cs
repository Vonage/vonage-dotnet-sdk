using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.Common;
using Vonage.Meetings.CreateRoom;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Test.Unit.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateRoom
{
    public class RequestBuilderTest
    {
        private readonly Room.Callback callback;
        private readonly DateTime expiresAt;
        private readonly Room.Features features;
        private readonly Room.JoinOptions joinOptions;
        private readonly Room.RecordingOptions recordingOptions;
        private readonly RoomApprovalLevel approvalLevel;
        private readonly string displayName;
        private readonly string metadata;
        private readonly string themeId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.displayName = fixture.Create<string>();
            this.metadata = fixture.Create<string>();
            this.expiresAt = fixture.Create<DateTime>();
            this.approvalLevel = fixture.Create<RoomApprovalLevel>();
            this.recordingOptions = fixture.Create<Room.RecordingOptions>();
            this.joinOptions = fixture.Create<Room.JoinOptions>();
            this.features = fixture.Create<Room.Features>();
            this.callback = fixture.Create<Room.Callback>();
            this.themeId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveDefaultValues() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName(this.displayName)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.Metadata.Should().BeNone();
                    success.Type.Should().Be(RoomType.Instant);
                    success.ExpiresAt.Should().BeNone();
                    success.JoinApprovalLevel.Should().BeNone();
                    success.RecordingOptions.Should().BeNone();
                    success.ExpireAfterUse.Should().BeFalse();
                    success.ThemeId.Should().BeNone();
                    success.CallbackUrls.Should().BeNone();
                    success.AvailableFeatures.IsChatAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsRecordingAvailable.Should().BeTrue();
                    success.AvailableFeatures.IsWhiteboardAvailable.Should().BeTrue();
                    success.InitialJoinOptions.MicrophoneState.Should().Be(RoomMicrophoneState.Default);
                });

        [Fact]
        public void Build_ShouldReturnFailure_GivenDisplayNameExceeds200Length() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName(StringHelper.GenerateString(201))
                .Create()
                .Should()
                .BeParsingFailure("DisplayName length cannot be higher than 200.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenDisplayNameIsNullOrWhitespace(string invalidDisplayName) =>
            CreateRoomRequest
                .Build()
                .WithDisplayName(invalidDisplayName)
                .Create()
                .Should()
                .BeParsingFailure("DisplayName cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenMetadataExceeds500Length() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName(this.displayName)
                .WithMetadata(StringHelper.GenerateString(501))
                .Create()
                .Should()
                .BeParsingFailure("Metadata length cannot be higher than 500.");

        [Fact]
        public void Build_ShouldReturnSuccess_() =>
            CreateRoomRequest
                .Build()
                .WithDisplayName(this.displayName)
                .WithMetadata(this.metadata)
                .AsLongTermRoom(this.expiresAt)
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
                    success.Metadata.Should().BeSome(this.metadata);
                    success.Type.Should().Be(RoomType.LongTerm);
                    success.ExpiresAt.Should().BeSome(this.expiresAt);
                    success.ExpireAfterUse.Should().BeTrue();
                    success.ThemeId.Should().BeSome(this.themeId);
                    success.JoinApprovalLevel.Should().BeSome(this.approvalLevel);
                    success.RecordingOptions.Should().BeSome(this.recordingOptions);
                    success.InitialJoinOptions.Should().Be(this.joinOptions);
                    success.AvailableFeatures.Should().Be(this.features);
                    success.CallbackUrls.Should().BeSome(this.callback);
                });
    }
}