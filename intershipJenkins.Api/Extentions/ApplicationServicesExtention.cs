using intershipJenkins.Api.Models;
using intershipJenkins.Api.Services.BookService;

namespace intershipJenkins.Api.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationservices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseConfiguration>(config.GetSection("intershipJenkinsDataBase"));

            services.AddScoped<IBookServices, BookService>();

            return services;
        }
    }
}
