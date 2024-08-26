#region
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Serialization;
#endregion

namespace Vonage.Messaging;

internal static class SignatureValidation
{
    private const string SignatureKey = "sig";

    internal static string BuildQueryString(IDictionary<string, string> query)
    {
        try
        {
            return query
                .OrderBy(pair => pair.Key)
                .Where(pair => pair.Key != SignatureKey)
                .Select(FormatProperty)
                .Aggregate(string.Empty, string.Concat);
        }
        catch
        {
            return string.Empty;
        }
    }

    private static string FormatProperty(KeyValuePair<string, string> pair) =>
        $"&{pair.Key.Replace('=', '_').Replace('&', '_')}={pair.Value.Replace('=', '_').Replace('&', '_')}";

    internal static bool ValidateSignature<T>(T webhook, string signatureSecret, SmsSignatureGenerator.Method method)
        where T : ISignable
    {
        var dict = ConvertToDictionary(webhook);
        var query = BuildQueryString(dict);
        var signature = SmsSignatureGenerator.GenerateSignature(query, signatureSecret, method);
        Debug.WriteLine(signature);
        return signature == webhook.Sig;
    }

    private static Dictionary<string, string> ConvertToDictionary<T>(T webhook) where T : ISignable
    {
        var serialized = JsonConvert.SerializeObject(webhook, VonageSerialization.SerializerSettings);
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
    }
}