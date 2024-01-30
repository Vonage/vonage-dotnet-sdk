using System;
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.Users;
using Vonage.Users.CreateUser;
using Xunit;

namespace Vonage.Test.Users.CreateUser;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldHaveNoChannels_GivenDefault() =>
        CreateUserRequest.Build()
            .Create()
            .Map(request => request.Channels)
            .Should()
            .BeSuccess(channels =>
            {
                channels.Messenger.Should().BeEmpty();
                channels.Pstn.Should().BeEmpty();
                channels.Mms.Should().BeEmpty();
                channels.Sip.Should().BeEmpty();
                channels.Sms.Should().BeEmpty();
                channels.Vbc.Should().BeEmpty();
                channels.Viber.Should().BeEmpty();
                channels.WhatsApp.Should().BeEmpty();
                channels.WebSocket.Should().BeEmpty();
            });

    [Fact]
    public void Build_ShouldHaveNoDisplayName_GivenDefault() =>
        CreateUserRequest.Build()
            .Create()
            .Map(request => request.DisplayName)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoImageUrl_GivenDefault() =>
        CreateUserRequest.Build()
            .Create()
            .Map(request => request.ImageUrl)
            .Should()
            .BeSuccess(Maybe<Uri>.None);

    [Fact]
    public void Build_ShouldHaveNoName_GivenDefault() =>
        CreateUserRequest.Build()
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldHaveNoUserProperties_GivenDefault() =>
        CreateUserRequest.Build()
            .Create()
            .Map(request => request.Properties)
            .Should()
            .BeSuccess(properties => properties.CustomData.Should().BeEmpty());

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Build_ShouldReturnFailure_GivenDisplayNameIsEmptyOrWhitespace(string value) =>
        CreateUserRequest.Build()
            .WithDisplayName(value)
            .Create()
            .Map(request => request.DisplayName)
            .Should()
            .BeParsingFailure("DisplayName cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Build_ShouldReturnFailure_GivenNameIsEmptyOrWhitespace(string value) =>
        CreateUserRequest.Build()
            .WithName(value)
            .Create()
            .Map(request => request.DisplayName)
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetDisplayName() =>
        CreateUserRequest.Build()
            .WithDisplayName("Test")
            .Create()
            .Map(request => request.DisplayName)
            .Should()
            .BeSuccess(Maybe<string>.Some("Test"));

    [Fact]
    public void Build_ShouldSetImageUrl() =>
        CreateUserRequest.Build()
            .WithImageUrl(new Uri("https://example.com"))
            .Create()
            .Map(request => request.ImageUrl)
            .Should()
            .BeSuccess(Maybe<Uri>.Some(new Uri("https://example.com")));

    [Fact]
    public void Build_ShouldSetName() =>
        CreateUserRequest.Build()
            .WithName("Test")
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(Maybe<string>.Some("Test"));

    [Fact]
    public void Build_ShouldSetPstnChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelPstn(123))
            .WithChannel(new ChannelPstn(456))
            .Create()
            .Map(request => request.Channels.Pstn)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelPstn(123),
                new ChannelPstn(456),
            }));

    [Fact]
    public void Build_ShouldSetSipChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelSip("sip:4442138907@sip.example.com;transport=tls", "SIP 1", "Password 1"))
            .WithChannel(new ChannelSip("sip:1234567897@sip.example.com;transport=tls", "SIP 2", "Password 2"))
            .Create()
            .Map(request => request.Channels.Sip)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelSip("sip:4442138907@sip.example.com;transport=tls", "SIP 1", "Password 1"),
                new ChannelSip("sip:1234567897@sip.example.com;transport=tls", "SIP 2", "Password 2"),
            }));

    [Fact]
    public void Build_ShouldSetVbcChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelVbc("403"))
            .WithChannel(new ChannelVbc("404"))
            .Create()
            .Map(request => request.Channels.Vbc)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelVbc("403"),
                new ChannelVbc("404"),
            }));

    [Fact]
    public void Build_ShouldSetWebSocketChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelWebSocket("wss://example.com/socket1", "audio/l16;rate=16000",
                new Dictionary<string, string> {{"customer_id", "ABC123"}}))
            .WithChannel(new ChannelWebSocket("wss://example.com/socket2", "audio/l16;rate=16000",
                new Dictionary<string, string> {{"customer_id", "ABC456"}}))
            .Create()
            .Map(request => request.Channels.WebSocket)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelWebSocket("wss://example.com/socket1", "audio/l16;rate=16000",
                    new Dictionary<string, string> {{"customer_id", "ABC123"}}),
                new ChannelWebSocket("wss://example.com/socket2", "audio/l16;rate=16000",
                    new Dictionary<string, string> {{"customer_id", "ABC456"}}),
            }));

    [Fact]
    public void Build_ShouldSetSmsChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelSms("447700900000"))
            .WithChannel(new ChannelSms("447700900001"))
            .Create()
            .Map(request => request.Channels.Sms)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelSms("447700900000"),
                new ChannelSms("447700900001"),
            }));

    [Fact]
    public void Build_ShouldSetMmsChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelMms("447700900000"))
            .WithChannel(new ChannelMms("447700900001"))
            .Create()
            .Map(request => request.Channels.Mms)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelMms("447700900000"),
                new ChannelMms("447700900001"),
            }));

    [Fact]
    public void Build_ShouldSetWhatsAppChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelWhatsApp("447700900000"))
            .WithChannel(new ChannelWhatsApp("447700900001"))
            .Create()
            .Map(request => request.Channels.WhatsApp)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelWhatsApp("447700900000"),
                new ChannelWhatsApp("447700900001"),
            }));

    [Fact]
    public void Build_ShouldSetViberChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelViber("447700900000"))
            .WithChannel(new ChannelViber("447700900001"))
            .Create()
            .Map(request => request.Channels.Viber)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelViber("447700900000"),
                new ChannelViber("447700900001"),
            }));

    [Fact]
    public void Build_ShouldSetMessengerChannel() =>
        CreateUserRequest.Build()
            .WithChannel(new ChannelMessenger("ABC123"))
            .WithChannel(new ChannelMessenger("ABD456"))
            .Create()
            .Map(request => request.Channels.Messenger)
            .Should()
            .BeSuccess(channels => channels.Should().BeEquivalentTo(new[]
            {
                new ChannelMessenger("ABC123"),
                new ChannelMessenger("ABD456"),
            }));

    [Fact]
    public void Build_ShouldSetUserProperties() =>
        CreateUserRequest.Build()
            .WithUserProperty("key1", "value")
            .WithUserProperty("key2", true)
            .Create()
            .Map(request => request.Properties)
            .Should()
            .BeSuccess(properties => properties.CustomData.Should().BeEquivalentTo(new Dictionary<string, object>
            {
                {"key1", "value"},
                {"key2", true},
            }));
}