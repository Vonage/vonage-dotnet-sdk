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

    internal static void ShouldMatchExpectedBasicResponse(this BasicInsightResponse actual)
    {
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
    }

    internal static void ShouldMatchExpectedStandardResponse(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.ShouldHaveExpectedRoaming("Acme Inc", "12345", "US", RoamingStatus.Roaming);
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    internal static void ShouldMatchExpectedStandardResponseWithNullCarrier(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.Roaming.Status.Should().Be(RoamingStatus.Unknown);
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, null, null, null, null);
    }

    internal static void ShouldMatchExpectedStandardResponseWithoutRoaming(this StandardInsightResponse actual)
    {
        actual.ShouldHaveExpectedStandardProperties();
        actual.Roaming.Status.Should().Be(RoamingStatus.Unknown);
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    internal static void ShouldMatchExpectedAdvancedResponse(this AdvancedInsightsResponse actual)
    {
        actual.Reachable.Should().Be(NumberReachability.Reachable);
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardCallerInfo(actual);
        actual.ShouldHaveExpectedCallerId("John", "Smith", "John Smith", CallerType.consumer);
        actual.ShouldHaveExpectedRoaming("Acme Inc", "12345", "US", RoamingStatus.Roaming);
        actual.Ported.Should().Be(PortedStatus.NotPorted);
        actual.ShouldHaveExpectedCarrier(actual.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    internal static void ShouldMatchExpectedAdvancedResponseWithNullableValues(this AdvancedInsightsResponse actual)
    {
        actual.Reachable.Should().BeNull();
        actual.ValidNumber.Should().Be(NumberValidity.valid);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardCallerInfo(actual);
        actual.ShouldHaveExpectedCallerId("John", "Smith", "John Smith", CallerType.consumer);
        actual.Roaming.Should().BeNull();
        actual.Ported.Should().BeNull();
        actual.ShouldHaveExpectedCarrier(actual.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
    }

    internal static void ShouldMatchExpectedAdvancedResponseWithoutRoaming(this AdvancedInsightsResponse actual)
    {
        AssertStatusSuccess(actual);
        AssertLookupOutcomeSuccess(actual);
        AssertStandardPhoneFormat(actual);
        actual.RequestPrice.Should().Be("0.04000000");
        actual.RemainingBalance.Should().Be("1.23456789");
        actual.ShouldHaveExpectedCarrier(actual.CurrentCarrier, "12345", "Acme Inc", "GB", "mobile");
        actual.ShouldHaveExpectedCarrier(actual.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
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

    internal static void ShouldHaveExpectedStandardProperties(this StandardInsightResponse actual)
    {
        AssertStandardCallerInfo(actual);
        actual.ShouldHaveExpectedCallerId("John", "Smith", "John Smith", CallerType.consumer);
        actual.Ported.Should().Be(PortedStatus.NotPorted);
        actual.ShouldHaveExpectedCarrier(actual.OriginalCarrier, "12345", "Acme Inc", "GB", "mobile");
        AssertStatusSuccess(actual);
        AssertStandardPhoneFormat(actual);
        AssertStandardPricing(actual);
    }

    internal static void ShouldHaveExpectedCallerId(this AdvancedInsightsResponse actual, string firstName,
        string lastName, string callerName, CallerType callerType)
    {
        actual.CallerIdentity.FirstName.Should().Be(firstName);
        actual.CallerIdentity.LastName.Should().Be(lastName);
        actual.CallerIdentity.CallerName.Should().Be(callerName);
        actual.CallerIdentity.CallerType.Should().Be(callerType);
    }

    internal static void ShouldHaveExpectedCallerId(this StandardInsightResponse actual, string firstName,
        string lastName, string callerName, CallerType callerType)
    {
        actual.CallerIdentity.FirstName.Should().Be(firstName);
        actual.CallerIdentity.LastName.Should().Be(lastName);
        actual.CallerIdentity.CallerName.Should().Be(callerName);
        actual.CallerIdentity.CallerType.Should().Be(callerType);
    }

    internal static void ShouldHaveExpectedCarrier(this AdvancedInsightsResponse actual, Carrier carrier,
        string networkCode, string name, string country, string networkType)
    {
        carrier.NetworkCode.Should().Be(networkCode);
        carrier.Name.Should().Be(name);
        carrier.Country.Should().Be(country);
        carrier.NetworkType.Should().Be(networkType);
    }

    internal static void ShouldHaveExpectedCarrier(this StandardInsightResponse actual, Carrier carrier,
        string networkCode, string name, string country, string networkType)
    {
        carrier.NetworkCode.Should().Be(networkCode);
        carrier.Name.Should().Be(name);
        carrier.Country.Should().Be(country);
        carrier.NetworkType.Should().Be(networkType);
    }

    internal static void ShouldHaveExpectedRoaming(this AdvancedInsightsResponse actual, string networkName,
        string networkCode, string countryCode, RoamingStatus status)
    {
        actual.Roaming.RoamingNetworkName.Should().Be(networkName);
        actual.Roaming.RoamingNetworkCode.Should().Be(networkCode);
        actual.Roaming.RoamingCountryCode.Should().Be(countryCode);
        actual.Roaming.Status.Should().Be(status);
    }

    internal static void ShouldHaveExpectedRoaming(this StandardInsightResponse actual, string networkName,
        string networkCode, string countryCode, RoamingStatus status)
    {
        actual.Roaming.RoamingNetworkName.Should().Be(networkName);
        actual.Roaming.RoamingNetworkCode.Should().Be(networkCode);
        actual.Roaming.RoamingCountryCode.Should().Be(countryCode);
        actual.Roaming.Status.Should().Be(status);
    }
}