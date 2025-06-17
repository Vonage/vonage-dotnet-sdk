using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vonage.Request;
using Vonage.Video.Authentication;

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
        services.AddScoped(_ => credentials);
        services.AddScoped(_ => new VonageClient(credentials));
        RegisterScopedServices(services);
        return services;
    }

    /// <summary>
    ///     Adds a scoped service of <see cref="VonageClient" />, and all api-specific clients, to the specified
    ///     IServiceCollection.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">The configuration properties to use.</param>
    /// <returns>The updated services.</returns>
    public static IServiceCollection AddVonageClientScoped(this IServiceCollection services,
        IConfiguration configuration)
    {
        var vonageConfiguration = Configuration.FromConfiguration(configuration);
        services.AddScoped(_ => vonageConfiguration.BuildCredentials());
        services.AddScoped(_ => new VonageClient(vonageConfiguration));
        RegisterScopedServices(services);
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
        services.AddTransient(_ => credentials);
        services.AddTransient(_ => new VonageClient(credentials));
        RegisterTransientServices(services);
        return services;
    }

    /// <summary>
    ///     Adds a transient service of <see cref="VonageClient" />, and all api-specific clients, to the specified
    ///     IServiceCollection.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="configuration">The configuration properties to use.</param>
    /// <returns>The updated services.</returns>
    public static IServiceCollection AddVonageClientTransient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var vonageConfiguration = Configuration.FromConfiguration(configuration);
        services.AddTransient(_ => vonageConfiguration.BuildCredentials());
        services.AddTransient(_ => new VonageClient(vonageConfiguration));
        RegisterTransientServices(services);
        return services;
    }

    private static void RegisterScopedServices(IServiceCollection services)
    {
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().AccountClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ApplicationClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ConversionClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().MessagesClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightV2Client);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumbersClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumberVerificationClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().PricingClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ProactiveConnectClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().RedactClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().SimSwapClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().ShortCodesClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().SubAccountsClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().SmsClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().UsersClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyV2Client);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VideoClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().VoiceClient);
        services.AddScoped<ITokenGenerator>(_ => new Jwt());
        services.AddScoped<IVideoTokenGenerator>(_ => new VideoTokenGenerator());
    }

    private static void RegisterTransientServices(IServiceCollection services)
    {
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().AccountClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ApplicationClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ConversionClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().MessagesClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumberInsightV2Client);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumbersClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumberVerificationClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().PricingClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ProactiveConnectClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().RedactClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().SimSwapClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().ShortCodesClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().SubAccountsClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().SmsClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().UsersClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VerifyV2Client);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VideoClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().VoiceClient);
        services.AddTransient<ITokenGenerator>(_ => new Jwt());
        services.AddTransient<IVideoTokenGenerator>(_ => new VideoTokenGenerator());
    }
}