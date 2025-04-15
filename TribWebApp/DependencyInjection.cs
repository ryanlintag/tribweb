public static class DependencyInjection
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // Register the AzureConfigService
        services.AddSingleton<IAzureConfigService, AzureConfigService>();

        return services;
    }
}