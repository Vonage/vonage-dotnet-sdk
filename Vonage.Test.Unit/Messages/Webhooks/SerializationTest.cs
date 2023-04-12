using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Messages.Webhooks;
using Xunit;

namespace Vonage.Test.Unit.Messages.Webhooks
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserializeMessengerAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "audio",
                    audio = new AudioDetails
                    {
                        url = "https://example.com/audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "file",
                    file = new FileDetails
                    {
                        url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "image",
                    image = new ImageDetails
                    {
                        url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeMessengerText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "text",
                    text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeMessengerUnsupported() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "unsupported",
                });

        [Fact]
        public void ShouldDeserializeMessengerVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "messenger",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "0123456789",
                    from = "9876543210",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "video",
                    video = new VideoDetails
                    {
                        url = "https://example.com/video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "mms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "audio",
                    audio = new AudioDetails
                    {
                        url = "https://example.com/audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "mms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "image",
                    image = new ImageDetails
                    {
                        url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "mms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "text",
                    text = "This is sample text.",
                });

        [Fact]
        public void ShouldDeserializeMmsVcard() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "mms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "vcard",
                    vcard = new VcardDetails
                    {
                        url = "https://example.com/conatact.vcf",
                    },
                });

        [Fact]
        public void ShouldDeserializeMmsVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "mms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "video",
                    video = new VideoDetails
                    {
                        url = "https://example.com/video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeSms() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "sms",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    to = "447700900000",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    text = "Hello From Vonage!",
                    sms = new SmsDetails
                    {
                        total_count = "2",
                        num_messages = "2",
                        keyword = "HELLO",
                    },
                    usage = new UsageDetails
                    {
                        currency = "EUR",
                        price = "0.0333",
                    },
                });

        [Fact]
        public void ShouldDeserializeViberFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "viber_service",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "1234567890abcdef",
                    },
                    to = "0123456789",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "file",
                    file = new FileDetails
                    {
                        url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeViberImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "viber_service",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "1234567890abcdef",
                    },
                    to = "0123456789",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "image",
                    image = new ImageDetails
                    {
                        url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeViberText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "viber_service",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "1234567890abcdef",
                    },
                    to = "0123456789",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "text",
                    text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeViberVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "viber_service",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "1234567890abcdef",
                    },
                    to = "0123456789",
                    from = "447700900001",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "video",
                    video = new VideoDetails
                    {
                        url = "https://example.com/video.mp4",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppAudio() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "audio",
                    audio = new AudioDetails
                    {
                        url = "https://example.com/audio.mp3",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppFile() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "file",
                    file = new FileDetails
                    {
                        url = "https://example.com/file.pdf",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppImage() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "image",
                    image = new ImageDetails
                    {
                        url = "https://example.com/image.jpg",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppOrder() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.channel.Should().Be("whatsapp");
                    success.message_uuid.Should().Be(new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"));
                    success.context.Should().Be(new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                        whatsapp_referred_product = new WhatsAppPreferredProduct
                        {
                            catalog_id = "1267260820787549",
                            product_retailer_id = "r07qei73l7",
                        },
                    });
                    success.profile.Should().Be(new ProfileDetails
                    {
                        name = "Jane Smith",
                    });
                    success.to.Should().Be("447700900000");
                    success.from.Should().Be("447700900001");
                    success.provider_message.Should().Be("string");
                    success.client_ref.Should().Be("string");
                    success.message_type.Should().Be("order");
                    success.timestamp.Should().Be(DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"));
                    success.order.HasValue.Should().BeTrue();
                    success.order?.catalog_id.Should().Be("2806150799683508");
                    success.order?.product_items.Should().BeEquivalentTo(new[]
                    {
                        new ProductItem
                        {
                            product_retailer_id = "pk1v7rudbg",
                            quantity = "1",
                            item_price = "9.99",
                            currency = "USD",
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
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "reply",
                    reply = new ReplyDetails
                    {
                        id = "row1",
                        title = "9am",
                        description = "Select 9am appointment time",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppSticker() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "sticker",
                    sticker = new StickerDetails
                    {
                        url = "https://api-us.nexmo.com/v3/media/1b456509-974c-458b-aafa-45fc48a4d976",
                    },
                });

        [Fact]
        public void ShouldDeserializeWhatsAppText() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "text",
                    text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                });

        [Fact]
        public void ShouldDeserializeWhatsAppUnsupported() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "unsupported",
                });

        [Fact]
        public void ShouldDeserializeWhatsAppVideo() =>
            this.helper.Serializer
                .DeserializeObject<MessageWebhookResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(new MessageWebhookResponse
                {
                    channel = "whatsapp",
                    message_uuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                    context = new ContextDetails
                    {
                        message_uuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                        message_from = "447700900000",
                    },
                    profile = new ProfileDetails
                    {
                        name = "Jane Smith",
                    },
                    to = "447700900000",
                    from = "447700900001",
                    provider_message = "string",
                    timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                    client_ref = "string",
                    message_type = "video",
                    video = new VideoDetails
                    {
                        url = "https://example.com/video.mp4",
                    },
                });
    }
}