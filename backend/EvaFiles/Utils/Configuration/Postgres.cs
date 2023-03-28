namespace EvaFiles.Utils.Configuration;

public static class Postgres
{
    private const string DefaultDatabaseHost = "localhost";
    private const string DefaultDatabaseName = "evafiles";
    private const string DefaultDatabaseSchema = "public";
    private const string DefaultDatabaseUserName = "postgres";
    private const string DefaultDatabasePassword = "root";

    private static string GetPostgresHost(this IConfiguration configuration) => 
        Environment.GetEnvironmentVariable("POSTGRESQL_HOST") ?? configuration.GetString("POSTGRESQL_HOST", DefaultDatabaseHost);

    private static string GetPostgresDatabase(this IConfiguration configuration) => 
        Environment.GetEnvironmentVariable("POSTGRESQL_DATABASE") ?? configuration.GetString("POSTGRESQL_DATABASE",DefaultDatabaseName);

    private static string GetPostgresUserName(this IConfiguration configuration) => 
        Environment.GetEnvironmentVariable("POSTGRESQL_USERNAME") ?? configuration.GetString("POSTGRESQL_USERNAME", DefaultDatabaseUserName);

    private static string GetPostgresPassword(this IConfiguration configuration) => 
        Environment.GetEnvironmentVariable("POSTGRESQL_PASSWORD") ?? configuration.GetString("POSTGRESQL_PASSWORD", DefaultDatabasePassword);

    public static string GetPostgresSchema(this IConfiguration configuration) => 
        Environment.GetEnvironmentVariable("POSTGRESQL_SCHEMA") ?? configuration.GetString("POSTGRESQL_SCHEMA", DefaultDatabaseSchema);
        
    public static string GetPostgresConnectionString(this IConfiguration configuration)
    {
        var host = configuration.GetPostgresHost();
        var database = configuration.GetPostgresDatabase();
        var schema = configuration.GetPostgresSchema();
        var username = configuration.GetPostgresUserName();
        var password = configuration.GetPostgresPassword();
        return $"Host={host};Database={database};Username={username};Password={password};search path={schema};Include Error Detail=true";
    }

}