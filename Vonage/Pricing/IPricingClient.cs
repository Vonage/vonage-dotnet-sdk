using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing;

/// <summary>
///     Exposes Pricing API features for retrieving outbound pricing information for SMS and voice services.
/// </summary>
public interface IPricingClient
{
    /// <summary>
    ///     Retrieves the pricing information for a specific country.
    /// </summary>
    /// <param name="type">The type of service: "sms", "sms-transit", or "voice".</param>
    /// <param name="request">The request containing the country code. See <see cref="PricingCountryRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="Country"/> containing pricing details and network information for the specified country.</returns>
    /// <example>
    /// <code>
    /// var request = new PricingCountryRequest { Country = "US" };
    /// var pricing = await client.PricingClient.RetrievePricingCountryAsync("sms", request);
    /// Console.WriteLine($"Country: {pricing.Name}, Default Price: {pricing.DefaultPrice}");
    /// </code>
    /// </example>
    Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request, Credentials creds = null);

    /// <summary>
    ///     Retrieves the pricing information for all countries.
    /// </summary>
    /// <param name="type">The type of service: "sms", "sms-transit", or "voice".</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="PricingResult"/> containing pricing details for all available countries.</returns>
    /// <example>
    /// <code>
    /// var pricing = await client.PricingClient.RetrievePricingAllCountriesAsync("voice");
    /// foreach (var country in pricing.Countries)
    /// {
    ///     Console.WriteLine($"{country.Name}: {country.DefaultPrice}");
    /// }
    /// </code>
    /// </example>
    Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null);

    /// <summary>
    ///     Retrieves the pricing information for countries matching a dialing prefix.
    /// </summary>
    /// <param name="type">The type of service: "sms", "sms-transit", or "voice".</param>
    /// <param name="request">The request containing the dialing prefix. See <see cref="PricingPrefixRequest"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="PricingResult"/> containing pricing details for countries matching the prefix.</returns>
    /// <example>
    /// <code>
    /// var request = new PricingPrefixRequest { Prefix = "44" };
    /// var pricing = await client.PricingClient.RetrievePrefixPricingAsync("sms", request);
    /// foreach (var country in pricing.Countries)
    /// {
    ///     Console.WriteLine($"{country.Name}: {country.DefaultPrice}");
    /// }
    /// </code>
    /// </example>
    Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request, Credentials creds = null);
}