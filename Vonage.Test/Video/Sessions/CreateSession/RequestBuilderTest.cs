using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions;
using Vonage.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Test.Video.Sessions.CreateSession;

[Trait("Category", "Request")]
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
            .BeParsingFailure("Unable to parse location '1.2.3.4.5'.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways() =>
        CreateSessionRequest.Build()
            .WithLocation(IpAddress.Empty)
            .WithMediaMode(MediaMode.Relayed)
            .WithArchiveMode(ArchiveMode.Always)
            .Create()
            .Should()
            .BeParsingFailure(
                "A session with always archive mode must also have the routed media mode.");

    [Fact]
    public void
        Build_ShouldReturnFailure_GivenMediaRouteIsNotRoutedWhenArchiveModeIsAlways_WithStringConstructor() =>
        CreateSessionRequest.Build()
            .WithLocation(string.Empty)
            .WithMediaMode(MediaMode.Relayed)
            .WithArchiveMode(ArchiveMode.Always)
            .Create()
            .Should()
            .BeParsingFailure(
                "A session with always archive mode must also have the routed media mode.");

    [Fact]
    public void Build_ShouldReturnSuccess() =>
        CreateSessionRequest.Build()
            .WithLocation(IpAddress.Empty)
            .WithMediaMode(MediaMode.Routed)
            .WithArchiveMode(ArchiveMode.Always)
            .Create()
            .Should()
            .BeSuccess();

    [Fact]
    public void Build_ShouldSetEncryption_GivenEncryptionIsEnabled() =>
        CreateSessionRequest.Build()
            .WithLocation(IpAddress.Empty)
            .WithMediaMode(MediaMode.Routed)
            .WithArchiveMode(ArchiveMode.Always)
            .EnableEncryption()
            .Create()
            .Map(request => request.EndToEndEncryption)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldHaveEncryptionDisabled_GivenDefault() =>
        CreateSessionRequest.Build()
            .WithLocation(IpAddress.Empty)
            .WithMediaMode(MediaMode.Routed)
            .WithArchiveMode(ArchiveMode.Always)
            .Create()
            .Map(request => request.EndToEndEncryption)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldReturnSuccess_WithStringConstructor() =>
        CreateSessionRequest.Build()
            .WithLocation(string.Empty)
            .WithMediaMode(MediaMode.Routed)
            .WithArchiveMode(ArchiveMode.Always)
            .Create()
            .Should()
            .BeSuccess();

    [Theory]
    [InlineData("", MediaMode.Routed, ArchiveMode.Always,
        "location=&archiveMode=always&p2p.preference=disabled&e2ee=false")]
    [InlineData("", MediaMode.Relayed, ArchiveMode.Manual,
        "location=&archiveMode=manual&p2p.preference=enabled&e2ee=false")]
    [InlineData("192.168.1.1", MediaMode.Routed, ArchiveMode.Always,
        "location=192.168.1.1&archiveMode=always&p2p.preference=disabled&e2ee=false")]
    [InlineData("localhost", MediaMode.Relayed, ArchiveMode.Manual,
        "location=localhost&archiveMode=manual&p2p.preference=enabled&e2ee=false")]
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