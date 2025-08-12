#region
using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
using Vonage.Serialization;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endregion

namespace Vonage.Test.Messaging;

public class DeliveryReceiptTest
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeDeliveryReceipt(JsonSerializerType serializer)
    {
        var webhook = Deserialize<DeliveryReceipt>(ReadJson("Messaging/Data/DeliveryReceipt.json"), serializer);
        Assert.Equal("447700900000", webhook.Msisdn);
        Assert.Equal("AcmeInc", webhook.To);
        Assert.Equal("12345", webhook.NetworkCode);
        Assert.Equal("0A0000001234567B", webhook.MessageId);
        Assert.Equal("0.03330000", webhook.Price);
        Assert.Equal(DlrStatus.delivered, webhook.Status);
        Assert.Equal("2001011400", webhook.Scts);
        Assert.Equal("0", webhook.ErrorCode);
        Assert.Equal("abcd1234", webhook.ApiKey);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", webhook.MessageTimestamp);
        Assert.True(1582650446 == webhook.Timestamp);
        Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", webhook.Nonce);
        Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", webhook.Sig);
        Assert.Equal("steve", webhook.ClientRef);
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeInboundSms(JsonSerializerType serializer)
    {
        var webhook = Deserialize<InboundSms>(ReadJson("Messaging/Data/InboundSms.json"), serializer);
        Assert.Equal("abcd1234", webhook.ApiKey);
        Assert.Equal("447700900001", webhook.Msisdn);
        Assert.Equal("447700900000", webhook.To);
        Assert.Equal("0A0000000123ABCD1", webhook.MessageId);
        Assert.Equal("Hello world", webhook.Text);
        Assert.Equal("text", webhook.Type);
        Assert.Equal("HELLO", webhook.Keyword);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", webhook.MessageTimestamp);
        Assert.Equal("1578787200", webhook.Timestamp);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", webhook.Nonce);
        Assert.True(webhook.Concat);
        Assert.Equal("1", webhook.ConcatRef);
        Assert.Equal("3", webhook.ConcatTotal);
        Assert.Equal("2", webhook.ConcatPart);
        Assert.Equal("abc123", webhook.Data);
        Assert.Equal("abc123", webhook.Udh);
    }

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

    private static T Deserialize<T>(string json, JsonSerializerType serializerType) => serializerType switch
    {
        JsonSerializerType.Newtonsoft => JsonConvert.DeserializeObject<T>(json),
        JsonSerializerType.SystemTextJson => JsonSerializer.Deserialize<T>(json),
        _ => throw new ArgumentOutOfRangeException(nameof(serializerType)),
    };

    private static string ReadJson(string path) => File.ReadAllText(path);
}