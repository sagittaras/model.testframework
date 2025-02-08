using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sagittaras.Model.TestFramework.Test.Environment.Memory;

public class MemoryFactory : TestFactory
{
    protected override void OnConfiguring(ServiceCollection services)
    {
        services.AddDbContext<TestContext>(options => { options.UseInMemoryDatabase(GetConnectionString(Engine.InMemory)); });
    }
}