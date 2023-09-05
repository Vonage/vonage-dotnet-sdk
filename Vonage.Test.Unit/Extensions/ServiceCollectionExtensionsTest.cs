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
using Vonage.Numbers;
using Vonage.Pricing;
using Vonage.ProactiveConnect;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.Users;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Voice;
using Xunit;

namespace Vonage.Test.Unit.Extensions
{
	public class ServiceCollectionExtensionsTest
	{
		private readonly Credentials credentials = Credentials.FromApiKeyAndSecret("key", "secret");

		private readonly IConfigurationRoot configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>
			{
				{"appSettings:Vonage_key", "RandomValue"},
			})
			.Build();

		[Fact]
		public void AddVonageClientScoped_ShouldProvideScopedClientInstance_GivenConfigurationIsProvided()
		{
			var provider = BuildScopedProviderWithConfiguration(configuration);
			provider.GetRequiredService<VonageClient>().Should().Be(provider.GetRequiredService<VonageClient>());
		}

		[Fact]
		public void AddVonageClientScoped_ShouldProvideScopedClientInstance_GivenCredentialsAreProvided()
		{
			var provider = BuildScopedProviderWithCredentials(this.credentials);
			provider.GetRequiredService<VonageClient>().Should().Be(provider.GetRequiredService<VonageClient>());
		}

		[Theory]
		[MemberData(nameof(GetSpecificVonageClients))]
		public void AddVonageClientScoped_ShouldProvideScopedSpecificClientInstance_GivenConfigurationIsProvided(
			Type type)
		{
			var provider = BuildScopedProviderWithConfiguration(configuration);
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

		[Fact]
		public void AddVonageClientTransient_ShouldProvideTransientClientInstance_GivenConfigurationIsProvided()
		{
			var provider = BuildTransientProviderWithConfiguration(configuration);
			provider.GetRequiredService<VonageClient>().Should()
				.NotBe(provider.GetRequiredService<VonageClient>());
		}

		[Fact]
		public void AddVonageClientTransient_ShouldProvideTransientClientInstance_GivenCredentialsAreProvided()
		{
			var provider = BuildTransientProviderWithCredentials(this.credentials);
			provider.GetRequiredService<VonageClient>().Should()
				.NotBe(provider.GetRequiredService<VonageClient>());
		}

		[Theory]
		[MemberData(nameof(GetSpecificVonageClients))]
		public void
			AddVonageClientTransient_ShouldProvideTransientSpecificClientInstance_GivenConfigurationIsProvided(
				Type type)
		{
			var provider = BuildTransientProviderWithConfiguration(configuration);
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
			yield return new object[] {typeof(IAccountClient)};
			yield return new object[] {typeof(IApplicationClient)};
			yield return new object[] {typeof(IConversionClient)};
			yield return new object[] {typeof(IMeetingsClient)};
			yield return new object[] {typeof(IMessagesClient)};
			yield return new object[] {typeof(INumberInsightClient)};
			yield return new object[] {typeof(INumbersClient)};
			yield return new object[] {typeof(IPricingClient)};
			yield return new object[] {typeof(IProactiveConnectClient)};
			yield return new object[] {typeof(IRedactClient)};
			yield return new object[] {typeof(IShortCodesClient)};
			yield return new object[] {typeof(ISmsClient)};
			yield return new object[] {typeof(IUsersClient)};
			yield return new object[] {typeof(IVerifyClient)};
			yield return new object[] {typeof(IVerifyV2Client)};
			yield return new object[] {typeof(IVoiceClient)};
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