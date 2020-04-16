using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nexmo.Api.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nexmo.Api.Messaging
{
    public class InboundSms
    {
        [JsonProperty("api-key")]
        [FromQuery(Name = "api-key")]
        public string ApiKey { get; set; }

        [JsonProperty("msisdn")]
        [FromQuery(Name = "msisdn")]        
        public string Msisdn { get; set; }

        [JsonProperty("to")]
        [FromQuery(Name = "to")]        
        public string To { get; set; }

        [JsonProperty("messageId")]
        [FromQuery(Name = "messageId")]        
        public string MessageId { get; set; }

        [JsonProperty("text")]
        [FromQuery(Name = "text")]        
        public string Text { get; set; }

        [JsonProperty("type")]
        [FromQuery(Name = "type")]        
        public string Type { get; set; }

        [JsonProperty("keyword")]
        [FromQuery(Name = "keyword")]        
        public string Keyword { get; set; }

        [JsonProperty("message-timestamp")]
        [FromQuery(Name = "message-timestamp")]        
        public string MessageTimestamp { get; set; }

        [JsonProperty("timestamp")]
        [FromQuery(Name = "timestamp")]        
        public string Timestamp { get; set; }

        [JsonProperty("nonce")]
        [FromQuery(Name = "nonce")]        
        public string Nonce { get; set; }

        [JsonProperty("concat")]
        [FromQuery(Name = "concat")]        
        public string Concat { get; set; }

        [JsonProperty("concat-ref")]
        [FromQuery(Name = "concat-ref")]        
        public string ConcatRef { get; set; }

        [JsonProperty("concat-total")]
        [FromQuery(Name = "concat-total")]        
        public string ConcatTotal { get; set; }
        [JsonProperty("concat-part")]
        [FromQuery(Name = "concat-part")]
        public string ConcatPart { get; set; }

        [JsonProperty("data")]
        [FromQuery(Name = "data")]        
        public string Data { get; set; }

        [JsonProperty("udh")]
        [FromQuery(Name = "udh")]        
        public string Udh { get; set; }
        
        [JsonProperty("sig")]
        [FromQuery(Name ="sig")]
        public string Sig { get; set; }

        public bool ValidateSignature(string signatureSecret, SmsSignatureGenerator.Method method)
        {
            //use json representation to create a useable dictionary
            var json = JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var signatureString = ConstructSignatureStringFromDictionary(dict);
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signatureSecret, method);

            return testSig == Sig;
        }

        public static string ConstructSignatureStringFromDictionary(IDictionary<string, string> query)
        {
            try
            {
                var sig_sb = new StringBuilder();
                var sorted_dict = new SortedDictionary<string, string>(StringComparer.Ordinal);
                foreach (var key in query.Keys)
                {
                    sorted_dict.Add(key, query[key].ToString());
                }
                foreach (var key in sorted_dict.Keys)
                {
                    if (key == "sig")
                    {
                        continue;
                    }
                    sig_sb.AppendFormat("&{0}={1}", key.Replace('=', '_').Replace('&', '_'), sorted_dict[key].ToString().Replace('=', '_').Replace('&', '_'));
                }
                return sig_sb.ToString();
            }
            catch
            {
                return "";
            }
        }

    }
}