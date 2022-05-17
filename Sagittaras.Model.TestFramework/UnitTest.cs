using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Sagittaras.Model.TestFramework
{
    /// <summary>
    /// Implementation of <see cref="IClassFixture{TFixture}" /> into custom default class for unit testing.
    /// Class is using interface <see cref="IAsyncLifetime" />, so every Database test starts from scratch.
    /// </summary>
    public abstract class UnitTest<TFactory, TDbContext> : IClassFixture<TFactory>, IAsyncLifetime
        where TFactory : DataModelFactory
        where TDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of Unit Test with prepared helper classes.
        /// </summary>
        /// <remarks>
        /// Instance of service provider for each test has custom scope.
        /// </remarks>
        /// <param name="factory">Factory which creates context for tests.</param>
        /// <param name="testOutputHelper">Instance of output helper for debugging.</param>
        protected UnitTest(TFactory factory, ITestOutputHelper testOutputHelper)
        {
            Factory = factory;
            TestOutputHelper = testOutputHelper;
            ServiceProvider = factory.ServiceProvider.CreateScope().ServiceProvider;
            Context = ServiceProvider.GetRequiredService<TDbContext>();
        }

        /// <summary>
        /// Instance of Factory creating Test Context.
        /// </summary>
        protected TFactory Factory { get; }

        /// <summary>
        /// Instance of <see cref="ITestOutputHelper" /> for output debugging purposes.
        /// </summary>
        protected ITestOutputHelper TestOutputHelper { get; }

        /// <summary>
        /// Instance of new Scope of <see cref="IServiceProvider" /> in current context.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Instance of actual used <see cref="TDbContext" />
        /// </summary>
        protected TDbContext Context { get; }

        /// <inheritdoc />
        public virtual async Task InitializeAsync()
        {
            await Context.Database.EnsureCreatedAsync();
        }

        /// <inheritdoc />
        public virtual async Task DisposeAsync()
        {
            await Context.Database.EnsureDeletedAsync();
        }
    }
}