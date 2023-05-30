using Microsoft.EntityFrameworkCore;

namespace Pbfl.API.Helpers;

public static class DbContextOptionsHelper
{
    public static Action<DbContextOptionsBuilder> GetDbContextOptions(this WebApplicationBuilder webApplicationBuilder)
    {
        const string providerKey = "DatabaseProvider";
        var configuration = webApplicationBuilder.Configuration;
        var dbProvider = configuration.GetValue(providerKey, string.Empty);
        
        return dbProvider switch
        {
            // "MySQL" => options => options.UseMySql(configuration.GetCheckedConnectionString("MySQL"), 
            //     ServerVersion.AutoDetect(configuration.GetCheckedConnectionString("MySQL")),
            //     x => x.MigrationsAssembly("MySqlMigrations")),
            "Postgres" => options => GetNpgsqlBuilder(options, configuration),
            "PostgresDebug" => options => GetNpgsqlBuilder(options, configuration).EnableSensitiveDataLogging(),
            // "SqlServer" => options => options.UseSqlServer(configuration.GetCheckedConnectionString("SqlServer"),
            //     x => x.MigrationsAssembly("SqlServerMigrations")),
            "SqliteDebug" => options => GetSqliteBuilder(options, configuration).EnableSensitiveDataLogging(),
            // Default to Sqlite:
            _ => options => GetSqliteBuilder(options, configuration)
        };
    }

    private static DbContextOptionsBuilder GetNpgsqlBuilder(DbContextOptionsBuilder options, ConfigurationManager configuration)
    {
        return options.UseNpgsql(configuration.GetCheckedConnectionString("Postgres"),
            x => x.MigrationsAssembly("PostgresMigrations"));
    }

    private static DbContextOptionsBuilder GetSqliteBuilder(DbContextOptionsBuilder options, ConfigurationManager configuration)
    {
        return options.UseSqlite(configuration.GetCheckedConnectionString("Sqlite", "Data Source=app.db"),
            x => x.MigrationsAssembly("SqliteMigrations"));
    }

    private static string GetCheckedConnectionString(this IConfiguration configuration, string name, string? fallbackConnStr = null)
    {
        var connStr = configuration.GetConnectionString(name);
        if (string.IsNullOrWhiteSpace(connStr))
        {
            if (!string.IsNullOrWhiteSpace(fallbackConnStr))
            {
                return fallbackConnStr;
            }
            throw new InvalidDataException($"Could not find a connection string called {name}.");
        }
        return connStr;
    }
}