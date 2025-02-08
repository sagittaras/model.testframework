using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Sagittaras.Model.TestFramework;

/// <summary>
///     Factory for creating configuration instance.
/// </summary>
internal static class ConfigurationFactory
{
    /// <summary>
    ///     Creates a new instance of configuration.
    /// </summary>
    /// <param name="assembly">Assembly in which we are searching for the user secrets ID.</param>
    /// <returns>Instance of prepared configuration.</returns>
    public static IConfiguration Create(Assembly assembly)
    {
        ConfigurationBuilder builder = new();
        builder.AddUserSecrets(assembly);

        return builder.Build();
    }
}