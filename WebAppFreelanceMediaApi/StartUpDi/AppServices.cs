using WebAppFreelanceMediaApi.Domain.Services;
using WebAppFreelanceMediaApi.Services.RepServices;

namespace WebAppFreelanceMediaApi.StartUpDi
{
    public static class AppServices
    {
        public static IServiceCollection AddServices
        (this IServiceCollection services)
        {
            services.AddScoped<IAdService, AdService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
