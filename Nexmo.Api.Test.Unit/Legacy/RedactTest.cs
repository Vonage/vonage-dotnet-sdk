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
        [Fact]
        public void TestRedact()
        {
            var expectedUri = $"{ApiUrl}/v1/redact/transaction";
            var requestContent = "{\"id\":\"Abc1234567\",\"product\":\"sms\",\"type\":\"outbound\"}";
            var responseContet = "{}";
            Setup(uri: expectedUri, responseContet, requestContent: requestContent, System.Net.HttpStatusCode.NoContent);

            var request = new Redact.RedactRequest("Abc1234567", "sms", "outbound");
            var client = new Client(new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var response = client.Redact.RedactTransaction(request);
            Assert.True(response);

        }
    }
}
