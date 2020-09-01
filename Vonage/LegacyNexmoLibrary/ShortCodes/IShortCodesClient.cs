using Nexmo.Api.Request;

namespace Nexmo.Api.ShortCodes
{
    [System.Obsolete("The Nexmo.Api.ShortCodes.IShortCodesClient class is obsolete. " +
        "References to it should be switched to the new Vonage.ShortCodes.IShortCodesClient class.")]
    public interface IShortCodesClient
    {
        OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null);

        OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null);

        AlertResponse SendAlert(AlertRequest request, Credentials creds = null);

        TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null);
    }
}