using AutoFixture;
using Vonage.SubAccounts.CreateSubAccount;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.SubAccounts.CreateSubAccount;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly string name;
    private readonly string secret;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.name = fixture.Create<string>();
        this.secret = fixture.Create<string>();
    }

    [Fact]
    public void Build_ShouldHaveNoSecret_GivenDefault() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.name)
            .Create()
            .Map(request => request.Secret)
            .Should()
            .BeSuccess(emptySecret => emptySecret.Should().BeNone());

    [Fact]
    public void Build_ShouldNotUseAccountBalance_GivenSharedBalanceIsDisabled() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.name)
            .DisableSharedAccountBalance()
            .Create()
            .Map(request => request.UsePrimaryAccountBalance)
            .Should()
            .BeSuccess(false);

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenDisplayNameIsNullOrWhitespace(string invalidName) =>
        CreateSubAccountRequest
            .Build()
            .WithName(invalidName)
            .Create()
            .Should()
            .BeParsingFailure("Name cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenNameExceeds80Length() =>
        CreateSubAccountRequest
            .Build()
            .WithName(StringHelper.GenerateString(81))
            .Create()
            .Should()
            .BeParsingFailure("Name length cannot be higher than 80.");

    [Fact]
    public void Build_ShouldSetName() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.name)
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(this.name);

    [Fact]
    public void Build_ShouldSetSecret() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.name)
            .WithSecret(this.secret)
            .Create()
            .Map(request => request.Secret)
            .Should()
            .BeSuccess(this.secret);

    [Fact]
    public void Build_ShouldUseSharedAccountBalance_GivenDefault() =>
        CreateSubAccountRequest
            .Build()
            .WithName(this.name)
            .Create()
            .Map(request => request.UsePrimaryAccountBalance)
            .Should()
            .BeSuccess(true);
}