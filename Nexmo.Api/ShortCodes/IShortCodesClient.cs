using Nexmo.Api.Request;

namespace Nexmo.Api.ShortCodes
{
    public interface IShortCodesClient
    {
        OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null);

        OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null);

        AlertResponse SendAlert(AlertRequest request, Credentials creds = null);

        TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null);
    }
}