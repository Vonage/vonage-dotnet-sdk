using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.SubAccounts.TransferNumber;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferNumber
{
    public class RequestBuilderTest
    {
        private readonly string from;
        private readonly string to;
        private readonly string country;
        private readonly string number;

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
                .BeFailure(ResultFailure.FromErrorMessage("Country cannot be null or whitespace."));

        [Property]
        public Property Build_ShouldReturnFailure_GivenCountryLengthIsDifferentThanTwo() =>
            Prop.ForAll(
                Arb.From<string>().MapFilter(_ => _, value => !string.IsNullOrWhiteSpace(value) && value.Length != 2),
                invalidCountry => TransferNumberRequest
                    .Build()
                    .WithFrom(this.from)
                    .WithTo(this.to)
                    .WithNumber(this.number)
                    .WithCountry(invalidCountry)
                    .Create()
                    .Should()
                    .BeFailure(ResultFailure.FromErrorMessage("Country length should be 2.")));

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
                .BeFailure(ResultFailure.FromErrorMessage("From cannot be null or whitespace."));

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
                .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));

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
                .BeFailure(ResultFailure.FromErrorMessage("To cannot be null or whitespace."));

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
}