using System;
using System.IO.Abstractions;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.ProactiveConnect.Items.ImportItems;

internal class ImportItemsRequestBuilder : IBuilderForListId, IBuilderForFile, IVonageRequestBuilder<ImportItemsRequest>
{
    private byte[] file;
    private Guid listId;
    private readonly IFile fileAbstraction;

    public ImportItemsRequestBuilder(IFile fileAbstraction) => this.fileAbstraction = fileAbstraction;

    /// <inheritdoc />
    public Result<ImportItemsRequest> Create() =>
        Result<ImportItemsRequest>
            .FromSuccess(new ImportItemsRequest
            {
                ListId = this.listId,
                File = this.file,
            })
            .Map(InputEvaluation<ImportItemsRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyListId, VerifyFile));

    /// <inheritdoc />
    public IVonageRequestBuilder<ImportItemsRequest> WithFileData(byte[] value)
    {
        this.file = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<ImportItemsRequest> WithFilePath(string value)
    {
        try
        {
            this.file = this.fileAbstraction.ReadAllBytes(value);
        }
        catch
        {
            this.file = Array.Empty<byte>();
        }

        return this;
    }

    /// <inheritdoc />
    public IBuilderForFile WithListId(Guid value)
    {
        this.listId = value;
        return this;
    }

    private static Result<ImportItemsRequest> VerifyFile(ImportItemsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.File, nameof(request.File));

    private static Result<ImportItemsRequest> VerifyListId(ImportItemsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ListId, nameof(request.ListId));
}

/// <summary>
///     Represents a builder for ListId.
/// </summary>
public interface IBuilderForListId
{
    /// <summary>
    ///     Sets the ListId.
    /// </summary>
    /// <param name="value">The list Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForFile WithListId(Guid value);
}

/// <summary>
///     Represents a builder for File.
/// </summary>
public interface IBuilderForFile
{
    /// <summary>
    ///     Sets the File data on the builder.
    /// </summary>
    /// <param name="value">The file data.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<ImportItemsRequest> WithFileData(byte[] value);

    /// <summary>
    ///     Sets the File path on the builder.
    /// </summary>
    /// <param name="value">The file path.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<ImportItemsRequest> WithFilePath(string value);
}