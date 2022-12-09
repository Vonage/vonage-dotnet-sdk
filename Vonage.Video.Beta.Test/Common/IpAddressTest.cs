using FluentAssertions;
using Vonage.Video.Beta.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class IpAddressTest
    {
        [Fact]
        public void Parse_ShouldReturnSome_GivenAddressIsEmpty()
        {
            IpAddress
                .Parse(string.Empty)
                .Map(address => address.Address)
                .Should()
                .Be(Result<string>.FromSuccess(string.Empty));
        }

        [Fact]
        public void Parse_ShouldReturnSome_GivenAddressIsLocalhost() =>
            IpAddress
                .Parse("localhost")
                .Map(address => address.Address)
                .Should()
                .Be(Result<string>.FromSuccess("localhost"));

        [Fact]
        public void Parse_ShouldReturnNone_GivenAddressCannotBeParsed() =>
            IpAddress
                .Parse("0.0.1.2.3.45.5")
                .Should()
                .Be(Result<IpAddress>.FromFailure(
                    ResultFailure.FromErrorMessage("Unable to parse location '0.0.1.2.3.45.5'.")));

        [Fact]
        public void Parse_ShouldReturnSome_GivenAddressCanBeParsed() =>
            IpAddress
                .Parse("192.168.1.26")
                .Map(address => address.Address)
                .Should()
                .Be(Result<string>.FromSuccess("192.168.1.26"));

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