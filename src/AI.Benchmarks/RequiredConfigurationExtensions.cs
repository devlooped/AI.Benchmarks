namespace Microsoft.Extensions.Configuration;

public static class RequiredConfigurationExtensions
{
    /// <summary>
    /// Gets a required value from configuration using the given key. If the value is null 
    /// or empty, throws <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value with the given key was null or empty.</exception>
    public static string ǃ(this IConfiguration configuration, string key) => Required(configuration, key);

    /// <summary>
    /// Gets a required value from configuration using the given key. If the value is null 
    /// or empty, throws <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <exception cref="InvalidOperationException">The value with the given key was null or empty.</exception>
    public static string Required(this IConfiguration configuration, string key)
        => configuration[key] ?? throw new InvalidOperationException($"Missing configuration '{key}'.");
}
