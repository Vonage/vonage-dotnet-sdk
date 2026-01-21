#region
using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Extensions;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.NumberInsightV2;
using Vonage.Numbers;
using Vonage.NumberVerification;
using Vonage.Pricing;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.SimSwap;
using Vonage.SubAccounts;
using Vonage.Users;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Video;
using Vonage.Video.Authentication;
using Vonage.Voice;
using Vonage.Voice.Emergency;
using Xunit;
#endregion

namespace Vonage.Test.Extensions
{
    [Trait("Category", "ServicesRegistration")]
    public class ServiceCollectionExtensionsTest
    {
        private readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"vonage:Vonage_key", "RandomValue"},
            })
            .Build();

        private readonly Credentials credentials = Credentials.FromApiKeyAndSecret("key", "secret");

        [Theory]
        [MemberData(nameof(GetRegisteredTypes))]
        public void AddVonageClientScoped_ShouldProvideScopedInstance_GivenConfigurationIsProvided(
            Type type)
        {
            var provider = BuildScopedProviderWithConfiguration(this.configuration);
            provider.GetRequiredService(type).Should().Be(provider.GetRequiredService(type));
        }

        [Theory]
        [MemberData(nameof(GetRegisteredTypes))]
        public void AddVonageClientScoped_ShouldProvideScopedInstance_GivenCredentialsAreProvided(
            Type type)
        {
            var provider = BuildScopedProviderWithCredentials(this.credentials);
            provider.GetRequiredService(type).Should().Be(provider.GetRequiredService(type));
        }

        [Theory]
        [MemberData(nameof(GetRegisteredTypes))]
        public void
            AddVonageClientTransient_ShouldProvideTransientInstance_GivenConfigurationIsProvided(
                Type type)
        {
            var provider = BuildTransientProviderWithConfiguration(this.configuration);
            provider.GetRequiredService(type).Should().NotBeNull();
        }

        [Theory]
        [MemberData(nameof(GetRegisteredTypes))]
        public void AddVonageClientTransient_ShouldProvideTransientInstance_GivenCredentialsAreProvided(
            Type type)
        {
            var provider = BuildTransientProviderWithCredentials(this.credentials);
            provider.GetRequiredService(type).Should().NotBeNull();
        }

        public static IEnumerable<object[]> GetRegisteredTypes()
        {
            yield return [typeof(VonageClient)];
            yield return [typeof(IAccountClient)];
            yield return [typeof(IApplicationClient)];
            yield return [typeof(IConversionClient)];
            yield return [typeof(IEmergencyClient)];
            yield return [typeof(IMessagesClient)];
            yield return [typeof(INumberInsightClient)];
            yield return [typeof(INumberInsightV2Client)];
            yield return [typeof(INumbersClient)];
            yield return [typeof(INumberVerificationClient)];
            yield return [typeof(IPricingClient)];
            yield return [typeof(IRedactClient)];
            yield return [typeof(ISimSwapClient)];
            yield return [typeof(IShortCodesClient)];
            yield return [typeof(ISubAccountsClient)];
            yield return [typeof(ISmsClient)];
            yield return [typeof(IUsersClient)];
            yield return [typeof(IVerifyClient)];
            yield return [typeof(IVerifyV2Client)];
            yield return [typeof(IVideoClient)];
            yield return [typeof(IVoiceClient)];
            yield return [typeof(ITokenGenerator)];
            yield return [typeof(IVideoTokenGenerator)];
            yield return [typeof(Credentials)];
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