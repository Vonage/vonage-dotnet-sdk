using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Numbers
{
    public interface INumbersClient
    {
        /// <summary>
        /// Retrieve all the inbound numbers associated with your Vonage account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumbersSearchResponse> GetOwnedNumbersAsync(NumberSearchRequest request, Credentials creds = null);

        /// <summary>
        /// Retrieve inbound numbers that are available for the specified country.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumbersSearchResponse> GetAvailableNumbersAsync(NumberSearchRequest request, Credentials creds = null);

        /// <summary>
        /// Request to purchase a specific inbound number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumberTransactionResponse> BuyANumberAsync(NumberTransactionRequest request, Credentials creds = null);

        /// <summary>
        /// Cancel your subscription for a specific inbound number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumberTransactionResponse> CancelANumberAsync(NumberTransactionRequest request, Credentials creds = null);

        /// <summary>
        /// Change the behaviour of a number that you own.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumberTransactionResponse> UpdateANumberAsync(UpdateNumberRequest request, Credentials creds = null);
        
        /// <summary>
        /// Transfer a number that you own to a subaccount.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiKey"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<NumberTransferResponse> TransferANumberAsync(NumberTransferRequest request, string apiKey, Credentials creds = null);

        /// <summary>
        /// Retrieve all the inbound numbers associated with your Vonage account.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null);

        /// <summary>
        /// Retrieve inbound numbers that are available for the specified country.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null);

        /// <summary>
        /// Request to purchase a specific inbound number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null);

        /// <summary>
        /// Cancel your subscription for a specific inbound number.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null);

        /// <summary>
        /// Change the behaviour of a number that you own.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null);
        
        /// <summary>
        /// Transfer a number that you own to a subaccount.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="apiKey"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        NumberTransferResponse TransferANumber(NumberTransferRequest request, string apiKey, Credentials creds = null);
    }
}