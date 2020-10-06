using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.ShortCodes
{
    public interface IShortCodesClient
    {
        Task<OptInSearchResponse> QueryOptInsAsync(OptInQueryRequest request, Credentials creds = null);

        Task<OptInRecord> ManageOptInAsync(OptInManageRequest request, Credentials creds = null);

        Task<AlertResponse> SendAlertAsync(AlertRequest request, Credentials creds = null);

        Task<TwoFactorAuthResponse> SendTwoFactorAuthAsync(TwoFactorAuthRequest request, Credentials creds = null);
    }
}