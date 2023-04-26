using System;
using System.Linq;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items.GetItems;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItems
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializer.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetItemsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Page.Should().Be(1);
                    success.PageSize.Should().Be(100);
                    success.TotalItems.Should().Be(2);
                    success.TotalPages.Should().Be(1);
                    success.Links.Self.Href.Should()
                        .Be(new Uri(
                            "https://api-eu.vonage.com/v0.1/bulk/lists/060b9c33-6c81-4fe4-9621-b10c0a4c06f3/items/?page_size=100&page=1"));
                    success.Links.Previous.Href.Should()
                        .Be(new Uri(
                            "https://api-eu.vonage.com/v0.1/bulk/lists/060b9c33-6c81-4fe4-9621-b10c0a4c06f3/items/?page_size=100&page=1"));
                    success.Links.Next.Href.Should()
                        .Be(new Uri(
                            "https://api-eu.vonage.com/v0.1/bulk/lists/060b9c33-6c81-4fe4-9621-b10c0a4c06f3/items/?page_size=100&page=1"));
                    success.Links.First.Href.Should()
                        .Be(new Uri(
                            "https://api-eu.vonage.com/v0.1/bulk/lists/060b9c33-6c81-4fe4-9621-b10c0a4c06f3/items/?page_size=100&page=1"));
                    success.EmbeddedItems.Items.Should().HaveCount(2);
                    var firstItem = success.EmbeddedItems.Items.ToList()[0];
                    var secondItem = success.EmbeddedItems.Items.ToList()[1];
                    firstItem.Id.Should().Be(new Guid("6e26d247-e074-4f68-b72b-dd92aa02c7e0"));
                    firstItem.CreatedAt.Should().Be(DateTimeOffset.Parse("2022-08-03T08:54:21.122Z"));
                    firstItem.UpdatedAt.Should().Be(DateTimeOffset.Parse("2022-08-03T08:54:21.122Z"));
                    firstItem.ListId.Should().Be(new Guid("060b9c33-6c81-4fe4-9621-b10c0a4c06f3"));
                    firstItem.Data["Id"].ToString().Should().Be("0038d00000B22zbAAB");
                    firstItem.Data["Email"].ToString().Should().Be("info@salesforce.com");
                    firstItem.Data["Phone"].ToString().Should().Be("(415) 555-1212");
                    firstItem.Data["LastName"].ToString().Should().Be("Minor");
                    firstItem.Data["FirstName"].ToString().Should().Be("Geoff");
                    firstItem.Data["OtherCountry"].ToString().Should().Be("Canada");
                    secondItem.Id.Should().Be(new Guid("f7c029ad-93c3-469c-9267-73c3c6864161"));
                    secondItem.CreatedAt.Should().Be(DateTimeOffset.Parse("2022-08-03T08:54:21.122Z"));
                    secondItem.UpdatedAt.Should().Be(DateTimeOffset.Parse("2022-08-03T08:54:21.122Z"));
                    secondItem.ListId.Should().Be(new Guid("060b9c33-6c81-4fe4-9621-b10c0a4c06f3"));
                    secondItem.Data["Id"].ToString().Should().Be("0038d00000B22zcAAB");
                    secondItem.Data["Email"].ToString().Should().Be("info@salesforce.com");
                    secondItem.Data["Phone"].ToString().Should().Be("(415) 555-1212");
                    secondItem.Data["LastName"].ToString().Should().Be("White");
                    secondItem.Data["FirstName"].ToString().Should().Be("Carole");
                    secondItem.Data["OtherCountry"].Should().BeNull();
                });
    }
}