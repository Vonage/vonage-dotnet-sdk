#region
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Cryptography;
using Vonage.Messaging;
using Vonage.Serialization;
using Xunit;
#endregion

namespace Vonage.Test.Messaging;

public class InboundSmsTest
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    [Theory]
    [InlineData(SmsSignatureGenerator.Method.md5)]
    [InlineData(SmsSignatureGenerator.Method.md5hash)]
    [InlineData(SmsSignatureGenerator.Method.sha1)]
    [InlineData(SmsSignatureGenerator.Method.sha256)]
    [InlineData(SmsSignatureGenerator.Method.sha512)]
    public void TestValidateSignatureMd5(SmsSignatureGenerator.Method encryptionMethod) =>
        BuildInboundSms(encryptionMethod).ValidateSignature(SigningSecret, encryptionMethod).Should().BeTrue();

    private static InboundSms BuildInboundSms(SmsSignatureGenerator.Method encryptionMethod)
    {
        var inboundSmsShell = new InboundSms
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
        var json = JsonConvert.SerializeObject(inboundSmsShell, VonageSerialization.SerializerSettings);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        inboundSmsShell.Sig = SmsSignatureGenerator.GenerateSignature(
            InboundSms.ConstructSignatureStringFromDictionary(dict), SigningSecret,
            encryptionMethod);
        return inboundSmsShell;
    }
}