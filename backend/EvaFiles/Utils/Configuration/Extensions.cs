namespace EvaFiles.Utils.Configuration;

public static class Extensions
{
    public static int GetInt(this IConfiguration configuration, string key, int defaultValue)
    {
        var setting = configuration.GetString(key, defaultValue.ToString());
        var success = int.TryParse(setting, out var value);
        if (!success)
        {
            throw new Exception($"Could not parse integer configuration setting '{key}' from value: '{setting}'");
        }
        return value;
    }

    public static bool GetBool(this IConfiguration configuration, string key, bool defaultValue)
    {
        var setting = configuration.GetString(key, defaultValue.ToString());
        var success = bool.TryParse(setting, out var value);
        if (!success)
        {
            throw new Exception($"Could not parse bool configuration setting '{key}' from value: '{setting}'");
        }
        return value;
    }

    public static string GetString(this IConfiguration configuration, string key, string defaultValue) => 
        configuration[key] ?? defaultValue;

}