using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo.Api.Conversations;
using Newtonsoft.Json;

namespace Nexmo.Api.Test.Unit
{
    [TestClass]
    public class ConversationsTest : MockedWebTest
    {
        string _mockAppId = @"8675309-3145-6789-1011-123456789ab";
        string _mockRsaKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXAIBAAKBgQCqGKukO1De7zhZj6+H0qtjTkVxwTCpvKe4eCZ0FPqri0cb2JZfXJ/DgYSF6vUp
wmJG8wVQZKjeGcjDOL5UlsuusFncCzWBQ7RKNUSesmQRMSGkVb1/3j+skZ6UtW+5u09lHNsj6tQ5
1s1SPrCBkedbNf0Tp0GbMJDyR4e9T04ZZwIDAQABAoGAFijko56+qGyN8M0RVyaRAXz++xTqHBLh
3tx4VgMtrQ+WEgCjhoTwo23KMBAuJGSYnRmoBZM3lMfTKevIkAidPExvYCdm5dYq3XToLkkLv5L2
pIIVOFMDG+KESnAFV7l2c+cnzRMW0+b6f8mR1CJzZuxVLL6Q02fvLi55/mbSYxECQQDeAw6fiIQX
GukBI4eMZZt4nscy2o12KyYner3VpoeE+Np2q+Z3pvAMd/aNzQ/W9WaI+NRfcxUJrmfPwIGm63il
AkEAxCL5HQb2bQr4ByorcMWm/hEP2MZzROV73yF41hPsRC9m66KrheO9HPTJuo3/9s5p+sqGxOlF
L0NDt4SkosjgGwJAFklyR1uZ/wPJjj611cdBcztlPdqoxssQGnh85BzCj/u3WqBpE2vjvyyvyI5k
X6zk7S0ljKtt2jny2+00VsBerQJBAJGC1Mg5Oydo5NwD6BiROrPxGo2bpTbu/fhrT8ebHkTz2epl
U9VQQSQzY1oZMVX8i1m5WUTLPz2yLJIBQVdXqhMCQBGoiuSoSjafUhV7i1cEGpb88h5NBYZzWXGZ
37sJ5QsW+sJyoNde3xH8vdXhzU7eT82D6X/scw9RZz+/6rCJ4p0=
-----END RSA PRIVATE KEY-----";
        string _mockConversationId = "CON-afe887d8-d587-4280-9aae-dfa4c9227d5e";
        string _mockUserId = "USR-afe887d8-d587-4280-9aae-dfa4c9227d5e";
        string _genericConversationObject = "{" +
                  "\"id\": \"CON-afe887d8-d587-4280-9aae-dfa4c9227d5e\"," +
                  "\"name\": \"my-conversation\"," +
                  "\"display_name\": \"Conversation with Ashley\"," +
                  "\"image_url\": \"https://example.com/my-image.png\"," +
                  "\"timestamp\": {" +
                                "\"created\": \"2019-09-03T18:40:24.324Z\"" +
                  "}," +
                  "\"_links\": {" +
                                "\"self\": {" +
                                    "\"href\": \"https://api.nexmo.com/v0.1/conversations/CON-c4724477-72ac-438e-9fc0-1d3e2ff8728c\"" +
                                "}" +
                            "}," +
                  "\"properties\": {" +
                                "\"custom_data\": { }" +
                            "}" +
                "}";
        string _genericUserObject = "{" +
                  "\"id\": \"USR-afe887d8-d587-4280-9aae-dfa4c9227d5e\"," +
                  "\"name\": \"Alice\"," +
                  "\"display_name\": \"Alice\"," +
                  "\"image_url\": \"https://example.com/my-image.png\"," +
                  "\"timestamp\": {" +
                                "\"created\": \"2019-09-03T18:40:24.324Z\"" +
                  "}," +
                  "\"_links\": {" +
                                "\"self\": {" +
                                    "\"href\": \"https://api.nexmo.com/v0.1/users/USR-c4724477-72ac-438e-9fc0-1d3e2ff8728c\"" +
                                "}" +
                            "}," +
                  "\"properties\": {" +
                                "\"custom_data\": { }" +
                            "}" +
                "}";
        string _expectedPostRequest = 
            "{" +
                    "\"name\":\"Robert\"," +
                    "\"display_name\":\"Bob\"," +
                    "\"image_url\":\"https://www.example.com/image.png\"," +
                    "\"properties\":" +
                        "{\"custom_data\":" +
                            "{" +
                                "\"foo\":\"bar\"" +
                            "}" +
                        "}" +
                "}";
        string _expectedCursorResult = @"{
  ""page_size"": 10,
  ""_embedded"": {
    ""conversations"": [
      {
        ""id"": ""CON-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""name"": ""my-conversation"",
        ""display_name"": ""Conversation with Ashley"",
        ""image_url"": ""https://example.com/my-image.png"",
        ""timestamp"": {
          ""created"": ""2019-09-03T18:40:24.324Z""
        },
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-c4724477-72ac-438e-9fc0-1d3e2ff8728c""
          }
        }
      }
    ]
  },
  ""_links"": {
    ""first"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations?order=desc&page_size=10""
    },
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b82973771e7d390d274a58e301386d5762600a3ffd799bfb3fc5190c5a0d124cdd0fc72fe6e450506b18e4e2edf9fe84c7a0""
    },
    ""next"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b829a650e69a39197bfd4c949f4243f60dc4babb696afa404d2f44e7775e32b967f2a1a0bb8fb259c0999ba5a4e501eaab55""
    },
    ""prev"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations?order=desc&page_size=10&cursor=069626a3de11d2ec900dff5042197bd75f1ce41dafc3f2b2481eb9151086e59aae9dba3e3a8858dc355232d499c310fbfbec43923ff657c0de8d49ffed9f7edb""
    }
  }
}";
        string _expectedUserCursorResult = @"{
  ""page_size"": 10,
  ""_embedded"": {
    ""users"": [
      {
        ""id"": ""USR-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""name"": ""Alice"",
        ""display_name"": ""Alice"",
        ""image_url"": ""https://example.com/my-image.png"",
        ""timestamp"": {
          ""created"": ""2019-09-03T18:40:24.324Z""
        },
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/users/USR-c4724477-72ac-438e-9fc0-1d3e2ff8728c""
          }
        }
      }
    ]
  },
  ""_links"": {
    ""first"": {
      ""href"": ""https://api.nexmo.com/v0.1/users?order=desc&page_size=10""
    },
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/users?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b82973771e7d390d274a58e301386d5762600a3ffd799bfb3fc5190c5a0d124cdd0fc72fe6e450506b18e4e2edf9fe84c7a0""
    },
    ""next"": {
      ""href"": ""https://api.nexmo.com/v0.1/users?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b829a650e69a39197bfd4c949f4243f60dc4babb696afa404d2f44e7775e32b967f2a1a0bb8fb259c0999ba5a4e501eaab55""
    },
    ""prev"": {
      ""href"": ""https://api.nexmo.com/v0.1/users?order=desc&page_size=10&cursor=069626a3de11d2ec900dff5042197bd75f1ce41dafc3f2b2481eb9151086e59aae9dba3e3a8858dc355232d499c310fbfbec43923ff657c0de8d49ffed9f7edb""
    }
  }
}";
        string _mockMemberId = "MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e";
        string _expectedPostForMemberCreateUserId = @"{""user_id"":""USR-afe887d8-d587-4280-9aae-dfa4c9227d5e"",""channel"":{""type"":""app""},""action"":""join""}";
        string _expectedPostForMemberCreateUserName = @"{""user_name"":""Alice"",""channel"":{""type"":""app""},""action"":""join""}";
        string _expectedPostForMemberUpdate = @"{""state"":""join"",""channel"":{""type"":""app""}}";
        string _expectMemberResponse = @"{
  ""id"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
  ""name"": ""ashley"",
  ""display_name"": ""Ashley Arthur"",
  ""user_id"": ""USR-2c52f0ec-7a48-4b52-9d47-df47482b2b7e"",
  ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
  ""state"": ""JOINED"",
  ""_links"": {
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/members/MEM-e784d5d1-dff2-424a-9de7-bc34f1901177""
    }
  },
  ""timestamp"": {
    ""invited"": ""2019-09-03T18:40:24.324Z"",
    ""joined"": ""2019-09-12T16:27:07.450Z"",
    ""left"": ""2019-09-13T02:16:55.390Z""
  },
  ""channel"": {
    ""type"": ""app""
  },
  ""initiator"": {
    ""invited"": {
      ""is_system"": true
    },
    ""joined"": {
      ""is_system"": true
    }
  },
  ""media"": {
    ""audio_settings"": {
      ""enabled"": false,
      ""earmuffed"": false,
      ""muted"": false
    }
  }
}";

        string _ExpectedCursorListMembers = @"
{
  ""page_size"": 10,
  ""_embedded"": {
    ""members"": [
      {
        ""id"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""name"": ""ashley"",
        ""display_name"": ""Ashley Arthur"",
        ""user_id"": ""USR-2c52f0ec-7a48-4b52-9d47-df47482b2b7e"",
        ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
        ""state"": ""JOINED"",
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/members/MEM-e784d5d1-dff2-424a-9de7-bc34f1901177""
          }
}
      }
    ]
  },
  ""_links"": {
    ""first"": {
      ""href"": ""https://api.nexmo.com/v0.1/members?order=desc&page_size=10""
    },
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/members?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b82973771e7d390d274a58e301386d5762600a3ffd799bfb3fc5190c5a0d124cdd0fc72fe6e450506b18e4e2edf9fe84c7a0""
    },
    ""next"": {
      ""href"": ""https://api.nexmo.com/v0.1/members?order=desc&page_size=10&cursor=88b395c167da4d94e929705cbd63b829a650e69a39197bfd4c949f4243f60dc4babb696afa404d2f44e7775e32b967f2a1a0bb8fb259c0999ba5a4e501eaab55""
    },
    ""prev"": {
      ""href"": ""https://api.nexmo.com/v0.1/members?order=desc&page_size=10&cursor=069626a3de11d2ec900dff5042197bd75f1ce41dafc3f2b2481eb9151086e59aae9dba3e3a8858dc355232d499c310fbfbec43923ff657c0de8d49ffed9f7edb""
    }
  }
}";
        string _postRequestCreateTextEvent = @"{""type"":""text"",""body"":{""text"":""foo""},""to"":""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",""from"":""MEM-e46d9542-752a-4786-8f12-25a2e623a793""}";
        string _postRequestCreateCustomEvent = @"{""type"":""custom:foo"",""body"":{""foo"":""bar""},""to"":""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",""from"":""MEM-e46d9542-752a-4786-8f12-25a2e623a793""}";
        string _postResultTextEvent = @"{
  ""body"": {
    ""text"": ""Hello World""
  },
  ""type"": ""text"",
  ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
  ""id"": 9,
  ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
  ""timestamp"": ""2019-09-12T19:49:21.823Z"",
  ""_links"": {
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
    }
  }
}";
        string _postResultCustomEvent = @"{
  ""body"": {
    ""foo"": ""bar""
  },
  ""type"": ""custom:foo"",
  ""id"": 9,
  ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
  ""timestamp"": ""2019-09-12T19:49:21.823Z"",
  ""_links"": {
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
    }
  }
}";
        string _cursorListEvents = @"{
  ""page_size"": 10,
  ""_embedded"": {
    ""events"": [
      {
        ""body"": {
          ""text"": ""Hello World""
        },
        ""type"": ""text"",
        ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
        ""id"": 9,
        ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""timestamp"": ""2019-09-12T19:49:21.823Z"",
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
          }
        }
      },
      {
        ""body"": {
          ""my"": ""Custom Data""
        },
        ""type"": ""custom:my_event"",
        ""id"": 9,
        ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""timestamp"": ""2019-09-12T19:49:21.823Z"",
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
          }
        }
      },
      {
        ""type"": ""member:invited"",
        ""id"": 9,
        ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""timestamp"": ""2019-09-12T19:49:21.823Z"",
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
          }
        },
        ""body"": {
          ""id"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
          ""name"": ""ashley"",
          ""display_name"": ""Ashley Arthur"",
          ""user_id"": ""USR-2c52f0ec-7a48-4b52-9d47-df47482b2b7e"",
          ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
          ""state"": ""JOINED"",
          ""_links"": {
            ""self"": {
              ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/members/MEM-e784d5d1-dff2-424a-9de7-bc34f1901177""
            }
          },
          ""timestamp"": {
            ""invited"": ""2019-09-03T18:40:24.324Z"",
            ""joined"": ""2019-09-12T16:27:07.450Z"",
            ""left"": ""2019-09-13T02:16:55.390Z""
          },
          ""channel"": {
            ""type"": ""app""
          },
          ""initiator"": {
            ""invited"": {
              ""is_system"": true
            },
            ""joined"": {
              ""is_system"": true
            }
          },
          ""media"": {
            ""audio_settings"": {
              ""enabled"": false,
              ""earmuffed"": false,
              ""muted"": false
            }
          }
        }
      },
      {
        ""type"": ""member:left"",
        ""id"": 9,
        ""from"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
        ""timestamp"": ""2019-09-12T19:49:21.823Z"",
        ""_links"": {
          ""self"": {
            ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events/9""
          }
        },
        ""body"": {
          ""id"": ""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",
          ""name"": ""ashley"",
          ""display_name"": ""Ashley Arthur"",
          ""user_id"": ""USR-2c52f0ec-7a48-4b52-9d47-df47482b2b7e"",
          ""conversation_id"": ""CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625"",
          ""state"": ""JOINED"",
          ""_links"": {
            ""self"": {
              ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/members/MEM-e784d5d1-dff2-424a-9de7-bc34f1901177""
            }
          },
          ""timestamp"": {
            ""invited"": ""2019-09-03T18:40:24.324Z"",
            ""joined"": ""2019-09-12T16:27:07.450Z"",
            ""left"": ""2019-09-13T02:16:55.390Z""
          },
          ""channel"": {
            ""type"": ""app""
          },
          ""initiator"": {
            ""invited"": {
              ""is_system"": true
            },
            ""joined"": {
              ""is_system"": true
            }
          },
          ""media"": {
            ""audio_settings"": {
              ""enabled"": false,
              ""earmuffed"": false,
              ""muted"": false
            }
          }
        }
      }
    ]
  },
  ""_links"": {
    ""first"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events?page_size=10""
    },
    ""self"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events?page_size=10&cursor=a30e3b7a3dcda1434f64bbb1a5fa489b""
    },
    ""next"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events?page_size=10&cursor=4db03d9254d1cdaecc7b1fc15b6bf1a81f3d3151191d784f1327893f8dc96416""
    },
    ""prev"": {
      ""href"": ""https://api.nexmo.com/v0.1/conversations/CON-92a44c64-7e4e-485f-a0c4-1f2adfc44625/events?page_size=10&cursor=84963f79fd25785be9706bd38bfd30c264f71964fa4edc8d8b4dd5f30bbd9f7c""
    }
  }
}";
        int _mockEventId = 9;

        Client _client;
        [TestInitialize]
        public void TestInitialize() {
            _client = new Client(new Request.Credentials()
            {
                ApplicationId = _mockAppId,
                ApplicationKey = _mockRsaKey
            });
        }
        [TestMethod]
        public void TestCreateConversation()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations",
                _genericConversationObject, _expectedPostRequest);
            
            var result = _client.Conversation.CreateConversation(new Conversations.CreateConversationRequest()
            {
                Name = "Robert",
                DisplayName = "Bob",
                ImageUrl = "https://www.example.com/image.png",
                Properties = new Conversations.Properties(new Foo())
            });
            Assert.AreEqual(result.Id, _mockConversationId);
        }

        [TestMethod]
        public void TestUpdateConversation()
        {   
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}",
                _genericConversationObject, _expectedPostRequest);
            var result = _client.Conversation.UpdateConversation(new Conversations.UpdateConversationRequest()
            {
                Name = "Robert",
                DisplayName = "Bob",
                ImageUrl = "https://www.example.com/image.png",
                Properties = new Conversations.Properties(new Foo())
            },
            _mockConversationId
            );
            Assert.AreEqual(result.Id, _mockConversationId);
        }

        [TestMethod]
        public void TestGetConversation()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}?", _genericConversationObject);

            var result = _client.Conversation.GetConversation(_mockConversationId);
            Assert.AreEqual(result.Id, _mockConversationId);
        }

        [TestMethod]
        public void TestListConversations()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations?page_size=10&order=asc&", _expectedCursorResult);
            var result = _client.Conversation.ListConversations(new Conversations.CursorListParams() { order = "asc", page_size = 10 });
            Assert.AreEqual(1, result.Embedded.Conversations.Count);
        }

        [TestMethod]
        public void TestDeleteConversations()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}", "");
            SetExpectStatus(System.Net.HttpStatusCode.NoContent);
            var result = _client.Conversation.DeleteConversation(_mockConversationId);
            Assert.AreEqual(result, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestCreateUser()
        {
            SetExpect($"{ApiUrl}/v0.1/users", _genericUserObject, _expectedPostRequest);
            var result = _client.Conversation.CreateUser(new Conversations.CreateUserRequest()
            {
                Name = "Robert",
                DisplayName = "Bob",
                ImageUrl = "https://www.example.com/image.png",
                Properties = new Conversations.Properties(new Foo())
            });
            Assert.AreEqual(result.Id, _mockUserId);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            SetExpect($"{ApiUrl}/v0.1/Users/{_mockUserId}", _genericUserObject, _expectedPostRequest);
            var result = _client.Conversation.UpdateUser(new Conversations.UpdateUserRequest()
            {
                Name = "Robert",
                DisplayName = "Bob",
                ImageUrl = "https://www.example.com/image.png",
                Properties = new Conversations.Properties(new Foo())
            },_mockUserId);
            Assert.AreEqual(result.Id, _mockUserId);
        }

        [TestMethod]
        public void TestGetUser()
        {
            SetExpect($"{ApiUrl}/v0.1/Users/{_mockUserId}?", _genericUserObject);
            var result = _client.Conversation.GetUser(_mockUserId);
            Assert.AreEqual(result.Id, _mockUserId);
        }

        [TestMethod]
        public void TestListUsers()
        {
            SetExpect($"{ApiUrl}/v0.1/users?page_size=10&order=asc&", _expectedUserCursorResult);
            var result = _client.Conversation.ListUsers(new Conversations.CursorListParams() { order = "asc", page_size = 10 });
            Assert.AreEqual(1, result.Embedded.Users.Count);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            SetExpect($"{ApiUrl}/v0.1/users/{_mockUserId}","");
            SetExpectStatus(System.Net.HttpStatusCode.NoContent);
            var result = _client.Conversation.DeleteUser(_mockUserId);
            Assert.AreEqual(result, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestCreateMemberFromUserName()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members", _expectMemberResponse, _expectedPostForMemberCreateUserName);
            var request = new CreateMemberWithNameRequest()
            {
                Action = CreateMemberRequestBase.CreateMemberAction.join,
                Channel = new Channel
                {
                    Type = Channel.MemberChannelType.app
                },
                Name = "Alice"
            };
            var response = _client.Conversation.CreateMember(request,_mockConversationId);
            Assert.AreEqual(_mockMemberId, response.Id);
        }

        [TestMethod]
        public void TestCreateMemberFromId()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members", _expectMemberResponse, _expectedPostForMemberCreateUserId);
            var request = new CreateMemberWithIdRequest()
            {
                Id = _mockUserId,
                Action = CreateMemberRequestBase.CreateMemberAction.join,
                Channel = new Channel
                {
                    Type = Channel.MemberChannelType.app
                }
            };
            var response = _client.Conversation.CreateMember(request, _mockConversationId);
            Assert.AreEqual(_mockMemberId, response.Id);
        }

        [TestMethod]
        public void TestUpdateMember()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members/{_mockMemberId}", _expectMemberResponse, _expectedPostForMemberUpdate);
            var request = new UpdateMemberRequest()
            {
                Channel = new Channel() { Type = Channel.MemberChannelType.app },
                State = UpdateMemberRequest.MemberState.join
            };
            var response = _client.Conversation.UpdateMember(request, _mockMemberId, _mockConversationId);
            Assert.AreEqual(_mockMemberId, response.Id);
        }

        [TestMethod]
        public void TestGetMember()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members/{_mockMemberId}?", _expectMemberResponse);
            var response = _client.Conversation.GetMember(_mockMemberId, _mockConversationId);
            Assert.AreEqual(_mockMemberId, response.Id);
        }

        [TestMethod]
        public void TestListMembers()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members?page_size=10&order=asc&", _ExpectedCursorListMembers);
            var result = _client.Conversation.ListMembers(new Conversations.CursorListParams() { order = "asc", page_size = 10 },_mockConversationId);
            Assert.AreEqual(result.Embedded.Members.Count, 1);
        }

        [TestMethod]
        public void TestDeleteMembers()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/members/{_mockMemberId}", "");
            SetExpectStatus(System.Net.HttpStatusCode.NoContent);
            var result = _client.Conversation.DeleteMember(_mockMemberId, _mockConversationId);
            Assert.AreEqual(result, System.Net.HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestCreateEventText()
        {
            //""to"":""MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"",""from"":""MEM-e46d9542-752a-4786-8f12-25a2e623a793""
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/events", _postResultTextEvent, _postRequestCreateTextEvent);
            var request = new CreateTextEventRequest()
            {
                Body = new TextEventBody()
                {
                    Text = "foo"
                },
                From = "MEM-e46d9542-752a-4786-8f12-25a2e623a793",
                To = "MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e"
            };
            var response = _client.Conversation.CreateEvent(request, _mockConversationId);
            Assert.AreEqual(_mockEventId, response.Id);
        }

        [TestMethod]
        public void TextCreateEventCustom()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/events", _postResultCustomEvent, _postRequestCreateCustomEvent);
            var request = new CreateCustomEventRequest<Foo>()
            {
                Body = new Foo()
                {
                    foo = "bar"
                },
                From = "MEM-e46d9542-752a-4786-8f12-25a2e623a793",
                To = "MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e",
                Type="custom:foo"
            };
            var response = _client.Conversation.CreateEvent(request, _mockConversationId);
            var foo = response.GetBodyAsType<Foo>();
            Assert.IsTrue(foo.foo == "bar");
            Assert.AreEqual(response.Id, _mockEventId);

        }

        [TestMethod]
        public void TestListEvents()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/events?start_id=0&end_id=9&", _cursorListEvents);
            var response = _client.Conversation.ListEvents(new EventListParams() { start_id = "0", end_id = "9" },_mockConversationId);
            foreach (var e in response.Embedded.Events)
            {
                if (e.Type=="member:invited")
                {
                    var member = e.GetBodyAsType<Member>();
                    Assert.AreEqual(member.Id, "MEM-afe887d8-d587-4280-9aae-dfa4c9227d5e");
                }
            }
            Assert.AreEqual(response.Embedded.Events.Count, 4);
        }

        [TestMethod]
        public void TestGetEvents()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/events/{_mockEventId}?", _postResultTextEvent);
            var response = _client.Conversation.GetEvent(_mockEventId, _mockConversationId);
            Assert.AreEqual(_mockEventId, response.Id);
        }
        [TestMethod]
        public void TestDeleteEvent()
        {
            SetExpect($"{ApiUrl}/v0.1/conversations/{_mockConversationId}/events/{_mockEventId}", "");
            SetExpectStatus(System.Net.HttpStatusCode.NoContent);
            var response = _client.Conversation.DeleteEvent(_mockEventId,_mockConversationId);
            Assert.AreEqual(response, System.Net.HttpStatusCode.NoContent);
        }

        class Foo
        {
            public string foo { get; set; } = "bar";
        }
    }
}
