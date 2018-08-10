using Newtonsoft.Json;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api
{
    public static class Redact
    {
        public class RedactCommand
        {
            /// <summary>
            /// The transaction ID to redact
            /// </summary>
            public string Id { get; set; }
            /// <summary>
            /// Product name that the ID provided relates to
            /// Must be one of: sms, voice, number-insight, verify, verify-sdk, message or workflow
            /// </summary>
            public string Product { get; set; }
            /// <summary>
            /// Required if redacting SMS data
            /// Must be one of: inbound or outbound
            /// </summary>
            public string Type { get; set; }
        }

        /// <summary>
        /// POST /v1/redact/transaction - redacts a specific transaction
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static void RedactTransaction(RedactCommand cmd, Credentials creds = null)
        {
            VersionedApiRequest.DoRequest("POST", ApiRequest.GetBaseUriFor(typeof(Redact), "/v1/redact/transaction"), cmd, creds);
        }
    }
}
