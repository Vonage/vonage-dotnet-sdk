using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions;
using Xunit;

namespace Vonage.Test.Video.Sessions;

public class IpAddressTest
{
    [Fact]
    public void Empty_ShouldReturnEmptyAddress() =>
        IpAddress.Empty
            .Address
            .Should()
            .Be(string.Empty);

    [Fact]
    public void Empty_ShouldReturnLocalhostAddress() =>
        IpAddress.Localhost
            .Address
            .Should()
            .Be("localhost");

    [Fact]
    public void Parse_ShouldReturnNone_GivenAddressCannotBeParsed() =>
        IpAddress
            .Parse("0.0.1.2.3.45.5")
            .Should()
            .BeParsingFailure("Unable to parse location '0.0.1.2.3.45.5'.");

    [Fact]
    public void Parse_ShouldReturnSome_GivenAddressCanBeParsed() =>
        IpAddress
            .Parse("192.168.1.26")
            .Map(address => address.Address)
            .Should()
            .BeSuccess("192.168.1.26");

    [Fact]
    public void Parse_ShouldReturnSome_GivenAddressIsEmpty() =>
        IpAddress
            .Parse(string.Empty)
            .Map(address => address.Address)
            .Should()
            .BeSuccess(string.Empty);

    [Fact]
    public void Parse_ShouldReturnSome_GivenAddressIsLocalhost() =>
        IpAddress
            .Parse("localhost")
            .Map(address => address.Address)
            .Should()
            .BeSuccess("localhost");
}