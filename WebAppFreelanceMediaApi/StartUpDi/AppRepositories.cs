using WebAppFreelanceMediaApi.Data.Repositories;
using WebAppFreelanceMediaApi.Domain.Repositories;

namespace WebAppFreelanceMediaApi.StartUpDi
{
    public static class AppRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
