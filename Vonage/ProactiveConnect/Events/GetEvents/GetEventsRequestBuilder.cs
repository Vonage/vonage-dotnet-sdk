using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.ProactiveConnect.Events.GetEvents;

internal class GetEventsRequestBuilder : IBuilderForPage, IBuilderForPageSize, IVonageRequestBuilder<GetEventsRequest>
{
    private int pageSize;
    private int page;

    /// <inheritdoc />
    public Result<GetEventsRequest> Create() =>
        Result<GetEventsRequest>
            .FromSuccess(new GetEventsRequest
            {
                Page = this.page,
                PageSize = this.pageSize,
            });

    /// <inheritdoc />
    public IBuilderForPageSize WithPage(int value)
    {
        this.page = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<GetEventsRequest> WithPageSize(int value)
    {
        this.pageSize = value;
        return this;
    }
}

/// <summary>
///     Represents a builder for Page.
/// </summary>
public interface IBuilderForPage
{
    /// <summary>
    ///     Sets the Page.
    /// </summary>
    /// <param name="value">The page.</param>
    /// <returns>The builder.</returns>
    IBuilderForPageSize WithPage(int value);
}

/// <summary>
///     Represents a builder for PageSize.
/// </summary>
public interface IBuilderForPageSize
{
    /// <summary>
    ///     Sets the PageSize on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetEventsRequest> WithPageSize(int value);
}