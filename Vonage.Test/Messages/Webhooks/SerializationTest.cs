#region
using System;
using System.IO;
using System.Runtime.CompilerServices;
using FluentAssertions;
using Newtonsoft.Json;
using Vonage.Common;
using Vonage.Messages.Webhooks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;
#endregion

namespace Vonage.Test.Messages.Webhooks;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private const string Directory = "Messages/Webhooks/Data/";

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeWebhook(JsonSerializerType serializer) =>
        Deserialize<MessageWebhookResponse>(ReadJson(), serializer)
            .Should().BeEquivalentTo(new MessageWebhookResponse
            {
                Channel = "whatsapp",
                MessageUuid = new Guid("52afe398-e31a-4362-8c42-beb0c3b1d098"),
                To = "447700900000",
                From = "447700900001",
                Timestamp = DateTimeOffset.Parse("2020-01-01T14:00:00.000Z"),
                Profile = new ProfileDetails
                {
                    Name = "Jane Smith",
                },
                ContextStatus = "available",
                Context = new ContextDetails
                {
                    MessageUuid = "aaaaaaaa-bbbb-cccc-dddd-0123456789ab",
                    MessageFrom = "447700900000",
                },
                ProviderMessage = "string",
                ClientReference = "string",
                MessageType = "text",
                Text = "Nexmo Verification code: 12345.<br />Valid for 10 minutes.",
                Audio = new UrlDetails
                {
                    Url = "https://example.com/audio.mp3",
                    Name = "audio.mp3",
                },
                Video = new UrlDetails
                {
                    Url = "https://example.com/video.mp4",
                    Name = "video.mp4",
                },
                Image = new UrlDetails
                {
                    Url = "https://example.com/image.jpg",
                    Name = "image.jpg",
                    Caption = "Additional text to accompany the image.",
                },
                File = new UrlDetails
                {
                    Url = "https://example.com/file.pdf",
                    Name = "file.pdf",
                    Caption = "Additional text to accompany the file.",
                },
                Sticker = new UrlDetails
                {
                    Url = "https://api-us.nexmo.com/v3/media/1b456509-974c-458b-aafa-45fc48a4d976",
                },
                Vcard = new UrlDetails
                {
                    Url = "https://example.com/contact.vcf",
                    Name = "contact.vcf",
                },
                Location = new LocationDetails
                {
                    Latitude = 40.34772m,
                    Longitude = -74.18847m,
                    Name = "Vonage",
                    Address = "23 Main St, Holmdel, NJ 07733, USA",
                },
                Order = new OrderDetails
                {
                    CatalogId = "2806150799683508",
                    ProductItems =
                    [
                        new ProductItem
                        {
                            ProductRetailerId = "pk1v7rudbg",
                            Quantity = "1",
                            ItemPrice = "9.99",
                            Currency = "USD",
                        },
                    ],
                },
                Reply = new ReplyDetails
                {
                    Id = "row1",
                    Title = "9am",
                    Description = "Select 9am appointment time",
                },
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
                Content = [new ContentDetails("image", "https://example.com/image.jpg")],
                WhatsApp = new WhatsAppDetails
                {
                    Referral = new WhatsAppReferral(
                        "Check out our new product offering",
                        "New Products!",
                        "212731241638144",
                        "post",
                        "https://fb.me/2ZulEu42P",
                        "image",
                        "https://example.com/image.jpg",
                        "https://example.com/video.mp4",
                        "https://example.com/thumbnail.jpg",
                        "1234567890"),
                    Referredproduct = new WhatsAppReferredProduct
                    {
                        CatalogId = "1267260820787549",
                        ProductRetailerId = "r07qei73l7",
                    },
                },
                Self =
                    new HalLink(new Uri("https://api-eu.vonage.com/v1/messages/aaaaaaa-bbbb-4ccc-8ddd-0123456789ab")),
            });

    [Theory]
    [InlineData(JsonSerializerType.Newtonsoft)]
    [InlineData(JsonSerializerType.SystemTextJson)]
    public void ShouldDeserializeStatus(JsonSerializerType serializer) =>
        Deserialize<MessageStatusResponse>(ReadJson(), serializer)
            .Should().BeEquivalentTo(new MessageStatusResponse
            {
                MessageId = "aaaaaaaa-bbbb-4ccc-8ddd-0123456789ab",
                Channel = "sms",
                ClientReference = "abc123",
                From = "447700900001",
                Status = "submitted",
                Timestamp = DateTimeOffset.Parse("2025-02-03T12:14:25Z"),
                To = "447700900000",
                Error = new StatusError("https://developer.vonage.com/api-errors/messages#1000", "1000",
                    "Throttled - You have exceeded the submission capacity allowed on this account. Please wait and retry",
                    "bf0ca0bf927b3b52e3cb03217e1a1ddf"),
                Workflow = new StatusWorkflow("3TcNjguHxr2vcCZ9Ddsnq6tw8yQUpZ9rMHv9QXSxLan5ibMxqSzLdx9", "1", "2"),
                Usage = new StatusUsage("EUR", "0.0333"),
                Destination = new StatusDestination("12345"),
                Sms = new StatusSms("2"),
                WhatsApp = new StatusWhatsApp(
                    new StatusWhatsAppPricing("regular", "CBP", "service"),
                    new StatusWhatsAppConversation("1234567890",
                        new StatusWhatsAppConversationOrigin("user_initiated"))),
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