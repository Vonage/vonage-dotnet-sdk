using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Monads;

namespace Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;

/// <summary>
///     Represents a builder to build a Multiple Items product message content.
/// </summary>
public class MultipleItemsContentBuilder :
    IBuilderForHeader,
    IBuilderForBody,
    IBuilderForFooter,
    IBuilderForCatalog,
    IBuilderForOptionalSection
{
    private readonly List<Section> sections = new();
    private readonly List<string> productIds = new();
    private Maybe<string> section;
    private string catalogId;
    private TextSection body;
    private TextSection footer;
    private TextSection header;

    private MultipleItemsContentBuilder()
    {
    }

    /// <inheritdoc />
    public ProductMessage<MultipleItemsMessageContent> Build()
    {
        this.FinalizeSection();
        return new ProductMessage<MultipleItemsMessageContent>(new MultipleItemsMessageContent(
            this.header, this.body, this.footer, new ActionMultipleItems(this.catalogId, this.sections.ToArray())));
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static IBuilderForHeader InitializeForMultipleItems() => new MultipleItemsContentBuilder();

    /// <inheritdoc />
    public IBuilderForFooter WithBody(TextSection value)
    {
        this.body = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForMandatorySection WithCatalogId(string value)
    {
        this.catalogId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCatalog WithFooter(TextSection value)
    {
        this.footer = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForBody WithHeader(TextSection value)
    {
        this.header = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptionalSection WithProductRetailer(string value)
    {
        this.productIds.Add(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForProductRetailer WithSection(string value)
    {
        this.FinalizeSection();
        this.section = value;
        return this;
    }

    private void FinalizeSection()
    {
        var maybe = this.section
            .Map(sectionTitle => new Section(sectionTitle,
                this.productIds.Select(productId => new ProductItem(productId)).ToArray()));
        maybe.IfSome(newSection =>
        {
            this.sections.Add(newSection);
            this.section = Maybe<string>.None;
            this.productIds.Clear();
        });
    }
}

public interface IBuilderForHeader
{
    IBuilderForBody WithHeader(TextSection value);
}

public interface IBuilderForBody
{
    IBuilderForFooter WithBody(TextSection value);
}

public interface IBuilderForFooter
{
    IBuilderForCatalog WithFooter(TextSection value);
}

public interface IBuilderForCatalog
{
    IBuilderForMandatorySection WithCatalogId(string value);
}

public interface IBuilderForMandatorySection
{
    IBuilderForProductRetailer WithSection(string value);
}

public interface IBuilderForOptionalSection : IBuildable, IBuilderForMandatorySection, IBuilderForProductRetailer
{
}

public interface IBuilderForProductRetailer
{
    IBuilderForOptionalSection WithProductRetailer(string value);
}

public interface IBuildable
{
    ProductMessage<MultipleItemsMessageContent> Build();
}