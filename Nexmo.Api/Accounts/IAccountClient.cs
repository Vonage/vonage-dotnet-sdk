namespace Nexmo.Api.Accounts
{
    public interface IAccountClient
    {
        Balance GetAccountBalance();
        TopUpResult TopUpAccountBalance(TopUpRequest request);
        AccountSettingsResult ChangeAccountSettings(AccountSettingsRequest request);
    }
}