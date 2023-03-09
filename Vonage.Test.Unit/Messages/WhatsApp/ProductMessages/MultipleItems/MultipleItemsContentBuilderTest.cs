using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
using Vonage.Messages.WhatsApp.ProductMessages;
using Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;
using Xunit;

namespace Vonage.Test.Unit.Messages.WhatsApp.ProductMessages.MultipleItems
{
    public class MultipleItemsContentBuilderTest
    {
        private readonly Fixture fixture;
        private readonly IBuilderForHeader builder;

        public MultipleItemsContentBuilderTest()
        {
            this.builder = MultipleItemsContentBuilder.Initialize();
            this.fixture = new Fixture();
        }

        [Fact]
        public void Build_ShouldThrowVonageException_GivenAllSectionsContainMoreThan30Products()
        {
            Action act = () => this.builder
                .WithHeader(this.fixture.Create<string>())
                .WithBody(this.fixture.Create<string>())
                .WithFooter(this.fixture.Create<string>())
                .WithCatalogId(this.fixture.Create<string>())
                .WithSection(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithSection(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithSection(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithSection(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .Build();
            act.Should().ThrowExactly<VonageException>()
                .WithMessage("The message cannot have more than 30 products across all sections.");
        }

        [Fact]
        public void Build_ShouldThrowVonageException_GivenOneSectionContainsMoreThan10Products()
        {
            Action act = () => this.builder
                .WithHeader(this.fixture.Create<string>())
                .WithBody(this.fixture.Create<string>())
                .WithFooter(this.fixture.Create<string>())
                .WithCatalogId(this.fixture.Create<string>())
                .WithSection(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .WithProductRetailer(this.fixture.Create<string>())
                .Build();
            act.Should().ThrowExactly<VonageException>()
                .WithMessage("The message cannot have more than 10 products for a section.");
        }

        [Fact]
        public void Build_WithAllInformation() =>
            this.builder
                .WithHeader("Our top products")
                .WithBody("Check out these great products")
                .WithFooter("Sale now on!")
                .WithCatalogId("catalog_1")
                .WithSection("Cool products")
                .WithProductRetailer("product_1")
                .WithProductRetailer("product_2")
                .WithSection("Awesome products")
                .WithProductRetailer("product_3")
                .Build()
                .Should().BeEquivalentTo(new ProductMessage<MultipleItemsMessageContent>(
                    new MultipleItemsMessageContent(
                        new TextSection("Our top products", "text"),
                        new TextSection("Check out these great products"),
                        new TextSection("Sale now on!"),
                        new MultipleItemsAction("catalog_1",
                            new Section("Cool products", new ProductItem("product_1"), new ProductItem("product_2")),
                            new Section("Awesome products", new ProductItem("product_3"))))));

        [Fact]
        public void WithFooter_ShouldBeOptional() =>
            this.builder
                .WithHeader("Our top products")
                .WithBody("Check out these great products")
                .WithCatalogId("catalog_1")
                .WithSection("Cool products")
                .WithProductRetailer("product_1")
                .WithProductRetailer("product_2")
                .WithSection("Awesome products")
                .WithProductRetailer("product_3")
                .Build()
                .Should().BeEquivalentTo(new ProductMessage<MultipleItemsMessageContent>(
                    new MultipleItemsMessageContent(
                        new TextSection("Our top products", "text"),
                        new TextSection("Check out these great products"),
                        Maybe<TextSection>.None,
                        new MultipleItemsAction("catalog_1",
                            new Section("Cool products", new ProductItem("product_1"), new ProductItem("product_2")),
                            new Section("Awesome products", new ProductItem("product_3"))))));
    }
}