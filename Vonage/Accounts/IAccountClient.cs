using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Accounts;

public interface IAccountClient
{
    /// <summary>
    /// Retrieve the current balance of your Vonage API account
    /// </summary>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Balance> GetAccountBalanceAsync(Credentials creds = null);

    /// <summary>
    /// You can top up your account using this API when you have enabled auto-reload in the dashboard. 
    /// The amount added by the top-up operation will be the same amount as was added in the payment 
    /// when auto-reload was enabled. Your account balance is checked every 5-10 minutes and if it falls 
    /// below the threshold and auto-reload is enabled, then it will be topped up automatically. Use this 
    /// endpoint if you need to top up at times when your credit may be exhausted more quickly than the auto-reload may occur.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<TopUpResult> TopUpAccountBalanceAsync(TopUpRequest request, Credentials creds = null);

    /// <summary>
    /// Update the default callback URLs (where the webhooks are sent to) associated with your account: 
    /// * Callback URL for incoming SMS messages * Callback URL for delivery receipts        
    /// Note: that the URLs you provide must be valid and active. Vonage will check that they return a 200 OK response before the setting is saved
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<AccountSettingsResult> ChangeAccountSettingsAsync(AccountSettingsRequest request, Credentials creds = null);

    /// <summary>
    /// Many of Vonage's APIs are accessed using an API key and secret. It is recommended that you change or "rotate" 
    /// your secrets from time to time for security purposes. This section provides the API interface for achieving this. 
    /// Note: to work on secrets for your secondary accounts, you may authenticate with your primary 
    /// credentials and supply the secondary API keys as URL parameters to these API endpoints.
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<SecretsRequestResult> RetrieveApiSecretsAsync(string apiKey=null, Credentials creds = null);

    /// <summary>
    /// Createse an API Secret
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiKey">The Api Key to create a secret for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Secret> CreateApiSecretAsync(CreateSecretRequest request, string apiKey=null, Credentials creds = null);

    /// <summary>
    /// retrieves info about an api secret at the given id
    /// </summary>
    /// <param name="secretId">the id of the secret</param>
    /// <param name="apiKey">Api Key the secret is for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<Secret> RetrieveApiSecretAsync(string secretId, string apiKey=null, Credentials creds = null);

    /// <summary>
    /// Deletes an Api Secret
    /// </summary>
    /// <param name="secretId">the id of the secret to be deleted</param>
    /// <param name="apiKey">the api key the secret is for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<bool> RevokeApiSecretAsync(string secretId, string apiKey=null, Credentials creds = null);
        
    /// <summary>
    /// Create a new sub account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<SubAccount> CreateSubAccountAsync(CreateSubAccountRequest request, string apiKey = null, Credentials creds = null);

    /// <summary>
    /// Retrieve a sub account
    /// </summary>
    /// <param name="subAccountKey"></param>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<SubAccount> RetrieveSubAccountAsync(string subAccountKey, string apiKey = null, Credentials creds = null);

    /// <summary>
    /// Create a new sub account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    SubAccount CreateSubAccount(CreateSubAccountRequest request, string apiKey = null, Credentials creds = null);
        
    /// <summary>
    /// Retrieve a sub account
    /// </summary>
    /// <param name="subAccountKey"></param>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    SubAccount RetrieveSubAccount(string subAccountKey, string apiKey = null, Credentials creds = null);

    /// <summary>
    /// Retrieve the current balance of your Vonage API account
    /// </summary>
    /// <param name="creds"></param>
    /// <returns></returns>
    Balance GetAccountBalance(Credentials creds = null);

    /// <summary>
    /// You can top up your account using this API when you have enabled auto-reload in the dashboard. 
    /// The amount added by the top-up operation will be the same amount as was added in the payment 
    /// when auto-reload was enabled. Your account balance is checked every 5-10 minutes and if it falls 
    /// below the threshold and auto-reload is enabled, then it will be topped up automatically. Use this 
    /// endpoint if you need to top up at times when your credit may be exhausted more quickly than the auto-reload may occur.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null);

    /// <summary>
    /// Update the default callback URLs (where the webhooks are sent to) associated with your account: 
    /// * Callback URL for incoming SMS messages * Callback URL for delivery receipts        
    /// Note: that the URLs you provide must be valid and active. Vonage will check that they return a 200 OK response before the setting is saved
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request, Credentials creds = null);

    /// <summary>
    /// Many of Vonage's APIs are accessed using an API key and secret. It is recommended that you change or "rotate" 
    /// your secrets from time to time for security purposes. This section provides the API interface for achieving this. 
    /// Note: to work on secrets for your secondary accounts, you may authenticate with your primary 
    /// credentials and supply the secondary API keys as URL parameters to these API endpoints.
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    SecretsRequestResult RetrieveApiSecrets(string apiKey = null, Credentials creds = null);

    /// <summary>
    /// Createse an API Secret
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiKey">The Api Key to create a secret for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Secret CreateApiSecret(CreateSecretRequest request, string apiKey = null, Credentials creds = null);

    /// <summary>
    /// retrieves info about an api secret at the given id
    /// </summary>
    /// <param name="secretId">the id of the secret</param>
    /// <param name="apiKey">Api Key the secret is for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Secret RetrieveApiSecret(string secretId, string apiKey = null, Credentials creds = null);

    /// <summary>
    /// Deletes an Api Secret
    /// </summary>
    /// <param name="secretId">the id of the secret to be deleted</param>
    /// <param name="apiKey">the api key the secret is for</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    bool RevokeApiSecret(string secretId, string apiKey = null, Credentials creds = null);
}