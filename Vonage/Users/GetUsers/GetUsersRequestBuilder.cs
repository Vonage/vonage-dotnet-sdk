using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect;

namespace Vonage.Users.GetUsers;

internal class GetUsersRequestBuilder : IBuilderForOptional
{
    private int pageSize = 10;
    private FetchOrder order = FetchOrder.Ascending;
    private Maybe<string> name;

    /// <inheritdoc />
    public Result<GetUsersRequest> Create() => Result<GetUsersRequest>.FromSuccess(new GetUsersRequest(Maybe<string>.None, this.name, this.order, this.pageSize));

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithOrder(FetchOrder value)
    {
        this.order = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetUsersRequest>
{
    /// <summary>
    ///     Sets the user name on the builder.
    /// </summary>
    /// <param name="value">The user name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the order on the builder.
    /// </summary>
    /// <param name="value">The order.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithOrder(FetchOrder value);

    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPageSize(int value);
}