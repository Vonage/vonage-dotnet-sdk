using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Pricing
{
    public interface IPricingClient
    {
        /// <summary>
        /// Retrieves the pricing information based on the specified country.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Country RetrievePricingCountry(string type, PricingCountryRequest request, Credentials creds = null);

        /// <summary>
        /// Retrieves the pricing information based on the specified country.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<Country> RetrievePricingCountryAsync(string type, PricingCountryRequest request, Credentials creds = null);

        /// <summary>
        /// Retrieves the pricing information for all countries.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="creds"></param>
        /// <returns></returns>
        PricingResult RetrievePricingAllCountries(string type, Credentials creds = null);

        /// <summary>
        /// Retrieves the pricing information for all countries.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<PricingResult> RetrievePricingAllCountriesAsync(string type, Credentials creds = null);

        /// <summary>
        /// Retrieves the pricing information based on the dialing prefix.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        PricingResult RetrievePrefixPricing(string type, PricingPrefixRequest request, Credentials creds = null);

        /// <summary>
        /// Retrieves the pricing information based on the dialing prefix.
        /// </summary>
        /// <param name="type">The type of service you wish to retrieve data about: 
        /// either sms, sms-transit or voice.</param>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<PricingResult> RetrievePrefixPricingAsync(string type, PricingPrefixRequest request, Credentials creds = null);
    }
}