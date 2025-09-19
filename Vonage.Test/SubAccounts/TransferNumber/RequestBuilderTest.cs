using AutoFixture;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using Vonage.SubAccounts.TransferNumber;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Xunit;

namespace Vonage.Test.SubAccounts.TransferNumber;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly string country;
    private readonly string from;
    private readonly string number;
    private readonly string to;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.from = fixture.Create<string>();
        this.to = fixture.Create<string>();
        this.number = fixture.Create<string>();
        this.country = StringHelper.GenerateString(2);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenCountryIsNullOrWhitespace(string invalidValue) =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(invalidValue)
            .Create()
            .Should()
            .BeParsingFailure("Country cannot be null or whitespace.");

    [Property]
    public Property Build_ShouldReturnFailure_GivenCountryLengthIsDifferentThanTwo() =>
        Prop.ForAll(
            ArbMap.Default.GeneratorFor<string>().Where(value => !string.IsNullOrWhiteSpace(value) && value.Length != 2)
                .ToArbitrary(),
            invalidCountry => TransferNumberRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithNumber(this.number)
                .WithCountry(invalidCountry)
                .Create()
                .Should()
                .BeParsingFailure("Country length should be 2."));

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenFromIsNullOrWhitespace(string invalidValue) =>
        TransferNumberRequest
            .Build()
            .WithFrom(invalidValue)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Should()
            .BeParsingFailure("From cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string invalidValue) =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(invalidValue)
            .WithCountry(this.country)
            .Create()
            .Should()
            .BeParsingFailure("Number cannot be null or whitespace.");

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenToIsNullOrWhitespace(string invalidValue) =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(invalidValue)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Should()
            .BeParsingFailure("To cannot be null or whitespace.");

    [Fact]
    public void Build_ShouldSetCountry() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Map(request => request.Country)
            .Should()
            .BeSuccess(this.country);

    [Fact]
    public void Build_ShouldSetFrom() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Map(request => request.From)
            .Should()
            .BeSuccess(this.from);

    [Fact]
    public void Build_ShouldSetNumber() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Map(request => request.Number)
            .Should()
            .BeSuccess(this.number);

    [Fact]
    public void Build_ShouldSetTo() =>
        TransferNumberRequest
            .Build()
            .WithFrom(this.from)
            .WithTo(this.to)
            .WithNumber(this.number)
            .WithCountry(this.country)
            .Create()
            .Map(request => request.To)
            .Should()
            .BeSuccess(this.to);
}