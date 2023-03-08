using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Monads;

namespace Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;

/// <summary>
///     Represents a builder for a Multiple Items product message content.
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
            this.header, this.body, this.footer, new MultipleItemsAction(this.catalogId, this.sections.ToArray())));
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForHeader Initialize() => new MultipleItemsContentBuilder();

    /// <inheritdoc />
    public IBuilderForFooter WithBody(string value)
    {
        this.body = new TextSection(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForMandatorySection WithCatalogId(string value)
    {
        this.catalogId = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForCatalog WithFooter(string value)
    {
        this.footer = new TextSection(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForBody WithHeader(string value)
    {
        this.header = new TextSection(value, "text");
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

/// <summary>
///     Represents a builder that allows to set the Header.
/// </summary>
public interface IBuilderForHeader
{
    /// <summary>
    ///     Sets the Header.
    /// </summary>
    /// <param name="value">The header.</param>
    /// <returns>The builder.</returns>
    IBuilderForBody WithHeader(string value);
}

/// <summary>
///     Represents a builder that allows to set the Body.
/// </summary>
public interface IBuilderForBody
{
    /// <summary>
    ///     Sets the Body.
    /// </summary>
    /// <param name="value">The body.</param>
    /// <returns>The builder.</returns>
    IBuilderForFooter WithBody(string value);
}

/// <summary>
///     Represents a builder that allows to set the Footer.
/// </summary>
public interface IBuilderForFooter
{
    /// <summary>
    ///     Sets the Footer.
    /// </summary>
    /// <param name="value">The footer.</param>
    /// <returns>The builder.</returns>
    IBuilderForCatalog WithFooter(string value);
}

/// <summary>
///     Represents a builder that allows to set the CatalogId.
/// </summary>
public interface IBuilderForCatalog
{
    /// <summary>
    ///     Sets the CatalogId.
    /// </summary>
    /// <param name="value">The catalog Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForMandatorySection WithCatalogId(string value);
}

/// <summary>
///     Represents a builder that allows to set a Section.
/// </summary>
public interface IBuilderForMandatorySection
{
    /// <summary>
    ///     Sets a section.
    /// </summary>
    /// <param name="value">The section.</param>
    /// <returns>The builder.</returns>
    IBuilderForProductRetailer WithSection(string value);
}

/// <summary>
///     Represents a builder that allows to set a Section.
/// </summary>
public interface IBuilderForOptionalSection : IBuildable, IBuilderForMandatorySection, IBuilderForProductRetailer
{
}

/// <summary>
///     Represents a builder that allows to set a product retailer Id to the previous section.
/// </summary>
public interface IBuilderForProductRetailer
{
    /// <summary>
    ///     Sets the product retailer Id to the previous section.
    /// </summary>
    /// <param name="value">The product retailer Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptionalSection WithProductRetailer(string value);
}

/// <summary>
///     Represents a builder that allows to build the product message.
/// </summary>
public interface IBuildable
{
    /// <summary>
    ///     Builds the product message using all specified values.
    /// </summary>
    /// <returns>The product message.</returns>
    ProductMessage<MultipleItemsMessageContent> Build();
}