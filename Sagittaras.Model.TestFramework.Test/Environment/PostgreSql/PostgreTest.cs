using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework.Test.Environment.PostgreSql
{
    public abstract class PostgreTest(PostgreFactory factory, ITestOutputHelper testOutputHelper) : UnitTest<PostgreFactory, TestContext>(factory, testOutputHelper);
}