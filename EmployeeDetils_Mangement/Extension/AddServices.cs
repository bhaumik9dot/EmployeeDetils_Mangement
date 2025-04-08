using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.Service;
using System.Runtime.CompilerServices;

namespace EmployeeDetils_Mangement.Extension
{
    public static class AddServices
    {
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RouteOptions>(option =>
            {
                option.LowercaseUrls = true;
                option.LowercaseQueryStrings = true;
            });

            services.AddHttpClient();

            services.AddScoped<IEmployeeRepository, EmployeeService>();

        }
    }
}
