using Nexmo.Api.Request;

namespace Nexmo.Api.Numbers
{
    [System.Obsolete("The Nexmo.Api.Numbers.INumbersClient class is obsolete. " +
        "References to it should be switched to the new Vonage.Numbers.INumbersClient class.")]
    public interface INumbersClient
    {
        /// <summary>
        /// Retrieve all the inbound numbers associated with your Nexmo account.
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
    }
}