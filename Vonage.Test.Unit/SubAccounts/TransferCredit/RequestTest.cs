﻿using AutoFixture;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.TransferCredit;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferCredit
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        public RequestTest() => this.fixture = new Fixture();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.fixture.Create<string>())
                .WithTo(this.fixture.Create<string>())
                .WithAmount(this.fixture.Create<decimal>())
                .Create()
                .Map(request => request.WithApiKey("489dsSS564652"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts/489dsSS564652/credit-transfers/");

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.fixture.Create<string>())
                .WithTo(this.fixture.Create<string>())
                .WithAmount(this.fixture.Create<decimal>())
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/accounts//credit-transfers/");
    }
}