using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using Microsoft.Extensions.Primitives;

namespace Nexmo.Api.Request
{
    public static class SignatureHelper
    {
        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        public static bool IsSignatureValid(IEnumerable<KeyValuePair<string, StringValues>> querystring)
        {
            Action<IDictionary<string, string>, StringBuilder> buildStringFromParams = (param, strings) =>
            {
                foreach (var kvp in param)
                {
                    strings.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
                }
            };

            // Compare the local time with the timestamp
            var querystringList = querystring as IList<KeyValuePair<string, StringValues>> ?? querystring.ToList();
            var localTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var messageTime = int.Parse(querystringList.Single(kvp => kvp.Key == "timestamp").Value);
            // Message cannot be more than 5 minutes old
            const int maxDelta = 5 * 60;
            var difference = Math.Abs(localTime - messageTime);
            if (difference > maxDelta)
            {
                return false;
            }

            var sorted = new SortedDictionary<string, string>();
            // Sort the query parameters, removing sig as we go
            foreach (var kvp in querystringList.Where(kv => kv.Key != "sig"))
            {
                sorted.Add(kvp.Key, kvp.Value);
            }
            // Create the signing url using the sorted parameters and your SECURITY_SECRET
            var sb = new StringBuilder();
            buildStringFromParams(sorted, sb);
            var queryToSign = "&" + sb;
            queryToSign = queryToSign.Remove(queryToSign.Length - 1) + Configuration.Instance.Settings["appSettings:Nexmo.security_secret"].ToUpper();
            // Generate MD5
            var hashgen = MD5.Create();
            var hash = hashgen.ComputeHash(Encoding.UTF8.GetBytes(queryToSign));
            var generatedSig = ByteArrayToHexHelper.ByteArrayToHex(hash).ToLower();
            // A timing attack safe string comparison to validate hash
            return SlowEquals(Encoding.UTF8.GetBytes(generatedSig),
                Encoding.UTF8.GetBytes(querystringList.Single(kvp => kvp.Key == "sig").Value));
        }
    }
}