using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Conversations;
using Vonage.Conversations.UpdateConversation;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Conversations.UpdateConversation
{
    public class RequestBuilderTest
    {
        private const string ConversationId = "CON-1234";

        [Fact]
        public void Build_ShouldHaveNoCallback_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.Callback)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldHaveNoDisplayName_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.DisplayName)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldHaveNoName_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldHaveNoNumbers_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.Numbers)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldHaveNoProperties_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.Properties)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldHaveNoUri_GivenDefault() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.ImageUrl)
                .Should()
                .BeSuccess(value => value.Should().BeNone());

        [Fact]
        public void Build_ShouldReturnFailure_GivenCallbackEventMaskLengthIsAbove200Characters() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithCallback(new Callback(new Uri("https://example.com"), new string('a', 201),
                    new CallbackParameters("appId", new Uri("https://example.com")), HttpMethod.Get))
                .Create()
                .Should()
                .BeParsingFailure("Callback EventMask length cannot be higher than 200.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenConversationIdIsNullOrWhitespace(string invalidId) =>
            UpdateConversationRequest.Build()
                .WithConversationId(invalidId)
                .Create()
                .Should()
                .BeParsingFailure("ConversationId cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenDisplayNameIsProvidedButEmpty(string invalidDisplayName) =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithDisplayName(invalidDisplayName)
                .Create()
                .Should()
                .BeParsingFailure("DisplayName cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenDisplayNameLengthIsAbove50Characters() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithDisplayName(new string('a', 51))
                .Create()
                .Should()
                .BeParsingFailure("DisplayName length cannot be higher than 50.");

        [Theory]
        [InlineData("PUT")]
        [InlineData("DELETE")]
        [InlineData("HEAD")]
        [InlineData("OPTIONS")]
        [InlineData("TRACE")]
        [InlineData("PATCH")]
        [InlineData("CONNECT")]
        public void Build_ShouldReturnFailure_GivenHttpMethodIsNotGetOrPost(string method) =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithCallback(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), new HttpMethod(method)))
                .Create()
                .Should()
                .BeParsingFailure("Callback HttpMethod must be GET or POST.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenNameIsProvidedButEmpty(string invalidName) =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithName(invalidName)
                .Create()
                .Should()
                .BeParsingFailure("Name cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenNameLengthIsAbove100Characters() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithName(new string('a', 101))
                .Create()
                .Should()
                .BeParsingFailure("Name length cannot be higher than 100.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenPropertiesCustomSortKeyLengthIsAbove200Characters() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithProperties(new Properties(10, "type", new string('a', 201), new Dictionary<string, string>()))
                .Create()
                .Should()
                .BeParsingFailure("Properties CustomSortKey length cannot be higher than 200.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenPropertiesTypeLengthIsAbove200Characters() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithProperties(new Properties(10, new string('a', 201), "key", new Dictionary<string, string>()))
                .Create()
                .Should()
                .BeParsingFailure("Properties Type length cannot be higher than 200.");

        [Fact]
        public void Build_ShouldSetCallback() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithCallback(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), HttpMethod.Get))
                .Create()
                .Map(request => request.Callback)
                .Should()
                .BeSuccess(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), HttpMethod.Get));

        [Fact]
        public void Build_ShouldSetConversationId() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .Create()
                .Map(request => request.ConversationId)
                .Should()
                .BeSuccess(ConversationId);

        [Fact]
        public void Build_ShouldSetDisplayName() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithDisplayName(new string('a', 50))
                .Create()
                .Map(request => request.DisplayName)
                .Should()
                .BeSuccess(new string('a', 50));

        [Fact]
        public void Build_ShouldSetImageUrl() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithImageUrl(new Uri("https://example.com"))
                .Create()
                .Map(request => request.ImageUrl)
                .Should()
                .BeSuccess(new Uri("https://example.com"));

        [Fact]
        public void Build_ShouldSetName() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithName(new string('a', 100))
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(new string('a', 100));

        [Fact]
        public void Build_ShouldSetNumbers() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithNumber(new PhoneNumber("447700900000"))
                .WithNumber(new AppNumber("John Doe"))
                .Create()
                .Map(request => request.Numbers)
                .Should()
                .BeSuccess(numbers => numbers.Should().BeSome(new List<INumber>
                    {new PhoneNumber("447700900000"), new AppNumber("John Doe")}));

        [Fact]
        public void Build_ShouldSetProperties() =>
            UpdateConversationRequest.Build()
                .WithConversationId(ConversationId)
                .WithProperties(new Properties(55, new string('a', 200), "hello-there",
                    new Dictionary<string, string> {{"temp1", "123"}, {"temp12", "456"}}))
                .Create()
                .Map(request => request.Properties)
                .Should()
                .BeSuccess(properties =>
                    properties.Should().BeSome(new Properties(55, new string('a', 200), "hello-there",
                        new Dictionary<string, string> {{"temp1", "123"}, {"temp12", "456"}})));
    }
}