using Vonage.Common.Test;
using Vonage.Server.Serialization;

namespace Vonage.Server.Test.Video
{
    public abstract class BaseUseCase
    {
        protected BaseUseCase() =>
            this.Helper = UseCaseHelperNew.WithSerializer(JsonSerializerBuilder.Build());

        protected readonly UseCaseHelperNew Helper;
    }
}