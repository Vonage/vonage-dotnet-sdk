﻿#region
using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
using Vonage.Server;
#endregion

namespace Vonage.Video.Archives.ChangeLayout;

/// <summary>
///     Represents a request to change the layout of an archive.
/// </summary>
[Builder]
public readonly partial struct ChangeLayoutRequest : IVonageRequest, IHasApplicationId, IHasArchiveId
{
    /// <summary>
    ///     The layout to apply of the archive.
    /// </summary>
    [Mandatory(2)]
    public Layout Layout { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public Guid ArchiveId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Put, $"/v2/project/{this.ApplicationId}/archive/{this.ArchiveId}/layout")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this.Layout), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<ChangeLayoutRequest> VerifyArchiveId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ArchiveId, nameof(request.ArchiveId));

    [ValidationRule]
    internal static Result<ChangeLayoutRequest> VerifyApplicationId(ChangeLayoutRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));
}