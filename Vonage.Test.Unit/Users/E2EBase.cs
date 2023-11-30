using System;
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Test.Unit.TestHelpers;
using Vonage.Users;

namespace Vonage.Test.Unit.Users
{
    public class E2EBase
    {
        protected E2EBase(string serializationNamespace) : this() => this.Serialization =
            new SerializationTestHelper(serializationNamespace, JsonSerializer.BuildWithSnakeCase());

        protected E2EBase() => this.Helper = TestingContext.WithBearerCredentials("Vonage.Url.Api");
        internal readonly TestingContext Helper;
        internal readonly SerializationTestHelper Serialization;

        internal struct CustomData
        {
            public string CustomKey { get; set; }
            public CustomData(string customKey) => this.CustomKey = customKey;
        }

        internal static void VerifyUser(User success)
        {
            success.Id.Should().Be("USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
            success.Name.Should().Be("my_user_name");
            success.DisplayName.Should().Be("My User Name");
            success.ImageUrl.Should().Be(new Uri("https://example.com/image.png"));
            JsonSerializer.BuildWithSnakeCase()
                .DeserializeObject<CustomData>(success.Properties["custom_data"].ToString()).Should()
                .BeSuccess(new CustomData("custom_value"));
            success.Channels.Pstn.Should().BeEquivalentTo(new[] {new ChannelPstn(123457)});
            success.Channels.Sip.Should().BeEquivalentTo(new[]
                {new ChannelSip("sip:4442138907@sip.example.com;transport=tls", "New SIP", "Password")});
            success.Channels.Vbc.Should().BeEquivalentTo(new[] {new ChannelVbc("403")});
            success.Channels.WebSocket.Should().BeEquivalentTo(new[]
            {
                new ChannelWebSocket("wss://example.com/socket", "audio/l16;rate=16000",
                    new Dictionary<string, string> {{"customer_id", "ABC123"}}),
            });
            success.Channels.Sms.Should().BeEquivalentTo(new[] {new ChannelSms("447700900000")});
            success.Channels.Mms.Should().BeEquivalentTo(new[] {new ChannelMms("447700900000")});
            success.Channels.Viber.Should().BeEquivalentTo(new[] {new ChannelViber("447700900000")});
            success.Channels.WhatsApp.Should().BeEquivalentTo(new[] {new ChannelWhatsApp("447700900000")});
            success.Channels.Messenger.Should().BeEquivalentTo(new[] {new ChannelMessenger("12345abcd")});
            success.Links.Self.Href.Should()
                .Be(new Uri("https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec"));
        }
    }
}