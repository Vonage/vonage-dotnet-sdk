using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Vonage.Accounts;
using Vonage.Applications;
using Vonage.Conversions;
using Vonage.Extensions;
using Vonage.Messages;
using Vonage.Messaging;
using Vonage.NumberInsights;
using Vonage.Numbers;
using Vonage.Pricing;
using Vonage.Redaction;
using Vonage.Request;
using Vonage.ShortCodes;
using Vonage.Verify;
using Vonage.VerifyV2;
using Vonage.Voice;
using Xunit;

namespace Vonage.Test.Unit.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        private readonly ServiceProvider transientProvider;
        private readonly ServiceProvider scopedProvider;

        public ServiceCollectionExtensionsTest()
        {
            var credentials = Credentials.FromApiKeyAndSecret("key", "secret");
            this.transientProvider =
                new ServiceCollection().AddVonageClientTransient(credentials).BuildServiceProvider();
            this.scopedProvider = new ServiceCollection().AddVonageClientScoped(credentials).BuildServiceProvider();
        }

        [Fact]
        public void AddVonageClientScoped_ShouldProvideScopedClientInstance() =>
            this.scopedProvider.GetRequiredService<VonageClient>().Should()
                .Be(this.scopedProvider.GetService<VonageClient>());

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void AddVonageClientScoped_ShouldProvideScopedSpecificClientInstance(Type type) =>
            this.scopedProvider.GetRequiredService(type).Should()
                .Be(this.scopedProvider.GetRequiredService(type));

        [Fact]
        public void AddVonageClientTransient_ShouldProvideTransientClientInstance() =>
            this.transientProvider.GetRequiredService<VonageClient>().Should()
                .NotBe(this.transientProvider.GetRequiredService<VonageClient>());

        [Theory]
        [MemberData(nameof(GetSpecificVonageClients))]
        public void AddVonageClientTransient_ShouldProvideTransientSpecificClientInstance(Type type) =>
            this.transientProvider.GetRequiredService(type).Should()
                .NotBe(this.transientProvider.GetRequiredService(type));

        public static IEnumerable<object[]> GetSpecificVonageClients()
        {
            yield return new object[] {typeof(IAccountClient)};
            yield return new object[] {typeof(IApplicationClient)};
            yield return new object[] {typeof(IConversionClient)};
            yield return new object[] {typeof(IMessagesClient)};
            yield return new object[] {typeof(INumberInsightClient)};
            yield return new object[] {typeof(INumbersClient)};
            yield return new object[] {typeof(IPricingClient)};
            yield return new object[] {typeof(IRedactClient)};
            yield return new object[] {typeof(IShortCodesClient)};
            yield return new object[] {typeof(ISmsClient)};
            yield return new object[] {typeof(IVerifyClient)};
            yield return new object[] {typeof(IVerifyV2Client)};
            yield return new object[] {typeof(IVoiceClient)};
        }
    }
}