using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Infrastructure.Extensions;
using FluentMigrator.Runner;
using System.Reflection;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.DatabaseType() == DatabaseType.MySql)
            {
                AddDbContextMySqlServer(services, configuration);
                AddFluentMigratorMySql(services, configuration);
            }
            else
            {
                AddDbContextSqlServer(services, configuration);
                AddFluentMigratorSqlServer(services, configuration);
            }

            AddRepositories(services);
        }

        private static void AddDbContextMySqlServer(IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

            services.AddDbContext<MyRecipeBookDbContext>(optionsMyRecipeBookDbContext =>
            {
                optionsMyRecipeBookDbContext.UseMySql(configuration.ConnectionString(), serverVersion);
            });
        }

        private static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyRecipeBookDbContext>(optionsMyRecipeBookDbContext =>
            {
                optionsMyRecipeBookDbContext.UseSqlServer(configuration.ConnectionString());
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }

        private static void AddFluentMigratorMySql(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                    .AddMySql5()
                    .WithGlobalConnectionString(configuration.ConnectionString())
                    .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
            });
        }

        private static void AddFluentMigratorSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.ConnectionString())
                    .ScanIn(Assembly.Load("MyRecipeBook.Infrastructure")).For.All();
            });
        }

    }
}
