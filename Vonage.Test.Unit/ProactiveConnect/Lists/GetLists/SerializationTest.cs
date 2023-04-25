using System;
using System.Linq;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.GetLists;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetLists
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
                .DeserializeObject<GetListsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Page.Should().Be(1);
                    success.PageSize.Should().Be(100);
                    success.TotalItems.Should().Be(2);
                    success.TotalPages.Should().Be(1);
                    success.Links.Self.Href.Should()
                        .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/lists?page_size=100&page=1"));
                    success.Links.Previous.Href.Should()
                        .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/lists?page_size=100&page=1"));
                    success.Links.Next.Href.Should()
                        .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/lists?page_size=100&page=1"));
                    success.Links.First.Href.Should()
                        .Be(new Uri("https://api-eu.vonage.com/v0.1/bulk/lists?page_size=100&page=1"));
                    success.EmbeddedLists.Lists.Should().HaveCount(2);
                    var firstList = success.EmbeddedLists.Lists.ToList()[0];
                    var secondList = success.EmbeddedLists.Lists.ToList()[1];
                    firstList.Name.Should().Be("Recipients for demo");
                    firstList.Description.Should().Be("List of recipients for demo");
                    firstList.Tags.Should().BeEquivalentTo("vip");
                    firstList.Attributes.Should().BeEquivalentTo(new[]
                    {
                        new ListAttribute
                        {
                            Name = "firstName",
                        },
                        new ListAttribute
                        {
                            Name = "lastName",
                        },
                        new ListAttribute
                        {
                            Name = "number",
                            Alias = "Phone",
                            Key = true,
                        },
                    });
                    firstList.DataSource.Type.Should().Be(ListDataSourceType.Manual);
                    firstList.ItemsCount.Should().Be(1000);
                    firstList.SyncStatus.Should().Be(new SyncStatus
                    {
                        Dirty = false,
                        Value = SyncStatus.Status.Configured,
                        DataModified = false,
                        MetadataModified = false,
                    });
                    firstList.Id = new Guid("af8a84b6-c712-4252-ac8d-6e28ac9317ce");
                    firstList.CreatedAt = DateTimeOffset.Parse("2022-06-23T13:13:16.491Z");
                    firstList.UpdatedAt = DateTimeOffset.Parse("2022-06-23T13:13:16.491Z");
                    secondList.Name.Should().Be("Salesforce contacts");
                    secondList.Description.Should().Be("Salesforce contacts for campaign");
                    secondList.Tags.Should().BeEquivalentTo("salesforce");
                    secondList.Attributes.Should().BeEquivalentTo(new[]
                    {
                        new ListAttribute {Name = "Id"},
                        new ListAttribute {Name = "Phone", Key = true},
                        new ListAttribute {Name = "Email"},
                    });
                    secondList.DataSource.Should().Be(new ListDataSource
                    {
                        Type = ListDataSourceType.Salesforce,
                        Soql = "Some sql statement",
                        IntegrationId = "salesforce",
                    });
                });
    }
}