using Nexmo.Api.Request;

namespace Nexmo.Api.Accounts
{
    public interface IAccountClient
    {
        Balance GetAccountBalance(Credentials creds = null);
        
        TopUpResult TopUpAccountBalance(TopUpRequest request, Credentials creds = null);
        
        AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request, Credentials creds = null);

        SecretsRequestResult RetrieveApiSecrets(string apiKey=null, Credentials creds = null);

        Secret CreateApiSecret(CreateSecretRequest request, string apiKey=null, Credentials creds = null);

        Secret RetrieveApiSecret(string secretId, string apiKey=null, Credentials creds = null);

        bool RevokeApiSecret(string secretId, string apiKey=null, Credentials creds = null);
    }
}