# Model Test Framework by [Sagittaras Games](https://github.com/sagittaras)
Library for creating xUnit tests of Database Layer.

## Features
* Services factory for tests
* Support for parallelism
  * Empty database per test case
  * Scoped instances of services per test case
  * Database clean-up after the test

## Usage
### Connection Strings
For the support of parallelism and clean database for every test case, each database should be initialized with unique name. This is done through
`GetConnectionString(engine)` method, which prepares the connection string with uniquely generated database name.

How the unique connection string is generated is declared by the parameter and enum `Engine`.

#### Engine.DbEngine
When we are testing a data model layer we also need a access to the database itself. To ensure unique database credentials
through environments the Test Framework is using [User Secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows).

Everything needed to do is just initialize a secret storage in the test project and add custom connection (_without database name_).

```shell
dotnet user-secrets set "ConnectionStrings:UnitTest" "Server=localhost;Port=5432;User Id=sa;Password=SuperSecretPassword"
```

The framework will automatically adds a `;Database=<name>` to the connection string with uniquely generated name.

**User used in database connection string should have rights to create and drop databases**

#### Engine.InMemory
When using InMemory database the connection string in user secrets is not required. Framework only generates a unique data source name.

### Test Factory
Test Factory is a class providing options to configure a service provider used inside of tests. It is needed to create a new
class extending from `TestFactory` and overrides `OnConfiguring` method.


```csharp
public class UnitTestFactory : TestFactory 
{
    /// <summary>
    /// By default the connection string name is defined as UnitTest. But we are able
    /// to change this name simply by overriding the ConnectionString property.
    /// </summary>
    protected override string ConnectionString => "Postgre";

    /// <summary>
    /// We define our database context with generated connection string and
    /// add any required service. Or modify the available ones.
    /// </summary>
    protected override void OnConfiguring(ServiceCollection services)
    {
        services.AddDbContext<MyDbContext>(options => {
            options.UseSqlServer(GetConnectionString(Engine.DbEngine)); // Or any other database engine
        });
        services.AddScoped<IUserService, UserService>();
    }
}
```

### Unit Test Class
For every test class, the `UnitTest` is base class with generic parameters pointing at our 
test factory and used instance of Database Context.

This class uses xUnit's `IAsyncLifetime` allowing asynchronous creating and deletion of used database.

Provided properties:
* `TFactory Factory { get; }` Generic property pointing at `TestFactory` class
* `ITestOutputHelper TestOutputHelper { get; }` xUnit's class writing outputs to test results
* `IServiceProvider ServiceProvider { get; }` Instance of scoped service provider for the current test
* `TDbContext Context { get; }` Generic property with access to database context class

```csharp
/// <summary>
/// We can extend a new class to create custom basic class for the Unit test. Allowing
/// multiple different configurations for the Unit tests.
/// </summary>
public abstract class MyUnitTest : UnitTest<UnitTestFactory, MyDbContext> 
{
    protected MyUnitTest(UnitTestFactory factory, ITestOutputHelper testOutputHelper) : base(factory, testOutputHelper)
    {
    }
}
```

# Contact
* [Sagittaras Games GitHub](https://github.com/sagittaras)