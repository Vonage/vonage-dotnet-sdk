using AutoFixture;
using Vonage.SubAccounts.UpdateSubAccount;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.UpdateSubAccount;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly string name;
    private readonly string subAccountKey;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.subAccountKey = fixture.Create<string>();
        this.name = fixture.Create<string>();
    }

    [Fact]
    public void Build_ShouldDisableSharedAccountBalance() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .DisableSharedAccountBalance()
            .Create()
            .Map(request => request.UsePrimaryAccountBalance)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldEnableAccount() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .EnableAccount()
            .Create()
            .Map(request => request.Suspended)
            .Should()
            .BeSuccess(false);

    [Fact]
    public void Build_ShouldEnableSharedAccountBalance() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .EnableSharedAccountBalance()
            .Create()
            .Map(request => request.UsePrimaryAccountBalance)
            .Should()
            .BeSuccess(true);

    [Fact]
    public void Build_ShouldNotChangeName_GivenDefault() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .SuspendAccount()
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Fact]
    public void Build_ShouldNotChangeShareAccountBalance_GivenDefault() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .WithName(this.name)
            .Create()
            .Map(request => request.UsePrimaryAccountBalance)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Fact]
    public void Build_ShouldNotChangeStatus_GivenDefault() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .WithName(this.name)
            .Create()
            .Map(request => request.Suspended)
            .Should()
            .BeSuccess(success => success.Should().BeNone());

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenDisplayNameIsNullOrWhitespace(string invalidKey) =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(invalidKey)
            .SuspendAccount()
            .Create()
            .Should()
            .BeParsingFailure("SubAccountKey cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenNoPropertyWasChanged() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .Create()
            .Should()
            .BeParsingFailure("No property was modified.");

    [Fact]
    public void Build_ShouldSetName() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .WithName(this.name)
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(this.name);

    [Fact]
    public void Build_ShouldSetSubAccountKey() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .WithName(this.name)
            .Create()
            .Map(request => request.SubAccountKey)
            .Should()
            .BeSuccess(this.subAccountKey);

    [Fact]
    public void Build_ShouldSuspendAccount() =>
        UpdateSubAccountRequest
            .Build()
            .WithSubAccountKey(this.subAccountKey)
            .SuspendAccount()
            .Create()
            .Map(request => request.Suspended)
            .Should()
            .BeSuccess(true);
}