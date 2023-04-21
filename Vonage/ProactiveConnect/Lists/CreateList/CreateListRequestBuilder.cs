using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Lists.CreateList;

internal class CreateListRequestBuilder : IBuilderForName, IBuilderForOptional
{
    private readonly HashSet<string> tags = new();
    private readonly List<ListAttribute> attributes = new();
    private Maybe<ListDataSource> dataSource;
    private Maybe<string> description;
    private string name;

    /// <inheritdoc />
    public Result<CreateListRequest> Create() => Result<CreateListRequest>.FromSuccess(new CreateListRequest
        {
            Attributes = this.attributes.ToList(),
            Description = this.description,
            Name = this.name,
            Tags = this.tags.ToList(),
            DataSource = this.dataSource,
        })
        .Bind(VerifyNameNotEmpty)
        .Bind(VerifyNameLength)
        .Bind(VerifyDescriptionLength)
        .Bind(VerifyTagsCount)
        .Bind(VerifyAttributesNameLength)
        .Bind(VerifyAttributesAliasLength)
        .Bind(VerifyIntegrationIdNotEmptyWhenSalesforce)
        .Bind(VerifySoqlNotEmptyWhenSalesforce);

    /// <inheritdoc />
    public IBuilderForOptional WithAttribute(ListAttribute value)
    {
        this.attributes.Add(value);
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithDataSource(ListDataSource value)
    {
        this.dataSource = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithDescription(string value)
    {
        this.description = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithName(string value)
    {
        this.name = value;
        return this;
    }

    /// <inheritdoc />
    public IBuilderForOptional WithTag(string value)
    {
        this.tags.Add(value);
        return this;
    }

    private static Result<CreateListRequest> VerifyAttributesAliasLength(CreateListRequest request)
    {
        var results = request.Attributes.Select(attribute =>
                InputValidation.VerifyLengthLowerOrEqualThan(request, attribute.Alias, 50,
                    $"{nameof(request.Attributes)} {nameof(attribute.Alias)}"))
            .Where(result => result.IsFailure)
            .ToArray();
        return results.Any() ? results[0] : request;
    }

    private static Result<CreateListRequest> VerifyAttributesNameLength(CreateListRequest request)
    {
        var results = request.Attributes.Select(attribute =>
                InputValidation.VerifyLengthLowerOrEqualThan(request, attribute.Name, 50,
                    $"{nameof(request.Attributes)} {nameof(attribute.Name)}"))
            .Where(result => result.IsFailure)
            .ToArray();
        return results.Any() ? results[0] : request;
    }

    private static Result<CreateListRequest> VerifyDescriptionLength(CreateListRequest request) =>
        request.Description.Match(
            some => InputValidation.VerifyLengthLowerOrEqualThan(request, some, 1024, nameof(request.Description)),
            () => request);

    private static Result<CreateListRequest> VerifyIntegrationIdNotEmptyWhenSalesforce(CreateListRequest request)
    {
        var value = request.DataSource.IfNone(new ListDataSource {Type = ListDataSourceType.Manual});
        return value.Type == ListDataSourceType.Salesforce
            ? InputValidation.VerifyNotEmpty(request, value.IntegrationId,
                $"{nameof(request.DataSource)} {nameof(value.IntegrationId)}")
            : request;
    }

    private static Result<CreateListRequest> VerifyNameLength(CreateListRequest request) =>
        InputValidation.VerifyLengthLowerOrEqualThan(request, request.Name, 255, nameof(request.Name));

    private static Result<CreateListRequest> VerifyNameNotEmpty(CreateListRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Name, nameof(request.Name));

    private static Result<CreateListRequest> VerifySoqlNotEmptyWhenSalesforce(CreateListRequest request)
    {
        var value = request.DataSource.IfNone(new ListDataSource {Type = ListDataSourceType.Manual});
        return value.Type == ListDataSourceType.Salesforce
            ? InputValidation.VerifyNotEmpty(request, value.Soql,
                $"{nameof(request.DataSource)} {nameof(value.Soql)}")
            : request;
    }

    private static Result<CreateListRequest> VerifyTagsCount(CreateListRequest request) =>
        InputValidation.VerifyCountLowerOrEqualThan(request, request.Tags, 10, nameof(request.Tags));
}

/// <summary>
///     Represents a builder for Name.
/// </summary>
public interface IBuilderForName
{
    /// <summary>
    ///     Sets the Name on the builder, up to 255 characters.
    /// </summary>
    /// <param name="value">The name.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithName(string value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateListRequest>
{
    /// <summary>
    ///     Adds an attribute on the builder.
    /// </summary>
    /// <param name="value">The attribute.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithAttribute(ListAttribute value);

    /// <summary>
    ///     Adds an data source on the builder.
    /// </summary>
    /// <param name="value">The data source.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithDataSource(ListDataSource value);

    /// <summary>
    ///     Sets the Description on the builder, up to 1024 characters.
    /// </summary>
    /// <param name="value">The description.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithDescription(string value);

    /// <summary>
    ///     Adds a tag on the builder. The request allows up to 10 tags, each must be between 1 and 15 characters.
    /// </summary>
    /// <param name="value">The tag.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithTag(string value);
}