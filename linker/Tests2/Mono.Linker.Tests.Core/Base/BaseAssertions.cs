namespace Mono.Linker.Tests.Core.Base
{
    public abstract class BaseAssertions
    {
        public abstract void IsNull(object obj, string message);

        public abstract void IsNotNull(object obj, string message);

        public abstract void IsTrue(bool value, string message);

        public abstract void Ignore(string reason);
    }
}
