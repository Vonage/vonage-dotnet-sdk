using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.CreateConversation;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
using PhoneNumber = Vonage.Conversations.PhoneNumber;

namespace Vonage.Test.Conversations.CreateConversation
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper = new SerializationTestHelper(
            typeof(SerializationTest).Namespace,
            JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() => this.helper.Serializer
            .DeserializeObject<Conversation>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(ConversationTests.VerifyExpectedResponse);

        [Fact]
        public void ShouldDeserialize200Minimal() => this.helper.Serializer
            .DeserializeObject<Conversation>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(ConversationTests.VerifyMinimalResponse);

        [Fact]
        public void ShouldSerialize() => BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeEmpty() => BuildEmptyRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

        internal static Result<CreateConversationRequest> BuildRequest() =>
            CreateConversationRequest.Build()
                .WithName("customer_chat")
                .WithDisplayName("Customer Chat")
                .WithImageUrl(new Uri("https://example.com/image.png"))
                .WithProperties(new Properties(60, "string",
                    "string", new Dictionary<string, string>
                    {
                        {"property1", "string"},
                        {"property2", "string"},
                    }))
                .WithCallback(new Callback(new Uri("http://example.com"), "string",
                    new CallbackParameters("string", new Uri("http://example.com")), HttpMethod.Post))
                .WithNumber(new PhoneNumber("447700900000"))
                .WithNumber(new SipNumber("sip:+Htg:;xa", "string", "string"))
                .WithNumber(new AppNumber("string"))
                .WithNumber(new WebSocketNumber("ws://example.com:8080", "string"))
                .WithNumber(new VbcNumber("447700900000"))
                .Create();

        internal static Result<CreateConversationRequest> BuildEmptyRequest() =>
            CreateConversationRequest.Build()
                .Create();
    }
}