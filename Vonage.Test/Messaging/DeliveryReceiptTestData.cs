#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
using Vonage.Serialization;
#endregion

namespace Vonage.Test.Messaging;

internal static class DeliveryReceiptTestData
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    private static DeliveryReceipt CreateBasic() =>
        new DeliveryReceipt
        {
            ApiKey = "abcd1234",
            Msisdn = "447700900001",
            To = "447700900000",
            MessageId = "0A0000000123ABCD1",
            MessageTimestamp = "2020-01-01T12:00:00.000+00:00",
            Timestamp = 1578787200,
            Nonce = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
            ClientRef = "Test",
            Price = "50",
            Scts = "20200101100000",
            NetworkCode = "Test",
            StringStatus = "Test",
            ErrorCode = "Test",
        };

    internal static DeliveryReceipt CreateWithValidSignature(SmsSignatureGenerator.Method encryptionMethod)
    {
        var receipt = CreateBasic();
        var json = JsonConvert.SerializeObject(receipt, VonageSerialization.SerializerSettings);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        receipt.Sig = SmsSignatureGenerator.GenerateSignature(
            SignatureValidation.BuildQueryString(dict),
            SigningSecret,
            encryptionMethod);
        return receipt;
    }
}