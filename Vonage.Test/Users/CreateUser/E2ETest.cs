using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Users;
using Vonage.Users.CreateUser;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Users.CreateUser;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task CreateEmptyUser()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/users")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerializeEmpty)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.UsersClient
            .CreateUserAsync(CreateUserRequest
                .Build()
                .Create())
            .Should()
            .BeSuccessAsync(VerifyUser);
    }

    [Fact]
    public async Task CreateUser()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v1/users")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.UsersClient
            .CreateUserAsync(CreateUserRequest
                .Build()
                .WithName("my_user_name")
                .WithDisplayName("My User Name")
                .WithImageUrl(new Uri("https://example.com/image.png"))
                .WithUserProperty("custom_key", "custom_value")
                .WithChannel(new ChannelPstn(123457))
                .WithChannel(new ChannelSip("sip:4442138907@sip.example.com;transport=tls", "New SIP", "Password"))
                .WithChannel(new ChannelVbc("403"))
                .WithChannel(new ChannelWebSocket("wss://example.com/socket", "audio/l16;rate=16000",
                    new Dictionary<string, string> {{"customer_id", "ABC123"}}))
                .WithChannel(new ChannelSms("447700900000"))
                .WithChannel(new ChannelMms("447700900000"))
                .WithChannel(new ChannelWhatsApp("447700900000"))
                .WithChannel(new ChannelViber("447700900000"))
                .WithChannel(new ChannelMessenger("12345abcd"))
                .Create())
            .Should()
            .BeSuccessAsync(VerifyUser);
    }
}