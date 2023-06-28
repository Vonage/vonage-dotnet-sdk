﻿using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.Test.Unit.TestHelpers;
using Vonage.VerifyV2.Cancel;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.Cancel
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;

        public E2ETest() => this.helper = new E2EHelper(
            "Vonage.Url.Api",
            Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(),
                Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey")));

        [Fact]
        public async Task CancelVerificationRequest()
        {
            var requestId = Guid.NewGuid();
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.helper.Server.Url}/v2/verify/{requestId}")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result = await this.helper.VonageClient.VerifyV2Client.CancelAsync(CancelRequest.Parse(requestId));
            result.Should().BeSuccess();
        }
    }
}