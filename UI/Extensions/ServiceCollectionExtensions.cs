using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void Scoped(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IArticleRepository, EfArticleRepository>();
            services.AddScoped<ITopicRepository, EfTopicRepository>();

            services.AddScoped<ITopicService, TopicManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IArticleService, ArticleManager>();
        }
    }
}
