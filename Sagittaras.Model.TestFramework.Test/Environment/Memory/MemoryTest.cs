using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework.Test.Environment.Memory;

public abstract class MemoryTest(MemoryFactory factory, ITestOutputHelper testOutputHelper) : UnitTest<MemoryFactory, TestContext>(factory, testOutputHelper);