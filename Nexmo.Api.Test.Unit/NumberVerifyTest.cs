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
    }
}
