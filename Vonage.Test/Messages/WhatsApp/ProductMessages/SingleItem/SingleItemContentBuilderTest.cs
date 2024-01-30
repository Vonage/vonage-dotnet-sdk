using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Messages.WhatsApp.ProductMessages;
using Vonage.Messages.WhatsApp.ProductMessages.SingleItem;
using Xunit;

namespace Vonage.Test.Messages.WhatsApp.ProductMessages.SingleItem
{
    [Trait("Category", "Request")]
    public class SingleItemContentBuilderTest
    {
        private readonly IOptionalBuilderForBody builder = SingleItemContentBuilder.Initialize();

        [Fact]
        public void Build_WithAllInformation() =>
            this.builder
                .WithBody("Check out this cool product")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build()
                .Should().Be(new ProductMessage<SingleItemMessageContent>(
                    new SingleItemMessageContent(new TextSection("Check out this cool product"),
                        new TextSection("Sale now on!"),
                        new SingleItemAction("catalog_1", "product_1"))));

        [Fact]
        public void Build_WithMandatoryInformation() =>
            this.builder
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build()
                .Should().Be(new ProductMessage<SingleItemMessageContent>(
                    new SingleItemMessageContent(Maybe<TextSection>.None, Maybe<TextSection>.None,
                        new SingleItemAction("catalog_1", "product_1"))));

        [Fact]
        public void WithBody_ShouldBeOptional() =>
            this.builder
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build()
                .Should().Be(new ProductMessage<SingleItemMessageContent>(
                    new SingleItemMessageContent(Maybe<TextSection>.None, new TextSection("Sale now on!"),
                        new SingleItemAction("catalog_1", "product_1"))));

        [Fact]
        public void WithFooter_ShouldBeOptional() =>
            this.builder
                .WithBody("Check out this cool product")
                .WithCatalogId("catalog_1")
                .WithProductRetailerId("product_1")
                .Build()
                .Should().Be(new ProductMessage<SingleItemMessageContent>(
                    new SingleItemMessageContent(new TextSection("Check out this cool product"),
                        Maybe<TextSection>.None,
                        new SingleItemAction("catalog_1", "product_1"))));
    }
}