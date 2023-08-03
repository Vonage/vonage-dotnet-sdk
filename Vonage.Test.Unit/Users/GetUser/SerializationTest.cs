using System;
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Users;
using Xunit;

namespace Vonage.Test.Unit.Users.GetUser
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<User>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(this.VerifyUser);

        internal void VerifyUser(User success)
        {
            success.Id.Should().Be("USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
            success.Name.Should().Be("my_user_name");
            success.DisplayName.Should().Be("My User Name");
            success.ImageUrl.Should().Be(new Uri("https://example.com/image.png"));
            this.helper.Serializer.DeserializeObject<CustomData>(success.Properties["custom_data"].ToString()).Should()
                .BeSuccess(new CustomData("custom_value"));
            success.Channels.Pstn.Should().BeEquivalentTo(new[] {new ChannelPSTN(123457)});
            success.Channels.Sip.Should().BeEquivalentTo(new[]
                {new ChannelSIP("sip:4442138907@sip.example.com;transport=tls", "New SIP", "Password")});
            success.Channels.Vbc.Should().BeEquivalentTo(new[] {new ChannelVBC("403")});
            success.Channels.WebSocket.Should().BeEquivalentTo(new[]
            {
                new ChannelWebSocket("wss://example.com/socket", "audio/l16;rate=16000",
                    new Dictionary<string, string> {{"customer_id", "ABC123"}}),
            });
            success.Channels.Sms.Should().BeEquivalentTo(new[] {new ChannelSMS("447700900000")});
            success.Channels.Mms.Should().BeEquivalentTo(new[] {new ChannelMMS("447700900000")});
            success.Channels.Viber.Should().BeEquivalentTo(new[] {new ChannelViber("447700900000")});
            success.Channels.WhatsApp.Should().BeEquivalentTo(new[] {new ChannelWhatsApp("447700900000")});
            success.Channels.Messenger.Should().BeEquivalentTo(new[] {new ChannelMessenger("12345abcd")});
            success.Links.Self.Href.Should()
                .Be(new Uri("https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec"));
        }
    }
}