using Commons.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class AddApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();
            return services;
        }
    }
}
