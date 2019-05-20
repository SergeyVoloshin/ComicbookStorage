
namespace ComicbookStorage.Infrastructure.DI
{
    using System.Reflection;
    using Application.Services;
    using Application.Services.Configuration;
    using AutoMapper;
    using Domain.DataAccess;
    using Domain.Services;
    using EF;
    using EF.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NetCore.AutoRegisterDi;

    public static class ProductionModeStartup
    {
        public static void AddProductionDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            Assembly applicationServiceAssembly = Assembly.GetAssembly(typeof(ComicbookService));
            Assembly domainServiceAssembly = Assembly.GetAssembly(typeof(ComicbookManager));
            Assembly repositoryAssembly = Assembly.GetAssembly(typeof(ComicbookRepository));

            services.AddAutoMapper(applicationServiceAssembly);

            services.RegisterAssemblyPublicNonGenericClasses(
                    applicationServiceAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(
                    domainServiceAssembly)
                .Where(c => c.Name.EndsWith("Manager"))
                .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(
                    repositoryAssembly)
                .Where(c => c.Name.EndsWith("Repository"))
                .AsPublicImplementedInterfaces();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            AppConfiguration appConfiguration = new AppConfiguration();
            configuration.GetSection("AppConfiguration").Bind(appConfiguration);
            services.AddSingleton<IAppConfiguration>(appConfiguration);

            services.AddDbContext<ComicbookStorageContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

    }
}
