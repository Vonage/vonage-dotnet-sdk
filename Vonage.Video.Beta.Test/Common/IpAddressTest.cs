using FluentAssertions;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class IpAddressTest
    {
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

        [Fact]
        public void Parse_ShouldReturnNone_GivenAddressCannotBeParsed() =>
            IpAddress
                .Parse("0.0.1.2.3.45.5")
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Unable to parse location '0.0.1.2.3.45.5'."));

        [Fact]
        public void Parse_ShouldReturnSome_GivenAddressCanBeParsed() =>
            IpAddress
                .Parse("192.168.1.26")
                .Map(address => address.Address)
                .Should()
                .BeSuccess("192.168.1.26");

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
    }
}