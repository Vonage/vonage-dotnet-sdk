using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class RedactTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TestRedact(bool passCreds)
        {
            var expectedUri = $"{ApiUrl}/v1/redact/transaction";
            var requestContent = "{\"id\":\"Abc1234567\",\"product\":\"sms\",\"type\":\"outbound\"}";
            var responseContet = "{}";
            Setup(uri: expectedUri, responseContet, requestContent: requestContent, System.Net.HttpStatusCode.NoContent);

            var request = new Redact.RedactRequest("Abc1234567", "sms", "outbound");
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            bool response;
            if (passCreds)
            {
                response = client.Redact.RedactTransaction(request,creds);
            }
            else
            {
                response = client.Redact.RedactTransaction(request);
            }
            Assert.True(response);

        }
    }
}
