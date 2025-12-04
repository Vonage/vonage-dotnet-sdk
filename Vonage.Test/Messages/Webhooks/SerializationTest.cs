#region
using System;
using System.IO;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Messages.Webhooks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endregion

namespace Vonage.Test.Messages.Webhooks;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private const string Directory = "Messages/Webhooks/Data/";
    private const string WhatsAppContextMessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab";
    private const string ViberContextMessageUuid = "1234567890abcdef";
    private const string StandardClientReference = "string";
    private const string StandardProviderMessage = "string";
    private const string ProfileName = "Jane Smith";
    private static readonly Guid StandardMessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098");
    private static readonly DateTimeOffset StandardTimestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z");

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerAudio(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "audio",
                Audio = new UrlDetails
                {
                    Url = "https://example.com/audio.mp3",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerFile(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "file",
                File = new UrlDetails
                {
                    Url = "https://example.com/file.pdf",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerImage(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "image",
                Image = new UrlDetails
                {
                    Url = "https://example.com/image.jpg",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerText(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "text",
                Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerUnsupported(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "unsupported",
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMessengerVideo(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "messenger",
                MessageUuid = StandardMessageUuid,
                To = "0123456789",
                From = "9876543210",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "video",
                Video = new UrlDetails
                {
                    Url = "https://example.com/video.mp4",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMmsAudio(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "mms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "audio",
                Audio = new UrlDetails
                {
                    Url = "https://example.com/audio.mp3",
                    Name = "audio.mp3",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMmsImage(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "mms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "image",
                Image = new UrlDetails
                {
                    Url = "https://example.com/image.jpg",
                    Name = "image.jpg",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMmsText(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "mms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "text",
                Text = "This is sample text.",
                Origin = new Origin("12345"),
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMmsVcard(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "mms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "vcard",
                Vcard = new UrlDetails
                {
                    Url = "https://example.com/contact.vcf",
                    Name = "contact.vcf",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeMmsVideo(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "mms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "video",
                Video = new UrlDetails
                {
                    Url = "https://example.com/video.mp4",
                    Name = "video.mp4",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeSms(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "sms",
                MessageUuid = StandardMessageUuid,
                To = "447700900000",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                Text = "Hello From Vonage!",
                Sms = new SmsDetails
                {
                    TotalCount = "2",
                    MessagesCount = "2",
                    Keyword = "HELLO",
                },
                Usage = new UsageDetails
                {
                    Currency = "EUR",
                    Price = "0.0333",
                },
                Origin = new Origin("12345"),
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeViberFile(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "viber_service",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = ViberContextMessageUuid,
                },
                To = "0123456789",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "file",
                File = new UrlDetails
                {
                    Url = "https://example.com/file.pdf",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeViberImage(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "viber_service",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = ViberContextMessageUuid,
                },
                To = "0123456789",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "image",
                Image = new UrlDetails
                {
                    Url = "https://example.com/image.jpg",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeViberText(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "viber_service",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = ViberContextMessageUuid,
                },
                To = "0123456789",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "text",
                Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeViberVideo(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "viber_service",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = ViberContextMessageUuid,
                },
                To = "0123456789",
                From = "447700900001",
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "video",
                Video = new UrlDetails
                {
                    Url = "https://example.com/video.mp4",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppAudio(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "audio",
                Audio = new UrlDetails
                {
                    Url = "https://example.com/audio.mp3",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppFile(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "file",
                File = new UrlDetails
                {
                    Url = "https://example.com/file.pdf",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppImage(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "image",
                Image = new UrlDetails
                {
                    Url = "https://example.com/image.jpg",
                    Name = "image.jpg",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppLocation(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "location",
                Location = new LocationDetails
                {
                    Latitude = 40.34772m,
                    Longitude = -74.18847m,
                    Name = "Vonage",
                    Address = "23 Main St, Holmdel, NJ 07733, USA",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppOrder(JsonSerializerType serializer)
    {
        var result = Deserialize<MessageWebhookResponse>(ReadJson(), serializer);
        result.Channel.Should().Be("whatsapp");
        result.MessageUuid.Should().Be(StandardMessageUuid);
        result.Context.Should().Be(new ContextDetails
        {
            MessageUuid = WhatsAppContextMessageUuid,
            MessageFrom = "447700900000",
            ReferredProduct = new WhatsAppReferredProduct
            {
                CatalogId = "1267260820787549",
                ProductRetailerId = "r07qei73l7",
            },
        });
        result.Profile.Should().Be(new ProfileDetails
        {
            Name = ProfileName,
        });
        result.To.Should().Be("447700900000");
        result.From.Should().Be("447700900001");
        result.ProviderMessage.Should().Be(StandardProviderMessage);
        result.ClientReference.Should().Be(StandardClientReference);
        result.MessageType.Should().Be("order");
        result.Timestamp.Should().Be(StandardTimestamp);
        result.Order.HasValue.Should().BeTrue();
        result.Order?.CatalogId.Should().Be("2806150799683508");
        result.Order?.ProductItems.Should().BeEquivalentTo(new[]
        {
            new ProductItem
            {
                ProductRetailerId = "pk1v7rudbg",
                Quantity = "1",
                ItemPrice = "9.99",
                Currency = "USD",
            },
        });
    }

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppReply(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "reply",
                Reply = new ReplyDetails
                {
                    Id = "row1",
                    Title = "9am",
                    Description = "Select 9am appointment time",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppSticker(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "sticker",
                Sticker = new UrlDetails
                {
                    Url = "https://api-us.nexmo.com/v3/media/1b456509-974c-458b-aafa-45fc48a4d976",
                },
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppText(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "text",
                Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppUnsupported(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "unsupported",
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWhatsAppVideo(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().Be(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = StandardMessageUuid,
                Context = new ContextDetails
                {
                    MessageUuid = WhatsAppContextMessageUuid,
                    MessageFrom = "447700900000",
                },
                Profile = new ProfileDetails
                {
                    Name = ProfileName,
                },
                To = "447700900000",
                From = "447700900001",
                ProviderMessage = StandardProviderMessage,
                Timestamp = StandardTimestamp,
                ClientReference = StandardClientReference,
                MessageType = "video",
                Video = new UrlDetails
                {
                    Url = "https://example.com/video.mp4",
                },
            });

    private static T Deserialize<T>(string json, JsonSerializerType serializerType) => serializerType switch
    {
        JsonSerializerType.Newtonsoft => JsonConvert.DeserializeObject<T>(json),
        JsonSerializerType.SystemTextJson => JsonSerializer.Deserialize<T>(json),
        _ => throw new ArgumentOutOfRangeException(nameof(serializerType)),
    };

    private static string ReadJson([CallerMemberName] string name = null) =>
        File.ReadAllText(string.Concat(Directory, name, "-response.json"));
}