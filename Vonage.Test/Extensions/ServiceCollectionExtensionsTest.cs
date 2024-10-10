using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vonage.Extensions;
using Vonage.NumberVerification;
using Vonage.Request;
using Vonage.SimSwap;
using Xunit;

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
            yield return new object[] {typeof(VonageClient)};
            yield return new object[] {typeof(INumberVerificationClient)};
            yield return new object[] {typeof(ISimSwapClient)};
            yield return new object[] {typeof(ITokenGenerator)};
            yield return new object[] {typeof(Credentials)};
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