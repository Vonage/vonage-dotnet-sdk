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