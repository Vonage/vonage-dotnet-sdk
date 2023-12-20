using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.SubAccounts.TransferAmount;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.SubAccounts.TransferAmount
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
            TransferAmountRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Map(request => request.Reference)
                .Should()
                .BeSuccess(success => success.Should().BeNone());

        [Property]
        public Property Build_ShouldReturnFailure_GivenAmountIsNegative() =>
            Prop.ForAll(
                FsCheckExtensions.GetNegativeNumbers(),
                negativeAmount => TransferAmountRequest
                    .Build()
                    .WithFrom(this.from)
                    .WithTo(this.to)
                    .WithAmount(negativeAmount)
                    .Create()
                    .Should()
                    .BeParsingFailure("Amount cannot be negative."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenFromIsNullOrWhitespace(string invalidValue) =>
            TransferAmountRequest
                .Build()
                .WithFrom(invalidValue)
                .WithTo(this.to)
                .WithAmount(this.amount)
                .Create()
                .Should()
                .BeParsingFailure("From cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenToIsNullOrWhitespace(string invalidValue) =>
            TransferAmountRequest
                .Build()
                .WithFrom(this.from)
                .WithTo(invalidValue)
                .WithAmount(this.amount)
                .Create()
                .Should()
                .BeParsingFailure("To cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldSetAmount() =>
            TransferAmountRequest
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
            TransferAmountRequest
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
            TransferAmountRequest
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
            TransferAmountRequest
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