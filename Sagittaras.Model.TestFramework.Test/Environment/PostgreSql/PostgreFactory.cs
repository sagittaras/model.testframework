using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Sagittaras.Model.TestFramework.Test.Environment.PostgreSql;

public class PostgreFactory : TestFactory
{
    protected override string ConnectionString => "Default";

    protected override void OnConfiguring(ServiceCollection services)
    {
        services.AddDbContext<TestContext>(options => { options.UseNpgsql(GetConnectionString(Engine.DbEngine)); });
    }
}