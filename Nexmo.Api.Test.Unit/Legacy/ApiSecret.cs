using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nexmo.Api.Test.Unit.Legacy
{
    public class ApiSecret : TestBase
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ListSecrets(bool passCreds)
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
            var expectedUri = $"https://api.nexmo.com/accounts/{ApiKey}/secrets?api_key={ApiKey}&api_secret={ApiSecret}&";
            Setup(expectedUri, expectedResponse);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            List<Api.ApiSecret.Secret> secrets;
            if (passCreds)
            {
                secrets = client.ApiSecret.List(ApiKey,creds);
            }
            else
            {
                secrets = client.ApiSecret.List(ApiKey);
            }

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secrets[0].Id);            
            Assert.Equal("abc123", secrets[0]._links.self.href);            
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetSecret(bool passCreds)
        {
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{ApiKey}/secrets/{secretId}?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            Setup(expectedUri, expectedResponse);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Api.ApiSecret.Secret secret;
            if (passCreds)
            {
                secret = client.ApiSecret.Get(ApiKey, secretId,creds);
            }
            else
            {
                secret = client.ApiSecret.Get(ApiKey, secretId);
            }
            

            //ASSERT
            Assert.Equal(secretId, secret.Id);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void CreateSecret(bool passCreds)
        {
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{ApiKey}/secrets/?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            var expectedRequestContent = @"{""secret"":""8675309""}";
            Setup(expectedUri, expectedResponse, expectedRequestContent);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            Api.ApiSecret.Secret secret;
            if (passCreds)
            {
                secret = client.ApiSecret.Create(ApiKey, "8675309",creds);
            }
            else
            {
                secret = client.ApiSecret.Create(ApiKey, "8675309");
            }
            //ASSERT
            Assert.Equal(secretId, secret.Id);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteSecret(bool passCreds)
        {
            var secretId = "ad6dc56f-07b5-46e1-a527-85530e625800";
            var expectedUri = $"https://api.nexmo.com/accounts/{ApiKey}/secrets/{secretId}?api_key={ApiKey}&api_secret={ApiSecret}&";
            var expectedResponse = @"{
                  ""_links"": {
                    ""self"": {
                           ""href"": ""abc123""
                      }
                    },
                  ""id"": ""ad6dc56f-07b5-46e1-a527-85530e625800"",
                  ""created_at"": ""2017-03-02T16:34:49Z""
                }";
            var expectedRequestContent = "null";
            Setup(expectedUri, expectedResponse, expectedRequestContent);

            //ACT
            var creds = new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret };
            var client = new Client(creds);
            bool revoked;
            if (passCreds)
            {
                revoked = client.ApiSecret.Delete(ApiKey, secretId, creds);
            }
            else
            {
                revoked = client.ApiSecret.Delete(ApiKey, secretId);
            }
            

            Assert.True(revoked);
        }

    }
}
