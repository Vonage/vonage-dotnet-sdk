using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.CreateTheme;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.CreateTheme
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task CreateTheme()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/meetings/themes")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.Helper.VonageClient.MeetingsClient.CreateThemeAsync(CreateThemeRequest
                .Build()
                .WithBrand("Brand")
                .WithColor(Color.FromArgb(255, 255, 0, 255))
                .WithName("Theme1")
                .WithShortCompanyUrl(new Uri("https://example.com"))
                .Create());
            result.Should().BeSuccess();
        }

        [Fact]
        public async Task CreateThemeWithDefaultValues()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/meetings/themes")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                        .ShouldSerializeWithDefaultValues)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result = await this.Helper.VonageClient.MeetingsClient.CreateThemeAsync(CreateThemeRequest
                .Build()
                .WithBrand("Brand")
                .WithColor(Color.FromArgb(255, 255, 0, 255))
                .Create());
            result.Should().BeSuccess();
        }
    }
}