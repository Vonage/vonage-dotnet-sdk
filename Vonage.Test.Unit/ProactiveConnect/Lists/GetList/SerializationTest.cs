using System;
using FluentAssertions;
using Vonage.ProactiveConnect.Lists;
using Vonage.Serialization;
using Vonage.Test.Unit.Common;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetList
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithSnakeCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<List>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyList);

        internal static void VerifyList(List success)
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
        }
    }
}