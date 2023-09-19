using System.Linq;
using System.Web;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class AccountTest : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CreateApiSecret(bool passCreds)
        {
            //ARRANGE            
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Secret secret;
            if (passCreds)
            {
                secret = client.AccountClient.CreateApiSecret(new Accounts.CreateSecretRequest {Secret = "password"},
                    this.ApiKey, creds);
            }
            else
            {
                secret = client.AccountClient.CreateApiSecret(new Accounts.CreateSecretRequest {Secret = "password"},
                    this.ApiKey);
            }

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secret.Id);
            Assert.Equal("2017-03-02T16:34:49Z", secret.CreatedAt);
            Assert.Equal("abc123", secret.Links.Self.Href);
            Assert.Null(secret.Links.Next);
            Assert.Null(secret.Links.Prev);
            Assert.Null(secret.Links.First);
            Assert.Null(secret.Links.Last);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void CreateApiSecretAsync(bool passCreds)
        {
            //ARRANGE            
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Secret secret;
            if (passCreds)
            {
                secret = await client.AccountClient.CreateApiSecretAsync(
                    new Accounts.CreateSecretRequest {Secret = "password"}, this.ApiKey, creds);
            }
            else
            {
                secret = await client.AccountClient.CreateApiSecretAsync(
                    new Accounts.CreateSecretRequest {Secret = "password"}, this.ApiKey);
            }

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secret.Id);
            Assert.Equal("2017-03-02T16:34:49Z", secret.CreatedAt);
            Assert.Equal("abc123", secret.Links.Self.Href);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetAccountBalance(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{this.RestUrl}/account/get-balance?api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent = @"{""value"": 3.14159, ""autoReload"": false }";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Balance balance;
            if (passCreds)
            {
                balance = client.AccountClient.GetAccountBalance(creds);
            }
            else
            {
                balance = client.AccountClient.GetAccountBalance();
            }

            //ASSERT
            Assert.Equal(3.14159m, balance.Value);
            Assert.False(balance.AutoReload);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void GetAccountBalanceAsync(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{this.RestUrl}/account/get-balance?api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent = @"{""value"": 3.14159, ""autoReload"": false }";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Balance balance;
            if (passCreds)
            {
                balance = await client.AccountClient.GetAccountBalanceAsync(creds);
            }
            else
            {
                balance = await client.AccountClient.GetAccountBalanceAsync();
            }

            //ASSERT
            Assert.Equal(3.14159m, balance.Value);
            Assert.False(balance.AutoReload);
        }

     

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RetrieveApiSecrets(bool passCreds)
        {
            //ARRANGE            
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                        ""href"": ""abc123""
                      }
                  },
                  ""_embedded"": {
                    ""secrets"": [
                      {
                        ""_links"": {
                          ""self"": {
                            ""href"": ""abc123""
                          }
                        },
                        ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                        ""created_at"": ""2017-03-02T16:34:49Z""
                      }
                    ]
                  }
                }";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.SecretsRequestResult secrets;
            if (passCreds)
            {
                secrets = client.AccountClient.RetrieveApiSecrets(this.ApiKey, creds);
            }
            else
            {
                secrets = client.AccountClient.RetrieveApiSecrets(this.ApiKey);
            }

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secrets.Embedded.Secrets[0].Id);
            Assert.Equal("2017-03-02T16:34:49Z", secrets.Embedded.Secrets[0].CreatedAt);
            Assert.Equal("abc123", secrets.Embedded.Secrets[0].Links.Self.Href);
            Assert.Equal("abc123", secrets.Links.Self.Href);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void RetrieveApiSecretsAsync(bool passCreds)
        {
            //ARRANGE            
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                        ""href"": ""abc123""
                      }
                  },
                  ""_embedded"": {
                    ""secrets"": [
                      {
                        ""_links"": {
                          ""self"": {
                            ""href"": ""abc123""
                          }
                        },
                        ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                        ""created_at"": ""2017-03-02T16:34:49Z""
                      }
                    ]
                  }
                }";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.SecretsRequestResult secrets;
            if (passCreds)
            {
                secrets = await client.AccountClient.RetrieveApiSecretsAsync(this.ApiKey, creds);
            }
            else
            {
                secrets = await client.AccountClient.RetrieveApiSecretsAsync(this.ApiKey);
            }

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secrets.Embedded.Secrets[0].Id);
            Assert.Equal("2017-03-02T16:34:49Z", secrets.Embedded.Secrets[0].CreatedAt);
            Assert.Equal("abc123", secrets.Embedded.Secrets[0].Links.Self.Href);
            Assert.Equal("abc123", secrets.Links.Self.Href);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RetrieveSecret(bool passCreds)
        {
            //ARRANGE            
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/{secretId}";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Secret secret;
            if (passCreds)
            {
                secret = client.AccountClient.RetrieveApiSecret(secretId, this.ApiKey, creds);
            }
            else
            {
                secret = client.AccountClient.RetrieveApiSecret(secretId, this.ApiKey);
            }

            //ASSERT
            Assert.Equal(secretId, secret.Id);
            Assert.Equal("2017-03-02T16:34:49Z", secret.CreatedAt);
            Assert.Equal("abc123", secret.Links.Self.Href);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void RetrieveSecretAsync(bool passCreds)
        {
            //ARRANGE            
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/{secretId}";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.Secret secret;
            if (passCreds)
            {
                secret = await client.AccountClient.RetrieveApiSecretAsync(secretId, this.ApiKey, creds);
            }
            else
            {
                secret = await client.AccountClient.RetrieveApiSecretAsync(secretId, this.ApiKey);
            }

            //ASSERT
            Assert.Equal(secretId, secret.Id);
            Assert.Equal("2017-03-02T16:34:49Z", secret.CreatedAt);
            Assert.Equal("abc123", secret.Links.Self.Href);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void RevokeSecret(bool passCreds)
        {
            //ARRANGE            
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/{secretId}";
            var expectedResponse = @"";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            bool response;
            if (passCreds)
            {
                response = client.AccountClient.RevokeApiSecret(secretId, this.ApiKey, creds);
            }
            else
            {
                response = client.AccountClient.RevokeApiSecret(secretId, this.ApiKey);
            }

            //ASSERT
            Assert.True(response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void RevokeSecretAsync(bool passCreds)
        {
            //ARRANGE            
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{this.ApiKey}/secrets/{secretId}";
            var expectedResponse = @"";
            this.Setup(expectedUri, expectedResponse);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            bool response;
            if (passCreds)
            {
                response = await client.AccountClient.RevokeApiSecretAsync(secretId, this.ApiKey, creds);
            }
            else
            {
                response = await client.AccountClient.RevokeApiSecretAsync(secretId, this.ApiKey);
            }

            //ASSERT
            Assert.True(response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void SetSettings(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{this.RestUrl}/account/settings";
            var expectedRequestContents =
                $"moCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&drCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/delivery-receipt")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent =
                @"{""mo-callback-url"": ""https://example.com/webhooks/inbound-sms"",""dr-callback-url"": ""https://example.com/webhooks/delivery-receipt"",""max-outbound-request"": 15,""max-inbound-request"": 30,""max-calls-per-second"": 4}";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent,
                requestContent: expectedRequestContents);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.AccountSettingsResult result;
            if (passCreds)
            {
                result = client.AccountClient.ChangeAccountSettings(
                    new Accounts.AccountSettingsRequest
                    {
                        MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
                        DrCallBackUrl = "https://example.com/webhooks/delivery-receipt"
                    }, creds);
            }
            else
            {
                result = client.AccountClient.ChangeAccountSettings(new Accounts.AccountSettingsRequest
                {
                    MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
                    DrCallBackUrl = "https://example.com/webhooks/delivery-receipt"
                });
            }

            //ASSERT
            Assert.Equal("https://example.com/webhooks/delivery-receipt", result.DrCallbackurl);
            Assert.Equal("https://example.com/webhooks/inbound-sms", result.MoCallbackUrl);
            Assert.Equal(4, result.MaxCallsPerSecond);
            Assert.Equal(30, result.MaxInboundRequest);
            Assert.Equal(15, result.MaxOutboundRequest);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void SetSettingsAsync(bool passCreds)
        {
            //ARRANGE
            var expectedUri = $"{this.RestUrl}/account/settings";
            var expectedRequestContents =
                $"moCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/inbound-sms")}&drCallBackUrl={HttpUtility.UrlEncode("https://example.com/webhooks/delivery-receipt")}&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent =
                @"{""mo-callback-url"": ""https://example.com/webhooks/inbound-sms"",""dr-callback-url"": ""https://example.com/webhooks/delivery-receipt"",""max-outbound-request"": 15,""max-inbound-request"": 30,""max-calls-per-second"": 4}";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent,
                requestContent: expectedRequestContents);

            //ACT
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);
            var client = this.BuildVonageClient(creds);
            Accounts.AccountSettingsResult result;
            if (passCreds)
            {
                result = await client.AccountClient.ChangeAccountSettingsAsync(
                    new Accounts.AccountSettingsRequest
                    {
                        MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
                        DrCallBackUrl = "https://example.com/webhooks/delivery-receipt"
                    }, creds);
            }
            else
            {
                result = await client.AccountClient.ChangeAccountSettingsAsync(new Accounts.AccountSettingsRequest
                {
                    MoCallBackUrl = "https://example.com/webhooks/inbound-sms",
                    DrCallBackUrl = "https://example.com/webhooks/delivery-receipt"
                });
            }

            //ASSERT
            Assert.Equal("https://example.com/webhooks/delivery-receipt", result.DrCallbackurl);
            Assert.Equal("https://example.com/webhooks/inbound-sms", result.MoCallbackUrl);
            Assert.Equal(4, result.MaxCallsPerSecond);
            Assert.Equal(30, result.MaxInboundRequest);
            Assert.Equal(15, result.MaxOutboundRequest);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void TopUp(bool passCreds)
        {
            //ARRANGE            
            var expectedUri =
                $"{this.RestUrl}/account/top-up?trx=00X123456Y7890123Z&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent = @"{""response"":""abc123""}";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

            //Act
            var client = this.BuildVonageClient(creds);
            Accounts.TopUpResult response;
            if (passCreds)
            {
                response = client.AccountClient.TopUpAccountBalance(
                    new Accounts.TopUpRequest {Trx = "00X123456Y7890123Z"}, creds);
            }
            else
            {
                response = client.AccountClient.TopUpAccountBalance(new Accounts.TopUpRequest
                    {Trx = "00X123456Y7890123Z"});
            }

            Assert.Equal("abc123", response.Response);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async void TopUpAsync(bool passCreds)
        {
            //ARRANGE            
            var expectedUri =
                $"{this.RestUrl}/account/top-up?trx=00X123456Y7890123Z&api_key={this.ApiKey}&api_secret={this.ApiSecret}&";
            var expectedResponseContent = @"{""response"":""abc123""}";
            this.Setup(uri: expectedUri, responseContent: expectedResponseContent);
            var creds = Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

            //Act
            var client = this.BuildVonageClient(creds);
            Accounts.TopUpResult response;
            if (passCreds)
            {
                response = await client.AccountClient.TopUpAccountBalanceAsync(
                    new Accounts.TopUpRequest {Trx = "00X123456Y7890123Z"}, creds);
            }
            else
            {
                response = await client.AccountClient.TopUpAccountBalanceAsync(new Accounts.TopUpRequest
                    {Trx = "00X123456Y7890123Z"});
            }

            Assert.Equal("abc123", response.Response);
        }
    }
}