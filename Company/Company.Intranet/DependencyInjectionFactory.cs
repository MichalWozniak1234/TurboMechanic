using Company.Interfaces.Services;
using Company.Services.Services;

namespace Company.Intranet
{
    public static class DependencyInjectionFactory
    {
        public static void Resolve(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDamageCategoryService, DamageCategoryService>();
        }
    }
}