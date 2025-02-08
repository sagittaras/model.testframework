using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sagittaras.Model.TestFramework;

/// <summary>
///     Factory class for model unit tests
/// </summary>
/// <remarks>
///     Class is preparing testing environment and single database per every test.
/// </remarks>
public class TestFactory
{
    /// <summary>
    ///     Instance of configuration for the test.
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    ///     Collection for building <see cref="IServiceProvider" />
    /// </summary>
    private readonly ServiceCollection _serviceCollection = [];

    /// <summary>
    ///     Backing field for <see cref="ServiceProvider" />
    /// </summary>
    private IServiceProvider? _serviceProvider;

    /// <summary>
    ///     Initializes a new instance of <see cref="TestFactory" /> with configured services.
    /// </summary>
    protected TestFactory()
    {
        DatabaseName = Guid.NewGuid().ToString("N");
        _configuration = ConfigurationFactory.Create(GetType().Assembly);
        _serviceCollection.AddSingleton(_configuration);
    }

    /// <summary>
    ///     Access to Dependency Container.
    /// </summary>
    /// <remarks>
    ///     Provider is created on demand. If the provider has no instance yet, configuration and building process
    ///     will be called.
    /// </remarks>
    public IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider is null)
            {
                OnConfiguring(_serviceCollection);
                _serviceProvider = _serviceCollection.BuildServiceProvider();
            }

            return _serviceProvider;
        }
    }

    /// <summary>
    ///     Gets the name of connection string inside the configuration.
    /// </summary>
    protected virtual string ConnectionString => "UnitTest";

    /// <summary>
    ///     Database name as uniquely generated GUID.
    /// </summary>
    protected string DatabaseName { get; }

    /// <summary>
    ///     Method providing additional way to register custom services.
    /// </summary>
    /// <param name="services">Services collection</param>
    protected virtual void OnConfiguring(ServiceCollection services)
    {
    }

    /// <summary>
    ///     Creates a connection string with uniquely named database.
    /// </summary>
    /// <returns>Default connection string.</returns>
    protected string GetConnectionString(Engine engine)
    {
        if (engine == Engine.InMemory) return DatabaseName;

        string baseConnectionString = _configuration.GetConnectionString(ConnectionString) ?? throw new InvalidOperationException($"Connection string `{ConnectionString}` not found");
        return $"{baseConnectionString};Database={DatabaseName}";
    }
}