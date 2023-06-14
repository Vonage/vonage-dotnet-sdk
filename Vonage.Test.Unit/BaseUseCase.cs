using Vonage.Common;
using Vonage.Common.Test;

namespace Vonage.Test.Unit
{
    public abstract class BaseUseCase
    {
        protected const string ApiKey = "27ebS990";

        protected BaseUseCase() =>
            this.helper = UseCaseHelper.WithSerializer(JsonSerializer.BuildWithSnakeCase());

        protected readonly UseCaseHelper helper;
    }
}