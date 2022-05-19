using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework.Test.Environment.PostgreSql
{
    public abstract class PostgreTest : UnitTest<PostgreFactory, MyContext>
    {
        protected PostgreTest(PostgreFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
        {
        }
    }
}