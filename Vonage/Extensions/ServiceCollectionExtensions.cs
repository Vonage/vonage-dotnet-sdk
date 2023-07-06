using Microsoft.Extensions.DependencyInjection;
using Vonage.Request;

namespace Vonage.Extensions;

/// <summary>
///     Extensions for IServiceCollection to register VonageClient.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Adds a scoped service of <see cref="VonageClient" />, and all api-specific clients, to the specified
    ///     IServiceCollection.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="credentials">Represents credentials for Vonage APIs.</param>
    /// <returns>The updated services.</returns>
    public static IServiceCollection AddVonageClientScoped(this IServiceCollection services, Credentials credentials)
    {
        services.AddScoped(_ => new VonageClient(credentials));
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().AccountClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ApplicationClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ConversionClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().MeetingsClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().MessagesClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumbersClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().PricingClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ProactiveConnectClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().RedactClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ShortCodesClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().SmsClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyV2Client);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VoiceClient);
        return services;
    }

    /// <summary>
    ///     Adds a transient service of <see cref="VonageClient" />, and all api-specific clients, to the specified
    ///     IServiceCollection.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="credentials">Represents credentials for Vonage APIs.</param>
    /// <returns>The updated services.</returns>
    public static IServiceCollection AddVonageClientTransient(this IServiceCollection services, Credentials credentials)
    {
        services.AddTransient(_ => new VonageClient(credentials));
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().AccountClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ApplicationClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ConversionClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().MeetingsClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().MessagesClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumbersClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().PricingClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ProactiveConnectClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().RedactClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ShortCodesClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().SmsClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyV2Client);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VoiceClient);
        return services;
    }
}