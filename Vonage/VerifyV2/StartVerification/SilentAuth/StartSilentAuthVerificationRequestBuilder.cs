using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.VerifyV2.StartVerification.SilentAuth;

/// <summary>
///     Represents a builder for a StartSilentAuthVerificationRequest.
/// </summary>
internal class StartSilentAuthVerificationRequestBuilder : IBuilderForBrand, IBuilderForWorkflow,
    IVonageRequestBuilder<StartSilentAuthVerificationRequest>
{
    private Result<SilentAuthWorkflow> workflow;
    private string brand;

    /// <inheritdoc />
    public Result<StartSilentAuthVerificationRequest> Create() => this.workflow
        .Map(this.ToVerificationRequest)
        .Bind(VerifyBrandNotEmpty);

    /// <inheritdoc />
    public IBuilderForWorkflow WithBrand(string value)
    {
        this.brand = value;
        return this;
    }

    /// <inheritdoc />
    public IVonageRequestBuilder<StartSilentAuthVerificationRequest> WithWorkflow(Result<SilentAuthWorkflow> value)
    {
        this.workflow = value;
        return this;
    }

    private StartSilentAuthVerificationRequest ToVerificationRequest(SilentAuthWorkflow value) =>
        new()
        {
            Brand = this.brand,
            Workflows = new[] {value},
        };

    private static Result<StartSilentAuthVerificationRequest> VerifyBrandNotEmpty(
        StartSilentAuthVerificationRequest request) =>
        InputValidation
            .VerifyNotEmpty(request, request.Brand, nameof(request.Brand));
}

/// <summary>
///     Represents a builder for Brand.
/// </summary>
public interface IBuilderForBrand
{
    /// <summary>
    ///     Sets the Brand.
    /// </summary>
    /// <param name="value">The Brand.</param>
    /// <returns>The builder.</returns>
    IBuilderForWorkflow WithBrand(string value);
}

/// <summary>
///     Represents a builder for Workflow.
/// </summary>
public interface IBuilderForWorkflow
{
    /// <summary>
    ///     Sets the Workflow.
    /// </summary>
    /// <param name="value">The Workflow.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<StartSilentAuthVerificationRequest> WithWorkflow(Result<SilentAuthWorkflow> value);
}