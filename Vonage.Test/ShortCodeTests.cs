﻿#region
using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Request;
using Vonage.ShortCodes;
using Xunit;
#endregion

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class ShortCodeTests : TestBase
{
    [Theory]
    [InlineData(true, false)]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public async Task SendAlertAsync(bool passCredentials, bool useAllParameters)
    {
        //ARRANGE
        var request = new AlertRequest
        {
            To = "16365553226",
        };
        var expectedUri = $"{this.RestUrl}/sc/us/alert/json?to={request.To}";
        if (useAllParameters)
        {
            request.StatusReportReq = "1";
            request.ClientRef = Guid.NewGuid().ToString();
            request.Template = "Test Template";
            request.Type = "text";
            expectedUri +=
                $"&status-report-req={request.StatusReportReq}&client-ref={request.ClientRef}&template={WebUtility.UrlEncode(request.Template)}&type={request.Type}";
        }

        var expectedResponseContent = this.GetResponseJson();
        expectedUri += "&";
        this.Setup(expectedUri, expectedResponseContent);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        var response = await client.ShortCodesClient.SendAlertAsync(request, passCredentials ? creds : null);

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
    public async Task ManageOptInAsync(bool passCredentials)
    {
        //ARRANGE
        var request = new OptInManageRequest
        {
            Msisdn = "15559301529",
        };
        var expectedResponseContent = this.GetResponseJson();
        var expectedUri =
            $"{this.RestUrl}/sc/us/alert/opt-in/manage/json?msisdn={request.Msisdn}&";
        this.Setup(expectedUri, expectedResponseContent);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        var response = await client.ShortCodesClient.ManageOptInAsync(request, passCredentials ? creds : null);

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
    public async Task QueryOptInsAsync(bool passCredentials, bool allParameters)
    {
        //ARRANGE
        var request = new OptInQueryRequest();
        var expectedUri = $"{this.RestUrl}/sc/us/alert/opt-in/query/json";
        if (allParameters)
        {
            request.Page = "1";
            request.PageSize = "20";
            expectedUri += $"?page-size={request.PageSize}&page={request.Page}&";
        }

        var expectedResponseContent = this.GetResponseJson();
        this.Setup(expectedUri, expectedResponseContent);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        var response = await client.ShortCodesClient.QueryOptInsAsync(request, passCredentials ? creds : null);

        //ASSERT
        Assert.Equal("3", response.OptInCount);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task SendTwoFactorAuthAsync(bool passCredentials)
    {
        //ARRANGE
        var request = new TwoFactorAuthRequest();
        var expectedUri = $"{this.RestUrl}/sc/us/2fa/json";
        var expectedResponseContent = this.GetResponseJson();
        this.Setup(expectedUri, expectedResponseContent);

        //ACT
        var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
        var client = this.BuildVonageClient(creds);
        var response =
            await client.ShortCodesClient.SendTwoFactorAuthAsync(request, passCredentials ? creds : null);

        //ASSERT
        Assert.Equal("1", response.MessageCount);
    }
}