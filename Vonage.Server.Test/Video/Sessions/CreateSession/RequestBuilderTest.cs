using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.CreateSession
{
    public class RequestBuilderTest
    {
        [Fact]
        public void Build_ShouldReturnFailure_GivenLocationCannotBeParsed_WithStringConstructor() =>
            CreateSessionRequest.Build()
                .WithLocation("1.2.3.4.5")
                .WithMediaMode(MediaMode.Relayed)
                .WithArchiveMode(ArchiveMode.Always)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Unable to parse location '1.2.3.4.5'."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways() =>
            CreateSessionRequest.Build()
                .WithLocation(IpAddress.Empty)
                .WithMediaMode(MediaMode.Relayed)
                .WithArchiveMode(ArchiveMode.Always)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    "A session with always archive mode must also have the routed media mode."));

        [Fact]
        public void
            Build_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways_WithStringConstructor() =>
            CreateSessionRequest.Build()
                .WithLocation(string.Empty)
                .WithMediaMode(MediaMode.Relayed)
                .WithArchiveMode(ArchiveMode.Always)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(
                    "A session with always archive mode must also have the routed media mode."));

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            CreateSessionRequest.Build()
                .WithLocation(IpAddress.Empty)
                .WithMediaMode(MediaMode.Routed)
                .WithArchiveMode(ArchiveMode.Always)
                .Create()
                .IsSuccess
                .Should()
                .BeTrue();

        [Fact]
        public void Build_ShouldReturnSuccess_WithStringConstructor() =>
            CreateSessionRequest.Build()
                .WithLocation(string.Empty)
                .WithMediaMode(MediaMode.Routed)
                .WithArchiveMode(ArchiveMode.Always)
                .Create()
                .IsSuccess
                .Should()
                .BeTrue();

        [Theory]
        [InlineData("", MediaMode.Routed, ArchiveMode.Always, "location=&archiveMode=always&p2p.preference=disabled")]
        [InlineData("", MediaMode.Relayed, ArchiveMode.Manual, "location=&archiveMode=manual&p2p.preference=enabled")]
        [InlineData("192.168.1.1", MediaMode.Routed, ArchiveMode.Always,
            "location=192.168.1.1&archiveMode=always&p2p.preference=disabled")]
        [InlineData("localhost", MediaMode.Relayed, ArchiveMode.Manual,
            "location=localhost&archiveMode=manual&p2p.preference=enabled")]
        public void GetUrlEncoded(string location, MediaMode mediaMode, ArchiveMode archiveMode, string expected) =>
            CreateSessionRequest.Build()
                .WithLocation(location)
                .WithMediaMode(mediaMode)
                .WithArchiveMode(archiveMode)
                .Create()
                .Map(request => request.GetUrlEncoded())
                .Should()
                .BeSuccess(expected);
    }
}