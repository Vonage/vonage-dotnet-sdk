namespace Vonage.Messages.WhatsApp.ProductMessages.SingleItem;

/// <summary>
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
    /// </summary>
    /// <returns></returns>
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

public interface IBuilderForBody
{
    IBuilderForFooter WithBody(string value);
}

public interface IBuilderForFooter
{
    IBuilderForCatalog WithFooter(string value);
}

public interface IBuilderForCatalog
{
    IBuilderForProductRetailer WithCatalogId(string value);
}

public interface IBuilderForProductRetailer
{
    IBuildable WithProductRetailerId(string value);
}

public interface IBuildable
{
    ProductMessage<SingleItemMessageContent> Build();
}