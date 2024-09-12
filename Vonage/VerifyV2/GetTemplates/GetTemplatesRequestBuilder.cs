#region
using Vonage.Common.Client;
using Vonage.Common.Monads;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

internal struct GetTemplatesRequestBuilder : IBuilderForOptional
{
    private Maybe<int> page;
    private Maybe<int> pageSize;

    /// <inheritdoc />
    public Result<GetTemplatesRequest> Create() => Result<GetTemplatesRequest>.FromSuccess(new GetTemplatesRequest
    {
        PageSize = this.pageSize,
        Page = this.page,
    });

    public IBuilderForOptional WithPageSize(int value) => this with {pageSize = value};

    public IBuilderForOptional WithPage(int value) => this with {page = value};
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<GetTemplatesRequest>
{
    /// <summary>
    ///     Sets the page size on the builder.
    /// </summary>
    /// <param name="value">The page size.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPageSize(int value);

    /// <summary>
    ///     Sets the page on the builder.
    /// </summary>
    /// <param name="value">The page.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPage(int value);
}