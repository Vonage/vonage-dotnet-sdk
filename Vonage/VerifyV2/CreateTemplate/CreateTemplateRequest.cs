﻿#region
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.VerifyV2.CreateTemplate;

/// <inheritdoc />
[Builder]
public readonly partial struct CreateTemplateRequest : IVonageRequest
{
    /// <summary>
    ///     Reference name for template.
    /// </summary>
    [Mandatory(0, nameof(VerifyNameNotEmpty))]
    public string Name { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v2/verify/templates";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal static Result<CreateTemplateRequest> VerifyNameNotEmpty(
        CreateTemplateRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Name, nameof(request.Name));
}