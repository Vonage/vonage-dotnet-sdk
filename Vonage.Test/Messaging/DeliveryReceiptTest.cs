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

public class DeliveryReceiptTest
{
    private const string SigningSecret = "Y6dI3wtDP8myVH5tnDoIaTxEvAJhgDVCczBa1mHniEqsdlnnebg";

    [Fact]
    public void TestDlrStruct()
    {
        var jsonFromNDP = @"{
                  ""msisdn"": ""447700900000"",
                  ""to"": ""AcmeInc"",
                  ""network-code"": ""12345"",
                  ""messageId"": ""0A0000001234567B"",
                  ""price"": ""0.03330000"",
                  ""status"": ""delivered"",
                  ""scts"": ""2001011400"",
                  ""err-code"": ""0"",
                  ""api-key"": ""abcd1234"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": 1582650446,
                  ""nonce"": ""ec11dd3e-1e7f-4db5-9467-82b02cd223b9"",
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
        var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP);
        Assert.Equal("447700900000", dlr.Msisdn);
        Assert.Equal("AcmeInc", dlr.To);
        Assert.Equal("12345", dlr.NetworkCode);
        Assert.Equal("0A0000001234567B", dlr.MessageId);
        Assert.Equal("0.03330000", dlr.Price);
        Assert.Equal(DlrStatus.delivered, dlr.Status);
        Assert.Equal("2001011400", dlr.Scts);
        Assert.Equal("0", dlr.ErrorCode);
        Assert.Equal("abcd1234", dlr.ApiKey);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
        Assert.True(1582650446 == dlr.Timestamp);
        Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
        Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
        Assert.Equal("steve", dlr.ClientRef);
    }

    [Fact]
    public void TestDlrStructCamelCaseIgnore()
    {
        var jsonFromNDP = @"{
                  ""msisdn"": ""447700900000"",
                  ""to"": ""AcmeInc"",
                  ""network-code"": ""12345"",
                  ""messageId"": ""0A0000001234567B"",
                  ""price"": ""0.03330000"",
                  ""status"": ""delivered"",
                  ""scts"": ""2001011400"",
                  ""err-code"": ""0"",
                  ""api-key"": ""abcd1234"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": 1582650446,
                  ""nonce"": ""ec11dd3e-1e7f-4db5-9467-82b02cd223b9"",
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
        var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP,
            VonageSerialization.SerializerSettings);
        Assert.Equal("447700900000", dlr.Msisdn);
        Assert.Equal("AcmeInc", dlr.To);
        Assert.Equal("12345", dlr.NetworkCode);
        Assert.Equal("0A0000001234567B", dlr.MessageId);
        Assert.Equal("0.03330000", dlr.Price);
        Assert.Equal(DlrStatus.delivered, dlr.Status);
        Assert.Equal("2001011400", dlr.Scts);
        Assert.Equal("0", dlr.ErrorCode);
        Assert.Equal("abcd1234", dlr.ApiKey);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
        Assert.True(1582650446 == dlr.Timestamp);
        Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
        Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
        Assert.Equal("steve", dlr.ClientRef);
    }

    [Fact]
    public void TestDlrStructNoStatus()
    {
        var jsonFromNDP = @"{
                  ""msisdn"": ""447700900000"",
                  ""to"": ""AcmeInc"",
                  ""network-code"": ""12345"",
                  ""messageId"": ""0A0000001234567B"",
                  ""price"": ""0.03330000"",                  
                  ""scts"": ""2001011400"",
                  ""err-code"": ""0"",
                  ""api-key"": ""abcd1234"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": 1582650446,
                  ""nonce"": ""ec11dd3e-1e7f-4db5-9467-82b02cd223b9"",
                  ""sig"": ""1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C"",
                  ""client-ref"": ""steve""
                }";
        var dlr = JsonConvert.DeserializeObject<DeliveryReceipt>(jsonFromNDP);
        Assert.Equal("447700900000", dlr.Msisdn);
        Assert.Equal("AcmeInc", dlr.To);
        Assert.Equal("12345", dlr.NetworkCode);
        Assert.Equal("0A0000001234567B", dlr.MessageId);
        Assert.Equal("0.03330000", dlr.Price);
        Assert.Equal(DlrStatus.unknown, dlr.Status);
        Assert.Equal("2001011400", dlr.Scts);
        Assert.Equal("0", dlr.ErrorCode);
        Assert.Equal("abcd1234", dlr.ApiKey);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", dlr.MessageTimestamp);
        Assert.True(1582650446 == dlr.Timestamp);
        Assert.Equal("ec11dd3e-1e7f-4db5-9467-82b02cd223b9", dlr.Nonce);
        Assert.Equal("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C", dlr.Sig);
        Assert.Equal("steve", dlr.ClientRef);
    }

    [Fact]
    public void TestInboundSmsStruct()
    {
        var jsonFromNdp = @"{
                  ""api-key"": ""abcd1234"",
                  ""msisdn"": ""447700900001"",
                  ""to"": ""447700900000"",
                  ""messageId"": ""0A0000000123ABCD1"",
                  ""text"": ""Hello world"",
                  ""type"": ""text"",
                  ""keyword"": ""HELLO"",
                  ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00"",
                  ""timestamp"": ""1578787200"",
                  ""nonce"": ""aaaaaaaa-bbbb-cccc-dddd-0123456789ab"",
                  ""concat"": ""true"",
                  ""concat-ref"": ""1"",
                  ""concat-total"": ""3"",
                  ""concat-part"": ""2"",
                  ""data"": ""abc123"",
                  ""udh"": ""abc123""
                }";
        var inboundSms = JsonConvert.DeserializeObject<InboundSms>(jsonFromNdp);
        Assert.Equal("abcd1234", inboundSms.ApiKey);
        Assert.Equal("447700900001", inboundSms.Msisdn);
        Assert.Equal("447700900000", inboundSms.To);
        Assert.Equal("0A0000000123ABCD1", inboundSms.MessageId);
        Assert.Equal("Hello world", inboundSms.Text);
        Assert.Equal("text", inboundSms.Type);
        Assert.Equal("HELLO", inboundSms.Keyword);
        Assert.Equal("2020-01-01T12:00:00.000+00:00", inboundSms.MessageTimestamp);
        Assert.Equal("1578787200", inboundSms.Timestamp);
        Assert.Equal("aaaaaaaa-bbbb-cccc-dddd-0123456789ab", inboundSms.Nonce);
        Assert.True(inboundSms.Concat);
        Assert.Equal("1", inboundSms.ConcatRef);
        Assert.Equal("3", inboundSms.ConcatTotal);
        Assert.Equal("2", inboundSms.ConcatPart);
        Assert.Equal("abc123", inboundSms.Data);
        Assert.Equal("abc123", inboundSms.Udh);
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
}