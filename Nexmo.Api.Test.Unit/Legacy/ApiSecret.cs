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
        [Fact]
        public void ListSecrets()
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
            var client = new Client(new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var secrets = client.ApiSecret.List(ApiKey);

            //ASSERT
            Assert.Equal("ad6dc56f-07b5-46e1-a527-85530e625800", secrets[0].Id);            
            Assert.Equal("abc123", secrets[0]._links.self.href);            
        }

        [Fact]
        public void GetSecret()
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
            var client = new Client(new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var secret = client.ApiSecret.Get(ApiKey, secretId);

            //ASSERT
            Assert.Equal(secretId, secret.Id);
        }

        [Fact]
        public void CreateSecret()
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
            var client = new Client(new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var secret = client.ApiSecret.Create(ApiKey, "8675309");

            //ASSERT
            Assert.Equal(secretId, secret.Id);
        }

        [Fact]
        public void DeleteSecret()
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
            var client = new Client(new Request.Credentials { ApiKey = ApiKey, ApiSecret = ApiSecret });
            var revoked = client.ApiSecret.Delete(ApiKey, secretId);

            Assert.True(revoked);
        }

    }
}
