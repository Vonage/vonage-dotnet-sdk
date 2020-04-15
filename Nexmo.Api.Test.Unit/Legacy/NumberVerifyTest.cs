using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class NumberVerifyTest : TestBase
    {

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SendControl(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/verify/control/json?request_id=B41F2D19-913C-4BB3-B825-624E375D2C31&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = "{\"status\":\"0\",\"command\":\"cancel\"}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            NumberVerify.ControlResponse results;
            if (passCreds)
            {
                results = client.NumberVerify.Control(new NumberVerify.ControlRequest
                {
                    request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                    cmd = "cancel"
                }, creds);
            }
            else
            {
                results = client.NumberVerify.Control(new NumberVerify.ControlRequest
                {
                    request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                    cmd = "cancel"
                });
            }

            //ASSERT
            Assert.Equal("0", results.status);
            Assert.Equal("cancel", results.command);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SendVerifyRequest(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/verify/json?number=12018675309&country=US&brand=AcmeInc&sender_id=ACME&code_length=6&lg=en-US&pin_expiry=240&next_event_wait=120&workflow_id=4&api_key={ApiKey}&api_secret={ApiSecret}&";
            
            var expectedResult = @"{
              ""request_id"": ""abcdef012345..."",
              ""status"": ""0""
              }";

            Setup(uri: expectedUri, responseContent: expectedResult);

            //ACT
            var request = new NumberVerify.VerifyRequest()
            {
                number = "12018675309",
                brand = "AcmeInc",
                country = "US",
                sender_id = "ACME",
                code_length = "6",
                lg = "en-US",
                pin_expiry = "240",
                next_event_wait = "120",
                workflow_id = "4"
            };
            var creds = new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            NumberVerify.VerifyResponse response;
            if (passCreds)
            {
                response = client.NumberVerify.Verify(request);
            }
            else
            {
                response = client.NumberVerify.Verify(request);
            }
            Assert.Equal("0", response.status);
            Assert.Equal("abcdef012345...", response.request_id);
        }
    }
}
