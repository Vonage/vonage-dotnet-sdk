using System;
using AutoFixture;
using Vonage.SubAccounts.GetTransfers;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.SubAccounts.GetTransfers
{
    public class RequestBuilderTest
    {
        private readonly DateTimeOffset startDate;
        private readonly DateTimeOffset endDate;
        private readonly string subAccountKey;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.startDate = fixture.Create<DateTimeOffset>();
            this.endDate = fixture.Create<DateTimeOffset>();
            this.subAccountKey = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldHaveNoSubAccountKey_GivenDefault() =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .Create()
                .Map(request => request.SubAccountKey)
                .Should()
                .BeSuccess(success => success.Should().BeNone());

        [Fact]
        public void Build_ShouldHavNoEndDate_GivenDefault() =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .Create()
                .Map(request => request.EndDate)
                .Should()
                .BeSuccess(success => success.Should().BeNone());

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_ShouldReturnFailure_GivenSubAccountKeyIsEmptyOrWhitespace(string invalidKey) =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .WithSubAccountKey(invalidKey)
                .Create()
                .Should()
                .BeParsingFailure("SubAccountKey cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldSetEndDate() =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .WithEndDate(this.endDate)
                .Create()
                .Map(request => request.EndDate)
                .Should()
                .BeSuccess(this.endDate);

        [Fact]
        public void Build_ShouldSetStartDate() =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .Create()
                .Map(request => request.StartDate)
                .Should()
                .BeSuccess(this.startDate);

        [Fact]
        public void Build_ShouldSetSubAccountKey() =>
            GetTransfersRequest
                .Build()
                .WithStartDate(this.startDate)
                .WithSubAccountKey(this.subAccountKey)
                .Create()
                .Map(request => request.SubAccountKey)
                .Should()
                .BeSuccess(this.subAccountKey);
    }
}