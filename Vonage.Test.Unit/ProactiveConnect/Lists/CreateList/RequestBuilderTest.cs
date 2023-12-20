using FluentAssertions;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.CreateList
{
    public class RequestBuilderTest
    {
        private readonly string name;

        public RequestBuilderTest() => this.name = new string('a', 255);

        [Fact]
        public void Build_ShouldReturnFailure_GivenAttributeAliasLengthIsHigherThan50Characters() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithAttribute(new ListAttribute
                {
                    Alias = new string('a', 51),
                })
                .Create()
                .Should()
                .BeParsingFailure("Attributes Alias length cannot be higher than 50.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenAttributeNameLengthIsHigherThan50Characters() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithAttribute(new ListAttribute
                {
                    Name = new string('a', 51),
                })
                .Create()
                .Should()
                .BeParsingFailure("Attributes Name length cannot be higher than 50.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void
            Build_ShouldReturnFailure_GivenDataSourceIntegrationIdIsNullOrWhitespaceForSalesforce(string value) =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithDataSource(new ListDataSource
                {
                    Type = ListDataSourceType.Salesforce,
                    IntegrationId = value,
                    Soql = "Random value",
                })
                .Create()
                .Should()
                .BeParsingFailure("DataSource IntegrationId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenDescriptionLengthIsHigherThan1024Characters() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithDescription(new string('a', 1025))
                .Create()
                .Should()
                .BeParsingFailure("Description length cannot be higher than 1024.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenNameIsNullOrWhitespace(string value) =>
            CreateListRequest
                .Build()
                .WithName(value)
                .Create()
                .Should()
                .BeParsingFailure("Name cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenNameLengthIsHigherThan255Characters() =>
            CreateListRequest
                .Build()
                .WithName(new string('a', 256))
                .Create()
                .Should()
                .BeParsingFailure("Name length cannot be higher than 255.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenTagsContainMoreThan10Elements() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithTag("tag01")
                .WithTag("tag02")
                .WithTag("tag03")
                .WithTag("tag04")
                .WithTag("tag05")
                .WithTag("tag06")
                .WithTag("tag07")
                .WithTag("tag08")
                .WithTag("tag09")
                .WithTag("tag10")
                .WithTag("tag11")
                .Create()
                .Should()
                .BeParsingFailure("Tags count cannot be higher than 10.");

        [Fact]
        public void Build_ShouldShouldSetAttributes() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithAttribute(new ListAttribute
                {
                    Alias = "Phone",
                    Name = "phone_number",
                    Key = true,
                })
                .WithAttribute(new ListAttribute
                {
                    Alias = "Identifier",
                    Name = "id",
                })
                .Create()
                .Map(request => request.Attributes)
                .Should()
                .BeSuccess(tags => tags.Should().BeEquivalentTo(new[]
                {
                    new ListAttribute
                    {
                        Alias = "Phone",
                        Name = "phone_number",
                        Key = true,
                    },
                    new ListAttribute
                    {
                        Alias = "Identifier",
                        Name = "id",
                    },
                }));

        [Fact]
        public void Build_ShouldShouldSetDescription() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithDescription(new string('a', 1024))
                .Create()
                .Map(request => request.Description)
                .Should()
                .BeSuccess(new string('a', 1024));

        [Fact]
        public void Build_ShouldShouldSetManualDataSource() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithDataSource(new ListDataSource {Type = ListDataSourceType.Manual})
                .Create()
                .Map(request => request.DataSource)
                .Should()
                .BeSuccess(new ListDataSource {Type = ListDataSourceType.Manual});

        [Fact]
        public void Build_ShouldShouldSetName() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .Create()
                .Map(request => request.Name)
                .Should()
                .BeSuccess(this.name);

        [Fact]
        public void Build_ShouldShouldSetSalesforceDataSource() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithDataSource(new ListDataSource
                    {Type = ListDataSourceType.Salesforce, IntegrationId = "123", Soql = "Some sql"})
                .Create()
                .Map(request => request.DataSource)
                .Should()
                .BeSuccess(new ListDataSource
                    {Type = ListDataSourceType.Salesforce, IntegrationId = "123", Soql = "Some sql"});

        [Fact]
        public void Build_ShouldShouldSetTags() =>
            CreateListRequest
                .Build()
                .WithName(this.name)
                .WithTag("tag01")
                .WithTag("tag02")
                .Create()
                .Map(request => request.Tags)
                .Should()
                .BeSuccess(tags => tags.Should().BeEquivalentTo("tag01", "tag02"));
    }
}