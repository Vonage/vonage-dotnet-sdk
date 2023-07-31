using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Items;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.GetItem
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
                .DeserializeObject<ListItem>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyItem);

        internal static void VerifyItem(ListItem success)
        {
            success.Id.Should().Be(new Guid("29192c4a-4058-49da-86c2-3e349d1065b7"));
            success.CreatedAt.Should().Be(DateTimeOffset.Parse("2022-06-19T17:59:28.085Z"));
            success.UpdatedAt.Should().Be(DateTimeOffset.Parse("2022-06-19T17:59:28.085Z"));
            success.ListId.Should().Be(new Guid("4cb98f71-a879-49f7-b5cf-2314353eb52c"));
            success.Data["value1"].ToString().Should().Be("value");
            int.Parse(success.Data["value2"].ToString()).Should().Be(0);
            bool.Parse(success.Data["value3"].ToString()).Should().BeTrue();
        }
    }
}