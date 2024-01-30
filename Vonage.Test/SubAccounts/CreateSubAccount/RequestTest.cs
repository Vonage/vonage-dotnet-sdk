﻿using AutoFixture;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.CreateSubAccount;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Fixture fixture;
    public RequestTest() => this.fixture = new Fixture();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.fixture.Create<string>())
            .Create()
            .Map(request => request.WithApiKey("489dsSS564652"))
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/accounts/489dsSS564652/subaccounts");

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint_WithoutPrimaryAccountKeyKey() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.fixture.Create<string>())
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("/accounts//subaccounts");
}