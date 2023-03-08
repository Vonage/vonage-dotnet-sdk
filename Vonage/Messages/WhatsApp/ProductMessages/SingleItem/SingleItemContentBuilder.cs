namespace Vonage.Messages.WhatsApp.ProductMessages.SingleItem;

/// <summary>
///     Represents a builder for Single Items product message content.
/// </summary>
public class SingleItemContentBuilder :
    IBuilderForBody,
    IBuilderForFooter,
    IBuilderForCatalog,
    IBuilderForProductRetailer,
    IBuildable
{
    private string catalogId;
    private string productRetailerId;
    private TextSection body;
    private TextSection footer;

    private SingleItemContentBuilder()
    {
    }

    /// <inheritdoc />
    public ProductMessage<SingleItemMessageContent> Build() =>
        new(new SingleItemMessageContent(this.body, this.footer,
            new SingleItemAction(this.catalogId, this.productRetailerId)));

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder</returns>
    public static IBuilderForBody Initialize() => new SingleItemContentBuilder();

    /// <inheritdoc />
    public IBuilderForFooter WithBody(string value)
    {
        this.body = new TextSection(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForProductRetailer WithCatalogId(string value)
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
    public IBuildable WithProductRetailerId(string value)
    {
        this.productRetailerId = value;
        return this;
    }
}

/// <summary>
///     Represents a builder that allows to set the Body.
/// </summary>
public interface IBuilderForBody
{
    /// <summary>
    ///     Sets the Body text.
    /// </summary>
    /// <param name="value">The Body.</param>
    /// <returns>The builder</returns>
    IBuilderForFooter WithBody(string value);
}

/// <summary>
///     Represents a builder that allows to set the Footer.
/// </summary>
public interface IBuilderForFooter
{
    /// <summary>
    ///     Sets the Footer text.
    /// </summary>
    /// <param name="value">The Footer.</param>
    /// <returns>The builder</returns>
    IBuilderForCatalog WithFooter(string value);
}

/// <summary>
///     Represents a builder that allows to set the Catalog.
/// </summary>
public interface IBuilderForCatalog
{
    /// <summary>
    ///     Sets the CatalogId.
    /// </summary>
    /// <param name="value">The catalog Id.</param>
    /// <returns>The builder</returns>
    IBuilderForProductRetailer WithCatalogId(string value);
}

/// <summary>
///     Represents a builder that allows to set a Product Retailer.
/// </summary>
public interface IBuilderForProductRetailer
{
    /// <summary>
    ///     Sets the product retailer Id.
    /// </summary>
    /// <param name="value">The product retailer Id.</param>
    /// <returns>The builder</returns>
    IBuildable WithProductRetailerId(string value);
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
    ProductMessage<SingleItemMessageContent> Build();
}