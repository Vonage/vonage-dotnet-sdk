using System;
using System.Threading.Tasks;
using System.Web;
using Vonage.ShortCodes;
using Xunit;

namespace Vonage.Test.Unit
{
    public class ShortCodeTests : TestBase
    {
        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void SendAlert(bool passCredentials, bool useAllParameters)
        {
            //ARRANGE
            AlertRequest request = new AlertRequest
            {
                To = "16365553226"
            };

            string expectedUri = $"{RestUrl}/sc/us/alert/json?to={request.To}";

            if (useAllParameters)
            {
                request.StatusReportReq = "1";
                request.ClientRef = Guid.NewGuid().ToString();
                request.Template = "Test Template";
                request.Type = "text";

                expectedUri += $"&status-report-req={request.StatusReportReq}&client-ref={request.ClientRef}&template={HttpUtility.UrlEncode(request.Template)}&type={request.Type}";
            }

            var expectedResponseContent = GetExpectedJson();
            expectedUri += $"&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            AlertResponse response = client.ShortCodesClient.SendAlert(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("1", response.MessageCount);
            Assert.NotEmpty(response.Messages);

            var message = response.Messages[0];

            Assert.Equal("delivered", message.Status);
            Assert.Equal("abcdefg", message.MessageId);
            Assert.Equal("16365553226", message.To);
            Assert.Equal("0", message.ErrorCode);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(true, true)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public async Task SendAlertAsync(bool passCredentials, bool useAllParameters)
        {
            //ARRANGE
            AlertRequest request = new AlertRequest
            {
                To = "16365553226"
            };

            string expectedUri = $"{RestUrl}/sc/us/alert/json?to={request.To}";

            if (useAllParameters)
            {
                request.StatusReportReq = "1";
                request.ClientRef = Guid.NewGuid().ToString();
                request.Template = "Test Template";
                request.Type = "text";

                expectedUri += $"&status-report-req={request.StatusReportReq}&client-ref={request.ClientRef}&template={HttpUtility.UrlEncode(request.Template)}&type={request.Type}";
            }

            var expectedResponseContent = GetExpectedJson();
            expectedUri += $"&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            AlertResponse response = await client.ShortCodesClient.SendAlertAsync(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("1", response.MessageCount);
            Assert.NotEmpty(response.Messages);

            var message = response.Messages[0];

            Assert.Equal("delivered", message.Status);
            Assert.Equal("abcdefg", message.MessageId);
            Assert.Equal("16365553226", message.To);
            Assert.Equal("0", message.ErrorCode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ManageOptIn(bool passCredentials)
        {
            //ARRANGE
            OptInManageRequest request = new OptInManageRequest
            {
                Msisdn = "15559301529"
            };

            var expectedResponseContent = GetExpectedJson();
            string expectedUri = $"{RestUrl}/sc/us/alert/opt-in/manage/json?msisdn={request.Msisdn}&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            OptInRecord response = client.ShortCodesClient.ManageOptIn(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("15559301529", response.Msisdn);
            Assert.True(response.OptIn);
            Assert.Equal("2014-08-21 17:34:47", response.OptInDate);
            Assert.False(response.OptOut);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ManageOptInAsync(bool passCredentials)
        {
            //ARRANGE
            OptInManageRequest request = new OptInManageRequest
            {
                Msisdn = "15559301529"
            };

            var expectedResponseContent = GetExpectedJson();
            string expectedUri = $"{RestUrl}/sc/us/alert/opt-in/manage/json?msisdn={request.Msisdn}&api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            OptInRecord response = await client.ShortCodesClient.ManageOptInAsync(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("15559301529", response.Msisdn);
            Assert.True(response.OptIn);
            Assert.Equal("2014-08-21 17:34:47", response.OptInDate);
            Assert.False(response.OptOut);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void QueryOptIns(bool passCredentials, bool allParameters)
        {
            //ARRANGE
            OptInQueryRequest request = new OptInQueryRequest();

            string expectedUri = $"{RestUrl}/sc/us/alert/opt-in/query/json?";

            if (allParameters)
            {
                request.Page = "1";
                request.PageSize = "20";

                expectedUri += $"page-size={request.PageSize}&page={request.Page}&";
            }

            var expectedResponseContent = GetExpectedJson();
            expectedUri += $"api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            OptInSearchResponse response = client.ShortCodesClient.QueryOptIns(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("3", response.OptInCount);
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task QueryOptInsAsync(bool passCredentials, bool allParameters)
        {
            //ARRANGE
            OptInQueryRequest request = new OptInQueryRequest();

            string expectedUri = $"{RestUrl}/sc/us/alert/opt-in/query/json?";

            if (allParameters)
            {
                request.Page = "1";
                request.PageSize = "20";

                expectedUri += $"page-size={request.PageSize}&page={request.Page}&";
            }

            var expectedResponseContent = GetExpectedJson();
            expectedUri += $"api_key={ApiKey}&api_secret={ApiSecret}&";

            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            OptInSearchResponse response = await client.ShortCodesClient.QueryOptInsAsync(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("3", response.OptInCount);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SendTwoFactorAuth(bool passCredentials)
        {
            //ARRANGE
            TwoFactorAuthRequest request = new TwoFactorAuthRequest();

            string expectedUri = $"{RestUrl}/sc/us/2fa/json?api_key={ApiKey}&api_secret={ApiSecret}&";
            string expectedResponseContent = GetExpectedJson();
            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            TwoFactorAuthResponse response = client.ShortCodesClient.SendTwoFactorAuth(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("1", response.MessageCount);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task SendTwoFactorAuthAsync(bool passCredentials)
        {
            //ARRANGE
            TwoFactorAuthRequest request = new TwoFactorAuthRequest();

            string expectedUri = $"{RestUrl}/sc/us/2fa/json?api_key={ApiKey}&api_secret={ApiSecret}&";
            string expectedResponseContent = GetExpectedJson();
            Setup(expectedUri, expectedResponseContent);

            //ACT
            var creds = Request.Credentials.FromApiKeyAndSecret(ApiKey, ApiSecret);
            var client = new VonageClient(creds);

            TwoFactorAuthResponse response = await client.ShortCodesClient.SendTwoFactorAuthAsync(request, passCredentials ? creds : null);

            //ASSERT
            Assert.Equal("1", response.MessageCount);
        }
    }
}
