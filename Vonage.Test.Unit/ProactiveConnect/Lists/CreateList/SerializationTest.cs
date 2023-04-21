using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.CreateList;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.CreateList
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
                .DeserializeObject<CreateListResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Name.Should().Be("my name");
                    success.Description.Should().Be("my description");
                    success.Tags.Should().BeEquivalentTo("vip", "sport");
                    success.Attributes.Should().BeEquivalentTo(new[]
                    {
                        new ListAttribute
                        {
                            Name = "phone_number",
                            Alias = "phone",
                        },
                    });
                    success.DataSource.Should().Be(new ListDataSource
                    {
                        Type = ListDataSourceType.Salesforce,
                        IntegrationId = "salesforce_credentials",
                        Soql = "some sql",
                    });
                    success.Id.Should().Be(new Guid("29192c4a-4058-49da-86c2-3e349d1065b7"));
                    success.CreatedAt.Should().Be(DateTimeOffset.Parse("2022-06-19T17:59:28.085Z"));
                    success.UpdatedAt.Should().Be(DateTimeOffset.Parse("2022-06-19T17:59:28.085Z"));
                    success.SyncStatus.Should().Be(new SyncStatus
                    {
                        Value = SyncStatus.Status.Configured,
                        Details = "Not found",
                        DataModified = true,
                        Dirty = true,
                    });
                    success.ItemsCount.Should().Be(500);
                });

        [Fact]
        public void ShouldSerializeWithMandatoryValues() =>
            CreateListRequest
                .Build()
                .WithName("my name")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithManualDataSource() =>
            CreateListRequest
                .Build()
                .WithName("my name")
                .WithDescription("my description")
                .WithTag("vip")
                .WithTag("sport")
                .WithAttribute(new ListAttribute
                {
                    Name = "phone_number",
                    Alias = "phone",
                })
                .WithDataSource(new ListDataSource
                {
                    Type = ListDataSourceType.Manual,
                })
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithSalesforceDataSource() =>
            CreateListRequest
                .Build()
                .WithName("my name")
                .WithDescription("my description")
                .WithTag("vip")
                .WithTag("sport")
                .WithAttribute(new ListAttribute
                {
                    Name = "phone_number",
                    Alias = "phone",
                })
                .WithDataSource(new ListDataSource
                {
                    Type = ListDataSourceType.Salesforce,
                    Soql = "some sql",
                    IntegrationId = "123456789",
                })
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());
    }
}