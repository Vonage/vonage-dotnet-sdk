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
        public class RedactRequest
        {
            /// <summary>
            /// The transaction ID to redact
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; set; }
            /// <summary>
            /// Product name that the ID provided relates to
            /// Must be one of: sms, voice, number-insight, verify, verify-sdk, message or workflow
            /// </summary>
            [JsonProperty("product")]
            public string Product { get; set; }
            /// <summary>
            /// Required if redacting SMS data
            /// Must be one of: inbound or outbound
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            public RedactRequest (string id, string product)
            {
                Id = id;
                Product = product;
            }
            public RedactRequest(string id, string product, string type)
            {
                Id = id;
                Product = product;
                Type = type;
            }

        }

        /// <summary>
        /// POST /v1/redact/transaction - redacts a specific transaction
        /// </summary>
        /// <param name="redactRequest"></param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NexmoResponse RedactTransaction(RedactRequest redactRequest, Credentials creds = null)
        {
            return ApiRequest.DoRequest("POST",ApiRequest.GetBaseUriFor(typeof(Redact), "/v1/redact/transaction"), redactRequest, ApiRequest.AuthType.Basic, creds);
        }
    }
}
