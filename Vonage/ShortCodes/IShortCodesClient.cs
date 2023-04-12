using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes;

public interface IShortCodesClient
{
    Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null);

    Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null);

    Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null);

    Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null);

    OptInSearchResponse QueryOptIns(OptInQueryRequest request, Credentials creds = null);

    OptInRecord ManageOptIn(OptInManageRequest request, Credentials creds = null);

    AlertResponse SendAlert(AlertRequest request, Credentials creds = null);

    TwoFactorAuthResponse SendTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null);
}