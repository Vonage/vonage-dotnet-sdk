using Vonage.Common;
using Vonage.Common.Test;

namespace Vonage.Test.Unit
{
    public abstract class BaseUseCase
    {
        protected BaseUseCase() =>
            this.helper = UseCaseHelper.WithSerializer(JsonSerializer.BuildWithSnakeCase());

        protected readonly UseCaseHelper helper;
    }
}