#region
using System;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
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
    public void DeserializeDeliveryReceipt(JsonSerializerType serializer) =>
        Deserialize<DeliveryReceipt>(ReadJson("Messaging/Data/DeliveryReceipt.json"), serializer)
            .ShouldMatchExpectedDeliveryReceipt();

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void DeserializeInboundSms(JsonSerializerType serializer) =>
        Deserialize<InboundSms>(ReadJson("Messaging/Data/InboundSms.json"), serializer).ShouldMatchExpectedInboundSms();

    [Theory]
    [InlineData(SmsSignatureGenerator.Method.md5)]
    [InlineData(SmsSignatureGenerator.Method.md5hash)]
    [InlineData(SmsSignatureGenerator.Method.sha1)]
    [InlineData(SmsSignatureGenerator.Method.sha256)]
    [InlineData(SmsSignatureGenerator.Method.sha512)]
    public void TestValidateSignatureMd5(SmsSignatureGenerator.Method encryptionMethod) =>
        DeliveryReceiptTestData.CreateWithValidSignature(encryptionMethod)
            .ValidateSignature(SigningSecret, encryptionMethod).Should().BeTrue();

    private static T Deserialize<T>(string json, JsonSerializerType serializerType) => serializerType switch
    {
        JsonSerializerType.Newtonsoft => JsonConvert.DeserializeObject<T>(json),
        JsonSerializerType.SystemTextJson => JsonSerializer.Deserialize<T>(json),
        _ => throw new ArgumentOutOfRangeException(nameof(serializerType)),
    };

    private static string ReadJson(string path) => File.ReadAllText(path);
}