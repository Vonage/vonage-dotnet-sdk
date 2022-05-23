using Newtonsoft.Json;
using Vonage.Cryptography;
using System;
using System.Collections.Generic;
using System.Text;
using Vonage.Serialization;

namespace Vonage.Messaging
{
    public class InboundSms
    {
        [JsonProperty("api-key")]
        public string ApiKey { get; set; }

        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("messageId")]
        public string MessageId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        [JsonProperty("message-timestamp")]
        public string MessageTimestamp { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("concat")]
        [JsonConverter(typeof(StringBoolConverter))]
        public bool Concat { get; set; }

        [JsonProperty("concat-ref")]        
        public string ConcatRef { get; set; }

        [JsonProperty("concat-total")]        
        public string ConcatTotal { get; set; }
        [JsonProperty("concat-part")]        
        public string ConcatPart { get; set; }

        [JsonProperty("data")]        
        public string Data { get; set; }

        [JsonProperty("udh")]        
        public string Udh { get; set; }
        
        [JsonProperty("sig")]        
        public string Sig { get; set; }

        public bool ValidateSignature(string signatureSecret, SmsSignatureGenerator.Method method)
        {
            //use json representation to create a useable dictionary
            var json = JsonConvert.SerializeObject(this, VonageSerialization.SerializerSettings);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            var signatureString = ConstructSignatureStringFromDictionary(dict);
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signatureSecret, method).ToString();
            System.Diagnostics.Debug.WriteLine(testSig);
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