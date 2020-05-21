using Newtonsoft.Json;
using Nexmo.Api.Cryptography;
﻿using System.Web;
using System;
using System.Collections.Generic;
using System.Web;
using Xunit;
namespace Nexmo.Api.Test.Unit
{
    public class SMS_test : TestBase
    {

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestSmsRequest(bool passCreds)
        {

            // ARRANGE
            var restUrl = "https://rest.nexmo.com";
            var expectedUri = $"{restUrl}/sms/json";            
            var responseContent = "{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}";
            var from = "98975";
            var to = "17775551212";
            var text = "this is a test";
            var requestContent = $"from={from}&to={to}&text={HttpUtility.UrlEncode(text)}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: responseContent, requestContent: requestContent);

            // ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            SMS.SMSResponse response;
            if (passCreds)
            {
                response = client.SMS.Send(new SMS.SMSRequest() { to = to, from = from, text = text }, creds);
            }
            else
            {
                response = client.SMS.Send(new SMS.SMSRequest() { to = to, from = from, text = text });
            }

            //ASSERT
            Assert.True(response.messages.Count == 1);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestSmsWithoutFrom(bool passCreds)
        {
            // ARRANGE
            var restUrl = "https://rest.nexmo.com";
            var expectedUri = $"{restUrl}/sms/json";
            var responseContent = "{\"message-count\": \"1\",\"messages\": [{\"to\": \"17775551212\",\"message-id\": \"02000000A3AF32FA\",\"status\": \"0\",\"remaining-balance\": \"7.55560000\",\"message-price\": \"0.00480000\",\"network\": \"310004\"}]}";
          
            var to = "17775551212";
            var text = "this is a test";
            var requestContent = $"to={to}&text={HttpUtility.UrlEncode(text)}&api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(uri: expectedUri, responseContent: responseContent, requestContent: requestContent);

            // ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            SMS.SMSResponse response;
            if (passCreds)
            {
                response = client.SMS.Send(new SMS.SMSRequest() { to = to, text = text }, creds);
            }
            else
            {
                response = client.SMS.Send(new SMS.SMSRequest() { to = to, text = text });
            }

            //ASSERT
            Assert.True(response.messages.Count == 1);
        }

        [Fact]
        public void TestGenerateSignatureMD5()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "666c2c1a3fe7d621ad10456c4531e702";
            var request = @"{
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
              ""udh"": ""abc123"",
              ""sig"":""12345""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.md5hash;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);
            Assert.Equal(testSig, expectedSig);
        }

        [Fact]
        public void TestGenerateSignatureMD5HMAC()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "D96F3D3BC11D36392F95ABB03F382EB2";
            var request = @"{
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
              ""udh"": ""abc123"",
              ""sig"":""12345""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.md5;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);
            Assert.Equal(testSig, expectedSig);
        }

        [Fact]
        public void TestGenerateSignatureSHA1HMAC()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "8C5FF7362BA6F63FDF74C5C2B35ACBE5BF563477";
            var request = @"{
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
              ""udh"": ""abc123"",
              ""sig"":""12345""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.sha1;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);            
            Assert.Equal(testSig, expectedSig);
        }

        [Fact]
        public void TestGenerateSignatureSHA256HMAC()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "B5FE66C4FE808C191B27D0AFC56918B5CC1FDC4784B82528C1D0537BA8A57192";
            var request = @"{
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
              ""udh"": ""abc123"",
              ""sig"":""12345""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.sha256;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);
            
            Assert.Equal(testSig, expectedSig);
        }

        [Fact]
        public void TestGenerateSignatureSHA512HMAC()
        {
            var signingKey = "2zzXDyLUAEdT8rTcKqJuOwgPmRYBDAu4jXDi0GmoARevPdOZ1R";
            var expectedSig = "AB1630493820A5DE881333F3320E2755212D3CF96B5E20158229B19928B380205043230F00F2E5FAE8FD4CEE8F7FD2CEF364C03086A00FF2F3644B05561CC232";
            var request = @"{
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
              ""udh"": ""abc123"",
              ""sig"":""12345""
            }";
            var message = JsonConvert.DeserializeObject<SMS.SMSInbound>(request);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(request);
            var signatureString = SMS.SMSInbound.ConstructSignatureStringFromDictionary(dict);
            var method = SmsSignatureGenerator.Method.sha512;
            var testSig = SmsSignatureGenerator.GenerateSignature(signatureString, signingKey, method);            
            Assert.Equal(testSig, expectedSig);
        }

        [Fact]
        public void TestDLRStruct()
        {
            var dlrRct = @"{
              ""msisdn"": ""447700900000"",
              ""to"": ""AcmeInc"",
              ""network-code"": ""12345"",
              ""messageId"": ""0A0000001234567B"",
              ""price"": ""0.03330000"",
              ""status"": ""delivered"",
              ""scts"": ""2001011400"",
              ""err-code"": ""0"",
              ""message-timestamp"": ""2020-01-01T12:00:00.000+00:00""
            }";
            var castObject = JsonConvert.DeserializeObject<SMS.SMSDeliveryReceipt>(dlrRct);
            Assert.Equal("447700900000", castObject.msisdn);
            Assert.Equal("AcmeInc", castObject.to);
            Assert.Equal("12345", castObject.network_code);
            Assert.Equal("0A0000001234567B", castObject.messageId);
            Assert.Equal("0.03330000", castObject.price);
            Assert.Equal("delivered", castObject.status);
            Assert.Equal("2001011400", castObject.scts);
            Assert.Equal("0", castObject.err_code);
            var ci = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            Assert.Equal(castObject.message_timestamp, System.DateTime.Parse("2020-01-01T12:00:00.000+00:00",ci));

        }

        [Fact]
        public void TestSendSmsWithSig()
        {
        }
    }
}
