using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Sagittaras.Model.TestFramework.Test.Environment;
using Sagittaras.Model.TestFramework.Test.Environment.PostgreSql;
using Xunit;
using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework.Test
{
    public class PostgreSqlTest : PostgreTest
    {
        public PostgreSqlTest(PostgreFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
        {
        }
        
        /// <summary>
        /// We inserts a new person in to database. There should be then exactly two persons.
        /// </summary>
        [Fact]
        public async Task Test_Insert()
        {
            Context.Add(new Person { FirstName = "Lucas", LastName = "Baker" });
            await Context.SaveChangesAsync();

            (await Context.Persons.CountAsync()).Should().Be(2);
        }
        
        /// <summary>
        /// There should be always exactly one person.
        /// </summary>
        [Fact]
        public async Task Test_TestData()
        {
            (await Context.Persons.CountAsync()).Should().Be(1);
        }
    }
}