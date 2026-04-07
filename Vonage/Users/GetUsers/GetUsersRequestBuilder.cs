using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Users.GetUsers;

internal class GetUsersRequestBuilder : IBuilderForOptional
{
    private FetchOrder order = FetchOrder.Ascending;
    private int pageSize = 10;
    private readonly Maybe<string> cursor;
    private Maybe<string> name;

    internal GetUsersRequestBuilder(Maybe<string> cursor) => this.cursor = cursor;

    /// <inheritdoc />
    public Result<GetUsersRequest> Create() => Result<GetUsersRequest>.FromSuccess(new GetUsersRequest
    {
        Cursor = this.cursor,
        PageSize = this.pageSize,
        Order = this.order,
        Name = this.name,
    });

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
///     Represents a builder for configuring optional parameters when retrieving users.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetUsersRequest>
{
    /// <summary>
    ///     Filters results to users matching the specified unique name.
    /// </summary>
    /// <param name="value">The exact user name to filter by.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithName("my-user")
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithName(string value);

    /// <summary>
    ///     Sets the sort order for the results.
    /// </summary>
    /// <param name="value">The sort order. Use <see cref="FetchOrder.Ascending"/> for oldest first or <see cref="FetchOrder.Descending"/> for newest first.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithOrder(FetchOrder.Descending)
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithOrder(FetchOrder value);

    /// <summary>
    ///     Sets the maximum number of user records to return per page.
    /// </summary>
    /// <param name="value">The page size. Defaults to 10 if not specified.</param>
    /// <returns>The builder instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// .WithPageSize(25)
    /// ]]></code>
    /// </example>
    IBuilderForOptional WithPageSize(int value);
}