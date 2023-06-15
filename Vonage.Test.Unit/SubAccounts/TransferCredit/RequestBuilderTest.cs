using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.SubAccounts.TransferCredit;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.TransferCredit
{
    public class RequestBuilderTest
    {
        private readonly decimal amount;
        private readonly string from;
        private readonly string to;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.from = fixture.Create<string>();
            this.to = fixture.Create<string>();
            this.amount = fixture.Create<decimal>();
        }

        [Fact]
        public void Build_ShouldHaveNoReference_GivenDefault() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Map(request => request.Reference)
                .Should()
                .BeSuccess(success => success.Should().BeNone());

        [Property]
        public Property Build_ShouldReturnFailure_GivenAmountIsNegative2() =>
            Prop.ForAll(
                FsCheckExtensions.GetNegativeNumbers(),
                negativeAmount => TransferCreditRequest
                    .Build()
                    .WithFrom(this.from)
                    .WithTo(this.to)
                    .WithAmount(negativeAmount)
                    .Create()
                    .Should()
                    .BeFailure(ResultFailure.FromErrorMessage("Amount cannot be negative.")));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenFromIsNullOrWhitespace(string invalidValue) =>
            TransferCreditRequest
                .Build()
                .WithFrom(invalidValue)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("From cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenToIsNullOrWhitespace(string invalidValue) =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(invalidValue)
                .WithAmount(this.amount)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("To cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldSetAmount() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Map(request => request.Amount)
                .Should()
                .BeSuccess(this.amount);

        [Fact]
        public void Build_ShouldSetFrom() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Map(request => request.From)
                .Should()
                .BeSuccess(this.from);

        [Fact]
        public void Build_ShouldSetReference() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .WithReference("Ref")
                .Create()
                .Map(request => request.Reference)
                .Should()
                .BeSuccess("Ref");

        [Fact]
        public void Build_ShouldSetTo() =>
            TransferCreditRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Map(request => request.To)
                .Should()
                .BeSuccess(this.to);
    }
}