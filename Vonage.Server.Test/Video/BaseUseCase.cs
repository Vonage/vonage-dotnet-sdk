using Vonage.Common.Test;
using Vonage.Server.Serialization;

namespace Vonage.Server.Test.Video
{
    public abstract class BaseUseCase
    {
        protected BaseUseCase() =>
            this.Helper = UseCaseHelper.WithSerializer(JsonSerializerBuilder.Build());

        protected readonly UseCaseHelper Helper;
    }
}