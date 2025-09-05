#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
using Vonage.Serialization;
#endregion

namespace Vonage.Test.Messaging;

internal static class InboundSmsTestData
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    private static InboundSms CreateBasic() =>
        new InboundSms
        {
            ApiKey = "abcd1234",
            Msisdn = "447700900001",
            To = "447700900000",
            MessageId = "0A0000000123ABCD1",
            Text = "Hello world",
            Keyword = "HELLO",
            MessageTimestamp = "2020-01-01T12:00:00.000+00:00",
            Timestamp = "1578787200",
            Nonce = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
            Concat = true,
            ConcatRef = "3",
        };

    internal static InboundSms CreateWithValidSignature(SmsSignatureGenerator.Method encryptionMethod)
    {
        var inboundSms = CreateBasic();
        var json = JsonConvert.SerializeObject(inboundSms, VonageSerialization.SerializerSettings);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        inboundSms.Sig = SmsSignatureGenerator.GenerateSignature(
            SignatureValidation.BuildQueryString(dict),
            SigningSecret,
            encryptionMethod);
        return inboundSms;
    }
}