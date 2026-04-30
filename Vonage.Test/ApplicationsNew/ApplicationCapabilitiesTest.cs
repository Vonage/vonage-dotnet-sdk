using FluentAssertions;
using Vonage.ApplicationsNew;
using Vonage.ApplicationsNew.Capabilities;
using Xunit;

namespace Vonage.Test.ApplicationsNew;

[Trait("Category", "Unit")]
[Trait("Product", "ApplicationsNew")]
public class ApplicationCapabilitiesTest
{
    [Fact]
    public void HasCapabilities_ShouldReturnFalse_WhenNoCapabilityIsSet() =>
        new ApplicationCapabilities().HasCapabilities.Should().BeFalse();

    public static TheoryData<ApplicationCapabilities> SingleCapabilityInstances => new()
    {
        new ApplicationCapabilities {Voice = new VoiceCapability()},
        new ApplicationCapabilities {Messages = new MessagesCapability()},
        new ApplicationCapabilities {Rtc = new RtcCapability()},
        new ApplicationCapabilities {Vbc = new VbcCapability()},
        new ApplicationCapabilities {NetworkApis = new NetworkApisCapability()},
        new ApplicationCapabilities {Meetings = new MeetingsCapability()},
        new ApplicationCapabilities {Verify = new VerifyCapability()},
        new ApplicationCapabilities {Video = new VideoCapability()},
    };

    [Theory]
    [MemberData(nameof(SingleCapabilityInstances))]
    public void HasCapabilities_ShouldReturnTrue_WhenAnyCapabilityIsSet(ApplicationCapabilities capabilities) =>
        capabilities.HasCapabilities.Should().BeTrue();
}
