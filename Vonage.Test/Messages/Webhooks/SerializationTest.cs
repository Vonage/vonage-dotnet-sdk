using System;
using FluentAssertions;
using Vonage.Messages.Webhooks;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Messages.Webhooks
{
    [Trait("Category", "Serialization")]
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserializeMessengerAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "audio",
                    Audio = new UrlDetails
                    {
                        Url = "https://example.com/audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "file",
                    File = new UrlDetails
                    {
                        Url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "image",
                    Image = new UrlDetails
                    {
                        Url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "text",
                    Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeMessengerUnsupported() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "unsupported",
                });

        [Fact]
        public void ShouldDeserializeMessengerVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "messenger",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "0123456789",
                    From = "9876543210",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "video",
                    Video = new UrlDetails
                    {
                        Url = "https://example.com/video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "mms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "audio",
                    Audio = new UrlDetails
                    {
                        Url = "https://example.com/audio.mp3",
                        Name = "audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "mms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "image",
                    Image = new UrlDetails
                    {
                        Url = "https://example.com/image.jpg",
                        Name = "image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "mms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "text",
                    Text = "This is sample text.",
                    Origin = new Origin("12345"),
                });

        [Fact]
        public void ShouldDeserializeMmsVcard() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "mms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "vcard",
                    Vcard = new UrlDetails
                    {
                        Url = "https://example.com/conatact.vcf",
                        Name = "contact.vcf",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "mms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "video",
                    Video = new UrlDetails
                    {
                        Url = "https://example.com/video.mp4",
                        Name = "video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeSms() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "sms",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    To = "447700900000",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
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

        [Fact]
        public void ShouldDeserializeViberFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "viber_service",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "1234567890abcdef",
                    },
                    To = "0123456789",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "file",
                    File = new UrlDetails
                    {
                        Url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeViberImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "viber_service",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "1234567890abcdef",
                    },
                    To = "0123456789",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "image",
                    Image = new UrlDetails
                    {
                        Url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeViberText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "viber_service",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "1234567890abcdef",
                    },
                    To = "0123456789",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "text",
                    Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeViberVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "viber_service",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "1234567890abcdef",
                    },
                    To = "0123456789",
                    From = "447700900001",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "video",
                    Video = new UrlDetails
                    {
                        Url = "https://example.com/video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "audio",
                    Audio = new UrlDetails
                    {
                        Url = "https://example.com/audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "file",
                    File = new UrlDetails
                    {
                        Url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "image",
                    Image = new UrlDetails
                    {
                        Url = "https://example.com/image.jpg",
                        Name = "image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppLocation() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "location",
                    Location = new LocationDetails
                    {
                        Latitude = 40.34772m,
                        Longitude = -74.18847m,
                        Name = "Vonage",
                        Address = "23 Main St, Holmdel, NJ 07733, USA",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppOrder() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Channel.Should().Be("whatsapp");
                    success.MessageUuid.Should().Be(new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"));
                    success.Context.Should().Be(new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                        ReferredProduct = new WhatsAppReferredProduct
                        {
                            CatalogId = "1267260820787549",
                            ProductRetailerId = "r07qei73l7",
                        },
                    });
                    success.Profile.Should().Be(new ProfileDetails
                    {
                        Name = "Jane Smith",
                    });
                    success.To.Should().Be("447700900000");
                    success.From.Should().Be("447700900001");
                    success.ProviderMessage.Should().Be("string");
                    success.ClientReference.Should().Be("string");
                    success.MessageType.Should().Be("order");
                    success.Timestamp.Should().Be(DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"));
                    success.Order.HasValue.Should().BeTrue();
                    success.Order?.CatalogId.Should().Be("2806150799683508");
                    success.Order?.ProductItems.Should().BeEquivalentTo(new[]
                    {
                        new ProductItem
                        {
                            ProductRetailerId = "pk1v7rudbg",
                            Quantity = "1",
                            ItemPrice = "9.99",
                            Currency = "USD",
                        },
                    });
                });

        [Fact]
        public void ShouldDeserializeWhatsAppReply() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "reply",
                    Reply = new ReplyDetails
                    {
                        Id = "row1",
                        Title = "9am",
                        Description = "Select 9am appointment time",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppSticker() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "sticker",
                    Sticker = new UrlDetails
                    {
                        Url = "https://api-us.nexmo.com/v3/media/1b456509-974c-458b-aafa-45fc48a4d976",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "text",
                    Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeWhatsAppUnsupported() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "unsupported",
                });

        [Fact]
        public void ShouldDeserializeWhatsAppVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    Channel = "whatsapp",
                    MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    Context = new ContextDetails
                    {
                        MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        MessageFrom = "447700900000",
                    },
                    Profile = new ProfileDetails
                    {
                        Name = "Jane Smith",
                    },
                    To = "447700900000",
                    From = "447700900001",
                    ProviderMessage = "string",
                    Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    ClientReference = "string",
                    MessageType = "video",
                    Video = new UrlDetails
                    {
                        Url = "https://example.com/video.mp4",
                    },
                });
    }
}