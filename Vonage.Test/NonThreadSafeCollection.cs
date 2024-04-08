using Xunit;

namespace Vonage.Test;

[CollectionDefinition(nameof(NonThreadSafeCollection), DisableParallelization = true)]
public class NonThreadSafeCollection
{
}