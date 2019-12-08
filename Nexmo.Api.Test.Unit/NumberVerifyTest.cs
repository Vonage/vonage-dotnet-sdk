using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexmo.Api.UnitTest
{
    public class NumberVerifyTest : TestBase
    {

        [Fact]
        public void SendControl()
        {
            //ARRANGE
            var expectedUri = $"{ApiUrl}/verify/control/json?request_id=B41F2D19-913C-4BB3-B825-624E375D2C31&cmd=cancel&api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponseContent = "{\"status\":\"0\",\"command\":\"cancel\"}";
            Setup(uri: expectedUri, responseContent: expectedResponseContent);

            //ACT
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var results = client.NumberVerify.Control(new NumberVerify.ControlRequest
            {
                request_id = "B41F2D19-913C-4BB3-B825-624E375D2C31",
                cmd = "cancel"
            });

            //ASSERT
            Assert.Equal("0", results.status);
            Assert.Equal("cancel", results.command);
        }

        [Fact]
        public void SendVerifyRequest()
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
            var client = new Client(new Request.Credentials() { ApiKey = ApiKey, ApiSecret = ApiSecret });
            client.NumberVerify.Verify(request);


        }
    }
}
