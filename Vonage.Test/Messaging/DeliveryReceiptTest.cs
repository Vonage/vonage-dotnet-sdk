﻿#region
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
using Vonage.Serialization;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

public class DeliveryReceiptTest
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    [Theory]
    [InlineData(SmsSignatureGenerator.Method.md5)]
    [InlineData(SmsSignatureGenerator.Method.md5hash)]
    [InlineData(SmsSignatureGenerator.Method.sha1)]
    [InlineData(SmsSignatureGenerator.Method.sha256)]
    [InlineData(SmsSignatureGenerator.Method.sha512)]
    public void TestValidateSignatureMd5(SmsSignatureGenerator.Method encryptionMethod) =>
        BuildDeliveryReceipt(encryptionMethod).ValidateSignature(SigningSecret, encryptionMethod).Should().BeTrue();

    private static DeliveryReceipt BuildDeliveryReceipt(SmsSignatureGenerator.Method encryptionMethod)
    {
        var receipt = new DeliveryReceipt
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
        var json = JsonConvert.SerializeObject(receipt, VonageSerialization.SerializerSettings);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        receipt.Sig = SmsSignatureGenerator.GenerateSignature(
            SignatureValidation.BuildQueryString(dict),
            SigningSecret,
            encryptionMethod);
        return receipt;
    }
}