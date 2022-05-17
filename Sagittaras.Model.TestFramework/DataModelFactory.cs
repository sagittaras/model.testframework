using System;
using System.IO;
using System.Linq;
using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Sagittaras.Model.TestFramework
{
    /// <summary>
    /// Factory class for model unit tests
    /// </summary>
    /// <remarks>
    /// Class is preparing testing environment and single database per every test.
    /// </remarks>
    public class DataModelFactory
    {
        /// <summary>
        /// A name of file containing the default connection string.
        /// </summary>
        private const string ConnectionStringFile = "connectionString.txt";

        /// <summary>
        /// Collection for building <see cref="IServiceProvider" />
        /// </summary>
        private readonly ServiceCollection _serviceCollection = new();

        /// <summary>
        /// Backing field for <see cref="ServiceProvider" />
        /// </summary>
        private IServiceProvider? _serviceProvider;

        /// <summary>
        /// Initializes a new instance of <see cref="DataModelFactory" /> with configured services.
        /// </summary>
        protected DataModelFactory()
        {
            _serviceCollection.AddSingleton(Mock.Of<IConfiguration>());
        }

        /// <summary>
        /// Access to Dependency Container.
        /// </summary>
        /// <remarks>
        /// Provider is created on demand. If the provider has no instance yet, configuration and building process
        /// will be called.
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
        /// Method providing additional way to register custom services.
        /// </summary>
        /// <param name="services">Services collection</param>
        protected virtual void OnConfiguring(ServiceCollection services)
        {
        }

        /// <summary>
        /// Loads up a default connection string from <see cref="ConnectionStringFile" /> and adds
        /// randomly generated database name to ensure each test will be running in separate database
        /// context.
        /// </summary>
        /// <returns>Default connection string.</returns>
        /// <exception cref="DirectoryNotFoundException">Unable to determine current location.</exception>
        protected static string GetConnectionString()
        {
            string? classLocation = Path.GetDirectoryName(new Uri(typeof(DataModelFactory).Assembly.Location).LocalPath);
            if (classLocation is null)
            {
                throw new DirectoryNotFoundException($"Could not determine location of {nameof(DataModelFactory)}");
            }

            string filePath = Path.Combine(classLocation, ConnectionStringFile);
            string dbName = $"xunit_{Guid.NewGuid():N}";
            return $"{File.ReadAllLines(filePath).First()};Database={dbName}";
        }
    }
}