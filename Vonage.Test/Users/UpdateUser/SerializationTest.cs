﻿using System;
using System.Collections.Generic;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Vonage.Users;
using Vonage.Users.UpdateUser;
using Xunit;

namespace Vonage.Test.Users.UpdateUser
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<User>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(E2EBase.VerifyUser);

        [Fact]
        public void ShouldSerialize() =>
            UpdateUserRequest
                .Build()
                .WithId("Some random id")
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
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeEmpty() =>
            UpdateUserRequest
                .Build()
                .WithId("Some random id")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}