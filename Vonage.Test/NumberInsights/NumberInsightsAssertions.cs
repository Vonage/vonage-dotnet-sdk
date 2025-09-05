#region
using FluentAssertions;
using Vonage.NumberInsights;
using Xunit;
#endregion

namespace Vonage.Test.NumberInsights;

internal static class NumberInsightsAssertions
{
    private static void AssertLookupOutcomeSuccess(dynamic actual)
    {
        Assert.Equal(0, actual.LookupOutcome);
        Assert.Equal("Success", actual.LookupOutcomeMessage);
    }

    private static void AssertStandardCallerInfo(dynamic actual)
    {
        Assert.Equal("John", actual.FirstName);
        Assert.Equal(CallerType.consumer, actual.CallerType);
        Assert.Equal("Smith", actual.LastName);
        Assert.Equal("John Smith", actual.CallerName);
    }

    private static void AssertStandardPhoneFormat(dynamic actual)
    {
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", actual.RequestId);
        Assert.Equal("447700900000", actual.InternationalFormatNumber);
        Assert.Equal("07700 900000", actual.NationalFormatNumber);
        Assert.Equal("GB", actual.CountryCode);
        Assert.Equal("GBR", actual.CountryCodeIso3);
        Assert.Equal("United Kingdom", actual.CountryName);
        Assert.Equal("44", actual.CountryPrefix);
    }

    private static void AssertStandardPricing(dynamic actual)
    {
        Assert.Equal("0.04000000", actual.RequestPrice);
        Assert.Equal("0.01500000", actual.RefundPrice);
        Assert.Equal("1.23456789", actual.RemainingBalance);
    }

    private static void AssertStatusSuccess(dynamic actual)
    {
        Assert.Equal(0, actual.Status);
        Assert.Equal("Success", actual.StatusMessage);
    }

    private static void ShouldHaveExpectedCallerId(this CallerId actual, CallerId callerId)
    {
        actual.FirstName.Should().Be(callerId.FirstName);
        actual.LastName.Should().Be(callerId.LastName);
        actual.CallerName.Should().Be(callerId.CallerName);
        actual.CallerType.Should().Be(callerId.CallerType);
    }

    private static void ShouldHaveExpectedCarrier(this Carrier actual, Carrier carrier)
    {
        actual.NetworkCode.Should().Be(carrier.NetworkCode);
        actual.Name.Should().Be(carrier.Name);
        actual.Country.Should().Be(carrier.Country);
        actual.NetworkType.Should().Be(carrier.NetworkType);
    }

    private static void ShouldHaveExpectedRoaming(this Roaming actual, Roaming roaming)
    {
        actual.RoamingCountryCode.Should().Be(roaming.RoamingCountryCode);
        actual.RoamingNetworkCode.Should().Be(roaming.RoamingNetworkCode);
        actual.RoamingNetworkName.Should().Be(roaming.RoamingNetworkName);
        actual.Status.Should().Be(roaming.Status);
    }

    private static void ShouldHaveExpectedStandardProperties(this StandardInsightResponse actual)
    {
        AssertStandardCallerInfo(actual);
        actual.CallerIdentity.ShouldHaveExpectedCallerId(new CallerId
            {FirstName = "John", LastName = "Smith", CallerName = "John Smith", CallerType = CallerType.consumer});
        actual.Ported.Should().Be(PortedStatus.NotPorted);
        actual.OriginalCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
    }

    internal static void ShouldMatchExpectedBasicResponse(this BasicInsightResponse actual)
    {
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
    }

    internal static void ShouldMatchExpectedStandardResponse(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.Roaming.ShouldHaveExpectedRoaming(new Roaming
        {
            RoamingNetworkName = "Acme Inc", RoamingNetworkCode = "12345", RoamingCountryCode = "US",
            Status = RoamingStatus.Roaming,
        });
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
    }

    internal static void ShouldMatchExpectedStandardResponseWithNullCarrier(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.Roaming.Status.Should().Be(RoamingStatus.Unknown);
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier());
    }

    internal static void ShouldMatchExpectedStandardResponseWithoutRoaming(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.Roaming.Status.Should().Be(RoamingStatus.Unknown);
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
    }

    internal static void ShouldMatchExpectedAdvancedResponse(this AdvancedInsightsResponse actual)
    {
        actual.Reachable.Should().Be(NumberReachability.Reachable);
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardCallerInfo(actual);
        actual.CallerIdentity.ShouldHaveExpectedCallerId(new CallerId
            {FirstName = "John", LastName = "Smith", CallerName = "John Smith", CallerType = CallerType.consumer});
        actual.Roaming.ShouldHaveExpectedRoaming(new Roaming
        {
            RoamingNetworkName = "Acme Inc", RoamingNetworkCode = "12345", RoamingCountryCode = "US",
            Status = RoamingStatus.Roaming,
        });
        actual.Ported.Should().Be(PortedStatus.NotPorted);
        actual.OriginalCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
    }

    internal static void ShouldMatchExpectedAdvancedResponseWithNullableValues(this AdvancedInsightsResponse actual)
    {
        actual.Reachable.Should().BeNull();
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardCallerInfo(actual);
        actual.CallerIdentity.ShouldHaveExpectedCallerId(new CallerId
            {FirstName = "John", LastName = "Smith", CallerName = "John Smith", CallerType = CallerType.consumer});
        actual.Roaming.Should().BeNull();
        actual.Ported.Should().BeNull();
        actual.OriginalCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
    }

    internal static void ShouldMatchExpectedAdvancedResponseWithoutRoaming(this AdvancedInsightsResponse actual)
    {
        AssertStatusSuccess(actual);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardPhoneFormat(actual);
        actual.RequestPrice.Should().Be("0.04000000");
        actual.RemainingBalance.Should().Be("1.23456789");
        actual.OriginalCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
        actual.CurrentCarrier.ShouldHaveExpectedCarrier(new Carrier
        {
            NetworkCode = "12345", Name = "Acme Inc", Country = "GB", NetworkType = "mobile",
        });
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        actual.Reachable.Should().Be(NumberReachability.Reachable);
        actual.Ported.Should().Be(PortedStatus.NotPorted);
        actual.Roaming.Status.Should().Be(RoamingStatus.NotRoaming);
        actual.Roaming.RoamingNetworkName.Should().BeNull();
        actual.Roaming.RoamingCountryCode.Should().BeNull();
        actual.Roaming.RoamingNetworkCode.Should().BeNull();
    }

    internal static void ShouldMatchExpectedRealTimeDataResponse(this AdvancedInsightsResponse actual,
        bool expectedActiveStatus)
    {
        actual.Reachable.Should().Be(NumberReachability.Reachable);
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        actual.RealTimeData.Should().NotBeNull();
        actual.RealTimeData.HandsetStatus.Should().Be("on");
        actual.RealTimeData.ActiveStatus.Should().Be(expectedActiveStatus);
    }

    internal static void ShouldMatchExpectedAsyncResponse(this AdvancedInsightsAsynchronousResponse actual)
    {
        actual.RequestId.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        actual.Number.Should().Be("447700900000");
        actual.RemainingBalance.Should().Be("1.23456789");
        actual.RequestPrice.Should().Be("0.01500000");
        actual.Status.Should().Be(0);
    }
}