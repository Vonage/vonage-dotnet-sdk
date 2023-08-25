using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.UpdateTheme;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateTheme
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task UpdateTheme()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/themes/cf7f7327-c8f3-4575-b113-0598571b499a")
                    .WithHeader("Authorization", "Bearer *")
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPatch())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.MeetingsClient.UpdateThemeAsync(UpdateThemeRequest
                    .Build()
                    .WithThemeId(new Guid("cf7f7327-c8f3-4575-b113-0598571b499a"))
                    .WithColor(Color.FromArgb(255, 255, 0, 255))
                    .WithName("Theme1")
                    .WithBrandText("Brand")
                    .WithShortCompanyUrl(new Uri("https://example.com"))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyTheme);
        }
    }
}