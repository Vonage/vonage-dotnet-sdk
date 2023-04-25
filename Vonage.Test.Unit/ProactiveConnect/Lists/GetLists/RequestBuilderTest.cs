using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.GetLists;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetLists
{
    public class RequestBuilderTest
    {
        private readonly int page;
        private readonly int pageSize;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.page = fixture.Create<int>();
            this.pageSize = fixture.Create<int>();
        }

        [Fact]
        public void Build_ShouldReturnSuccess() =>
            GetListsRequest
                .Build()
                .WithPage(this.page)
                .WithPageSize(this.pageSize)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.Page.Should().Be(this.page);
                    success.PageSize.Should().Be(this.pageSize);
                });
    }
}