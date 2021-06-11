using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using VanakIndustry.DataAccess.Contexts;
using VanakIndustry.Web.Configuration.Constants;

namespace VanakIndustry.Web.Initializers
{
    public static class DatabaseConfiguration
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //Logger.Info("Configuring the database and running migrations");

            var connectionString = configuration.GetValue<string>(SettingsKeys.ConnectionString);

            services.AddScoped(x => new VanakIndustryContext(connectionString));

            /*var migrator = new MysqlMigrator<MySqlConnection>(new MigratorOptions(connectionString, typeof(VanakIndustryContext).Assembly));

            migrator.Migrate();*/

            return services;
        }
    }
}
