using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions.Common;
using Vonage.Server.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.CreateSession
{
    public class CreateSessionRequestTest
    {
        [Fact]
        public void Default_ShouldReturnRequest()
        {
            CreateSessionRequest.Default.Location.Should().Be(IpAddress.Empty);
            CreateSessionRequest.Default.MediaMode.Should().Be(MediaMode.Relayed);
            CreateSessionRequest.Default.ArchiveMode.Should().Be(ArchiveMode.Manual);
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateSessionRequest.Default.GetEndpointPath()
                .Should()
                .Be("/session/create");

        [Theory]
        [InlineData("", MediaMode.Routed, ArchiveMode.Always, "location=&archiveMode=always&p2p.preference=disabled")]
        [InlineData("", MediaMode.Relayed, ArchiveMode.Manual, "location=&archiveMode=manual&p2p.preference=enabled")]
        [InlineData("192.168.1.1", MediaMode.Routed, ArchiveMode.Always,
            "location=192.168.1.1&archiveMode=always&p2p.preference=disabled")]
        [InlineData("localhost", MediaMode.Relayed, ArchiveMode.Manual,
            "location=localhost&archiveMode=manual&p2p.preference=enabled")]
        public void GetUrlEncoded(string location, MediaMode mediaMode, ArchiveMode archiveMode, string expected) =>
            CreateSessionRequest
                .Parse(location, mediaMode, archiveMode)
                .Map(request => request.GetUrlEncoded())
                .Should()
                .BeSuccess(expected);

        [Fact]
        public void Parse_ShouldReturnFailure_GivenLocationCannotBeParsed_WithStringConstructor() =>
            CreateSessionRequest
                .Parse("1.2.3.4.5", MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Unable to parse location '1.2.3.4.5'."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways() =>
            CreateSessionRequest
                .Parse(IpAddress.Empty, MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(CreateSessionRequest.IncompatibleMediaAndArchive));

        [Fact]
        public void
            Parse_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways_WithStringConstructor() =>
            CreateSessionRequest
                .Parse(string.Empty, MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage(CreateSessionRequest.IncompatibleMediaAndArchive));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            CreateSessionRequest
                .Parse(IpAddress.Empty, MediaMode.Routed, ArchiveMode.Always)
                .IsSuccess
                .Should()
                .BeTrue();

        [Fact]
        public void Parse_ShouldReturnSuccess_WithStringConstructor() =>
            CreateSessionRequest
                .Parse(string.Empty, MediaMode.Routed, ArchiveMode.Always)
                .IsSuccess
                .Should()
                .BeTrue();
    }
}