using intcomex_test_webapp.BLL.Helpers;
using intcomex_test_webapp.BLL.Services;
using intcomex_test_webapp.DAL;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace intcomex_test_webapp
{
    public static class IoC
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactTypeService, ContactTypeService>();
            services.AddScoped<IDapperSqlService, DapperSqlService>();
            services.AddScoped<IRegexUtilitiesService, RegexUtilitiesService>();

            return services;
        }
    }
}
