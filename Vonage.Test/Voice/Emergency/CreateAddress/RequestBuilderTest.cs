#region
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Vonage.Test.Common.TestHelpers;
using Vonage.Voice.Emergency;
using Vonage.Voice.Emergency.CreateAddress;
using Xunit;
#endregion

namespace Vonage.Test.Voice.Emergency.CreateAddress;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string NonEmptyString = "TEST";

    [Fact]
    public void Build_ShouldSetNoneName_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldReturnFailure_GivenNameLengthIsLowerThanMinimum() =>
        CreateAddressRequest.Build()
            .WithName(StringHelper.GenerateString(1))
            .Create()
            .Should()
            .BeParsingFailure("Name length cannot be lower than 2.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenNameLengthIsHigherThanMaximum() =>
        CreateAddressRequest.Build()
            .WithName(StringHelper.GenerateString(33))
            .Create()
            .Should()
            .BeParsingFailure("Name length cannot be higher than 32.");

    [Theory]
    [InlineData("F")]
    [InlineData("FRR")]
    public void Build_ShouldReturnFailure_GivenCountryLengthIsDifferentThanTwo(string invalidCountry) =>
        CreateAddressRequest.Build()
            .WithCountry(invalidCountry)
            .Create()
            .Should()
            .BeParsingFailure("Country length should be 2.");

    [Fact]
    public void Build_ShouldSetNoneFirstAddressLine_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.FirstAddressLine)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetNoneSecondAddressLine_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.SecondAddressLine)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetNoneCity_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.City)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetNoneRegion_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.Region)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetType_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.Type)
            .Should()
            .BeSuccess("emergency");

    [Fact]
    public void Build_ShouldSetNoneLocation_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.Location)
            .Should()
            .BeSuccess(Maybe<Address.AddressLocationType>.None);

    [Fact]
    public void Build_ShouldSetNonePostalCode_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.PostalCode)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Fact]
    public void Build_ShouldSetNoneCountry_GivenDefault() =>
        SerializationTest.GetEmptyRequest()
            .Map(request => request.Country)
            .Should()
            .BeSuccess(Maybe<string>.None);

    [Theory]
    [InlineData("AA")]
    [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
    public void Build_ShouldSetName(string validName) =>
        CreateAddressRequest.Build()
            .WithName(validName)
            .Create()
            .Map(request => request.Name)
            .Should()
            .BeSuccess(validName);

    [Fact]
    public void Build_ShouldSetFirstAddressLine() =>
        CreateAddressRequest.Build()
            .WithFirstAddressLine(NonEmptyString)
            .Create()
            .Map(request => request.FirstAddressLine)
            .Should()
            .BeSuccess(NonEmptyString);

    [Fact]
    public void Build_ShouldSetSecondAddressLine() =>
        CreateAddressRequest.Build()
            .WithSecondAddressLine(NonEmptyString)
            .Create()
            .Map(request => request.SecondAddressLine)
            .Should()
            .BeSuccess(NonEmptyString);

    [Fact]
    public void Build_ShouldSetCity() =>
        CreateAddressRequest.Build()
            .WithCity(NonEmptyString)
            .Create()
            .Map(request => request.City)
            .Should()
            .BeSuccess(NonEmptyString);

    [Fact]
    public void Build_ShouldSetRegion() =>
        CreateAddressRequest.Build()
            .WithRegion(NonEmptyString)
            .Create()
            .Map(request => request.Region)
            .Should()
            .BeSuccess(NonEmptyString);

    [Fact]
    public void Build_ShouldSetLocation() =>
        CreateAddressRequest.Build()
            .WithLocation(Address.AddressLocationType.Residential)
            .Create()
            .Map(request => request.Location)
            .Should()
            .BeSuccess(Address.AddressLocationType.Residential);

    [Fact]
    public void Build_ShouldSetPostalCode() =>
        CreateAddressRequest.Build()
            .WithPostalCode(NonEmptyString)
            .Create()
            .Map(request => request.PostalCode)
            .Should()
            .BeSuccess(NonEmptyString);

    [Fact]
    public void Build_ShouldSetCountry() =>
        CreateAddressRequest.Build()
            .WithCountry("US")
            .Create()
            .Map(request => request.Country)
            .Should()
            .BeSuccess("US");
}