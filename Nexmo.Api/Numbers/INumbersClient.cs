using Nexmo.Api.Request;

namespace Nexmo.Api.Numbers
{
    public interface INumbersClient
    {
        NumbersSearchResponse GetOwnedNumbers(NumberSearchRequest request, Credentials creds = null);

        NumbersSearchResponse GetAvailableNumbers(NumberSearchRequest request, Credentials creds = null);

        NumberTransactionResponse BuyANumber(NumberTransactionRequest request, Credentials creds = null);

        NumberTransactionResponse CancelANumber(NumberTransactionRequest request, Credentials creds = null);

        NumberTransactionResponse UpdateANumber(UpdateNumberRequest request, Credentials creds = null);
    }
}