using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nexmo.Api;

namespace Nexmo.Samples.Web.Controllers.Api
{
    // TODO: REFACTOR!

    [Route("/api/SmsDelivery")]
    public class SmsDeliveryController : Controller
    {
        private static readonly uint[] _lookup32 = CreateLookup32();

        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var s = i.ToString("X2");
                result[i] = s[0] + ((uint)s[1] << 16);
            }
            return result;
        }

        private static string ByteArrayToHexViaLookup32(byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        //////////

        static IMemoryCache _cache;

        public SmsDeliveryController(IMemoryCache cache) {
            _cache = cache;
        }

        readonly object _cacheLock = new object();

        [HttpGet]
        public ActionResult Get([FromQuery]SMS.SMSDeliveryReceipt response)
        {
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                }
            };

            // Upon initial setup with Nexmo, this action will be tested up to 5 times. No response data will be included. Just accept the empty request with a 200.
            if (null == response.to && null == response.msisdn)
                return new OkResult();

            // check for signed receipt
            if (Request.Query.ContainsKey("sig"))
            {
                var querystring = Request.Query;
                // Compare the local time with the timestamp
                var localTime =
                    (int) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
                var messageTime = int.Parse(querystring["timestamp"]);
                // Message cannot be more than 5 minutes old
                var maxDelta = 5*60;
                var difference = Math.Abs(localTime - messageTime);
                if (difference > maxDelta)
                {
                    Console.WriteLine("Timestamp difference greater than 5 minutes");
                }
                else
                {
                    var sorted = new SortedDictionary<string, string>();
                    // Sort the query parameters, removing sig as we go
                    foreach (var key in querystring.Keys.Where(k => k != "sig"))
                    {
                        sorted.Add(key, querystring[key]);
                    }
                    // Create the signing url using the sorted parameters and your SECURITY_SECRET
                    var sb = new StringBuilder();
                    buildStringFromParams(sorted, sb);
                    var queryToSign = "&" + sb;
                    queryToSign = queryToSign.Remove(queryToSign.Length - 1) + Configuration.Instance.Settings["appSettings:Nexmo.security_secret"].ToUpper();
                    // Generate MD5
                    var hashgen = MD5.Create();
                    var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(queryToSign));
                    var generatedSig = ByteArrayToHexViaLookup32(hash).ToLower();
                    // A timing attack safe string comparison to validate hash
                    Console.WriteLine(SlowEquals(Encoding.UTF8.GetBytes(generatedSig),
                        Encoding.UTF8.GetBytes(querystring["sig"]))
                        ? "Message was sent by Nexmo"
                        : "Alert: message not sent by Nexmo!");
                }
            }
            else
            {
                lock (_cacheLock)
                {
                    List<SMS.SMSDeliveryReceipt> receipts;
                    const string cachekey = "sms_receipts";
                    _cache.TryGetValue(cachekey, out receipts);
                    if (null == receipts)
                    {
                        receipts = new List<SMS.SMSDeliveryReceipt>();
                    }
                    receipts.Add(response);
                    _cache.Set(cachekey, receipts, DateTimeOffset.MaxValue);
                }
            }

            // always return 200 even if the message failed our validations so we don't get the same DLR again
            return new OkResult();
        }
    }
}