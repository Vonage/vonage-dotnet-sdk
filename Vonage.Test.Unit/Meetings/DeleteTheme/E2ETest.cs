using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.DeleteTheme;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteTheme
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DeleteTheme()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/themes/48a355bf-924d-4d4d-8e98-78575cf212dd")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.MeetingsClient.DeleteThemeAsync(
                    DeleteThemeRequest.Build()
                        .WithThemeId(new Guid("48a355bf-924d-4d4d-8e98-78575cf212dd"))
                        .Create())
                .Should()
                .BeSuccessAsync();
        }

        [Fact]
        public async Task DeleteThemeWithForceDelete()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v1/meetings/themes/48a355bf-924d-4d4d-8e98-78575cf212dd")
                    .WithParam("force", "true")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.MeetingsClient.DeleteThemeAsync(
                    DeleteThemeRequest.Build()
                        .WithThemeId(new Guid("48a355bf-924d-4d4d-8e98-78575cf212dd"))
                        .WithForceDelete()
                        .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}