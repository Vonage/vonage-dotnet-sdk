using Microsoft.Extensions.Configuration;
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
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().NumberVerificationClient);
        services.AddScoped(serviceProvider => serviceProvider.GetService<VonageClient>().SimSwapClient);
        services.AddScoped<ITokenGenerator>(_ => new Jwt());
    }

    private static void RegisterTransientServices(IServiceCollection services)
    {
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().NumberVerificationClient);
        services.AddTransient(serviceProvider => serviceProvider.GetService<VonageClient>().SimSwapClient);
        services.AddTransient<ITokenGenerator>(_ => new Jwt());
    }
}