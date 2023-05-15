using System;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.VerifyV2.Cancel;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.Cancel
{
    public class RequestTest
    {
        [Fact]
        public void Create_ShouldReturnFailure_GivenRequestIsEmpty() =>
            CancelRequest.Parse(Guid.Empty)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RequestId cannot be empty."));

        [Fact]
        public void Create_ShouldReturnSuccess() =>
            CancelRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
                .Map(request => request.RequestId)
                .Should()
                .BeSuccess(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"));

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CancelRequest.Parse(new Guid("f3a065af-ac5a-47a4-8dfe-819561a7a287"))
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v2/verify/f3a065af-ac5a-47a4-8dfe-819561a7a287");
    }
}