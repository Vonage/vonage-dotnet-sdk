using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Extensions;
using Vonage.Meetings;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.NumberInsightV2;
using Vonage.Numbers;
using Vonage.Pricing;
using Vonage.ProactiveConnect;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.SubAccounts;
using Vonage.Users;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Video;
using Vonage.Video.Authentication;
using Vonage.Voice;
using Xunit;

namespace Vonage.Test.Extensions
{
    [Trait("Category", "ServicesRegistration")]
    public class ServiceCollectionExtensionsTest
    {
        private readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"appSettings:Vonage_key", "RandomValue"},
            })
            .Build();

        private readonly Credentials credentials = Credentials.FromApiKeyAndSecret("key", "secret");

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void AddVonageClientScoped_ShouldProvideScopedSpecificClientInstance_GivenConfigurationIsProvided(
            Type type)
        {
            var provider = BuildScopedProviderWithConfiguration(this.configuration);
            provider.GetRequiredService(type).Should().Be(provider.GetRequiredService(type));
        }

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void AddVonageClientScoped_ShouldProvideScopedSpecificClientInstance_GivenCredentialsAreProvided(
            Type type)
        {
            var provider = BuildScopedProviderWithCredentials(this.credentials);
            provider.GetRequiredService(type).Should().Be(provider.GetRequiredService(type));
        }

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void
            AddVonageClientTransient_ShouldProvideTransientSpecificClientInstance_GivenConfigurationIsProvided(
                Type type)
        {
            var provider = BuildTransientProviderWithConfiguration(this.configuration);
            provider.GetRequiredService(type).Should()
                .NotBe(provider.GetRequiredService(type));
        }

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void AddVonageClientTransient_ShouldProvideTransientSpecificClientInstance_GivenCredentialsAreProvided(
            Type type)
        {
            var provider = BuildTransientProviderWithCredentials(this.credentials);
            provider.GetRequiredService(type).Should()
                .NotBe(provider.GetRequiredService(type));
        }

        public static IEnumerable<object[]> GetSpecificVonageClients()
        {
            yield return new object[] {typeof(VonageClient)};
            yield return new object[] {typeof(IAccountClient)};
            yield return new object[] {typeof(IApplicationClient)};
            yield return new object[] {typeof(IConversionClient)};
            yield return new object[] {typeof(IMeetingsClient)};
            yield return new object[] {typeof(IMessagesClient)};
            yield return new object[] {typeof(INumberInsightClient)};
            yield return new object[] {typeof(INumberInsightV2Client)};
            yield return new object[] {typeof(INumbersClient)};
            yield return new object[] {typeof(IPricingClient)};
            yield return new object[] {typeof(IProactiveConnectClient)};
            yield return new object[] {typeof(IRedactClient)};
            yield return new object[] {typeof(IShortCodesClient)};
            yield return new object[] {typeof(ISubAccountsClient)};
            yield return new object[] {typeof(ISmsClient)};
            yield return new object[] {typeof(IUsersClient)};
            yield return new object[] {typeof(IVerifyClient)};
            yield return new object[] {typeof(IVerifyV2Client)};
            yield return new object[] {typeof(IVideoClient)};
            yield return new object[] {typeof(IVoiceClient)};
            yield return new object[] {typeof(ITokenGenerator)};
            yield return new object[] {typeof(IVideoTokenGenerator)};
        }

        private static ServiceProvider BuildScopedProviderWithConfiguration(IConfiguration configuration) =>
            new ServiceCollection().AddVonageClientScoped(configuration).BuildServiceProvider();

        private static ServiceProvider BuildScopedProviderWithCredentials(Credentials credentials) =>
            new ServiceCollection().AddVonageClientScoped(credentials).BuildServiceProvider();

        private static ServiceProvider BuildTransientProviderWithConfiguration(IConfiguration configuration) =>
            new ServiceCollection().AddVonageClientTransient(configuration).BuildServiceProvider();

        private static ServiceProvider BuildTransientProviderWithCredentials(Credentials credentials) =>
            new ServiceCollection().AddVonageClientTransient(credentials).BuildServiceProvider();
    }
}