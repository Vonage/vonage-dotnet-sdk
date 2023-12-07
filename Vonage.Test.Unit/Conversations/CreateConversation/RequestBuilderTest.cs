using System;
using System.Collections.Generic;
using System.Net.Http;
using Vonage.Common.Test.Extensions;
using Vonage.Conversations.CreateConversation;
using Xunit;

namespace Vonage.Test.Unit.Conversations.CreateConversation
{
    public class RequestBuilderTest
    {
        [Fact]
        public void Build_ShouldReturnDefaultValues_GivenNoValuesHaveBeenSet() =>
            CreateConversationRequest.Build()
                .Create()
                .Should()
                .BeSuccess();

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenDisplayNameIsProvidedButEmpty(string invalidDisplayName) =>
            CreateConversationRequest.Build()
                .WithDisplayName(invalidDisplayName)
                .Create()
                .Map(request => request.DisplayName)
                .Should()
                .BeParsingFailure("DisplayName cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenDisplayNameLengthIsAbove50Characters() =>
            CreateConversationRequest.Build()
                .WithDisplayName(new string('a', 51))
                .Create()
                .Map(request => request.DisplayName)
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
            CreateConversationRequest.Build()
                .WithCallback(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), new HttpMethod(method)))
                .Create()
                .Map(request => request.Callback)
                .Should()
                .BeParsingFailure("Callback HttpMethod must be GET or POST.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenNameIsProvidedButEmpty(string invalidName) =>
            CreateConversationRequest.Build()
                .WithName(invalidName)
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeParsingFailure("Name cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenNameLengthIsAbove100Characters() =>
            CreateConversationRequest.Build()
                .WithName(new string('a', 101))
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeParsingFailure("Name length cannot be higher than 100.");

        [Fact]
        public void Build_ShouldSetCallback() =>
            CreateConversationRequest.Build()
                .WithCallback(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), HttpMethod.Get))
                .Create()
                .Map(request => request.Callback)
                .Should()
                .BeSuccess(new Callback(new Uri("https://example.com"), "mask",
                    new CallbackParameters("appId", new Uri("https://example.com")), HttpMethod.Get));

        [Fact]
        public void Build_ShouldSetDisplayName() =>
            CreateConversationRequest.Build()
                .WithDisplayName(new string('a', 50))
                .Create()
                .Map(request => request.DisplayName)
                .Should()
                .BeSuccess(new string('a', 50));

        [Fact]
        public void Build_ShouldSetImageUrl() =>
            CreateConversationRequest.Build()
                .WithImageUrl(new Uri("https://example.com"))
                .Create()
                .Map(request => request.ImageUrl)
                .Should()
                .BeSuccess(new Uri("https://example.com"));

        [Fact]
        public void Build_ShouldSetName() =>
            CreateConversationRequest.Build()
                .WithName(new string('a', 100))
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(new string('a', 100));

        [Fact]
        public void Build_ShouldSetProperties() =>
            CreateConversationRequest.Build()
                .WithProperties(new Properties(55, "Fake", "hello-there",
                    new Dictionary<string, string> {{"temp1", "123"}, {"temp12", "456"}}))
                .Create()
                .Map(request => request.Properties)
                .Should()
                .BeSuccess(properties =>
                    properties.Should().BeSome(new Properties(55, "Fake", "hello-there",
                        new Dictionary<string, string> {{"temp1", "123"}, {"temp12", "456"}})));
    }
}