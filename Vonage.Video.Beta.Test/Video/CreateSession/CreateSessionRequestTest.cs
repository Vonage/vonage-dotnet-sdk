using FluentAssertions;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Session;
using Vonage.Video.Beta.Video.Session.CreateSession;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.CreateSession
{
    public class CreateSessionRequestTest
    {
        [Fact]
        public void Parse_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways() =>
            CreateSessionRequest
                .Parse(IpAddress.Empty, MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .Be(Result<CreateSessionRequest>.FromFailure(
                    ResultFailure.FromErrorMessage(CreateSessionRequest.IncompatibleMediaAndArchive)));

        [Fact]
        public void Parse_ShouldReturnSuccess() =>
            CreateSessionRequest
                .Parse(IpAddress.Empty, MediaMode.Routed, ArchiveMode.Always)
                .IsSuccess
                .Should()
                .BeTrue();

        [Fact]
        public void
            Parse_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways_WithStringConstructor() =>
            CreateSessionRequest
                .Parse(string.Empty, MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .Be(Result<CreateSessionRequest>.FromFailure(
                    ResultFailure.FromErrorMessage(CreateSessionRequest.IncompatibleMediaAndArchive)));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenLocationCannotBeParsed_WithStringConstructor() =>
            CreateSessionRequest
                .Parse("1.2.3.4.5", MediaMode.Relayed, ArchiveMode.Always)
                .Should()
                .Be(Result<CreateSessionRequest>.FromFailure(
                    ResultFailure.FromErrorMessage("Unable to parse location '1.2.3.4.5'.")));

        [Fact]
        public void Parse_ShouldReturnSuccess_WithStringConstructor() =>
            CreateSessionRequest
                .Parse(string.Empty, MediaMode.Routed, ArchiveMode.Always)
                .IsSuccess
                .Should()
                .BeTrue();
    }
}