using System;
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
        public void Build_ShouldSetDisplayName() =>
            CreateConversationRequest.Build()
                .WithDisplayName(new string('a', 50))
                .Create()
                .Map(request => request.DisplayName)
                .Should()
                .BeSuccess(new string('a', 50));

        [Fact]
        public void Build_ShouldSetName() =>
            CreateConversationRequest.Build()
                .WithName(new string('a', 100))
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(new string('a', 100));

        [Fact]
        public void Build_ShouldSetUri() =>
            CreateConversationRequest.Build()
                .WithUri(new Uri("https://example.com"))
                .Create()
                .Map(request => request.Uri)
                .Should()
                .BeSuccess(new Uri("https://example.com"));
    }
}