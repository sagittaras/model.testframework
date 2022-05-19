using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework.Test.Environment.Memory
{
    public abstract class MemoryTest : UnitTest<MemoryFactory, MyContext>
    {
        protected MemoryTest(MemoryFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
        {
        }
    }
}