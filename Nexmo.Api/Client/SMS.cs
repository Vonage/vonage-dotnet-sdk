using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class SMS
    {
        public Credentials Credentials { get; set; }
        public SMS(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Send a SMS message.
        /// </summary>
        /// <param name="request">The SMS message request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.SMS.SMSResponse Send(Api.SMS.SMSRequest request, Credentials creds = null)
        {
            if (string.IsNullOrEmpty(request.from))
            {
                request.from = Configuration.Instance.Settings["Nexmo.sender_id"];
            }

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.SMS.SMSResponse), "/sms/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.SMS.SMSResponse>(response.JsonResponse);
        }
    }
}
