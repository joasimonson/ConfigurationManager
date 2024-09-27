using System.Configuration;
using System.Reflection;

namespace ConfigManager;

public class ConfigurationHelper
{
    private readonly Configuration _config;

    public ConfigurationHelper(string? configFile = null)
    {
        if (configFile is null)
        {
            _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        else
        {
            _config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
            {
                ExeConfigFilename = configFile,
            }, ConfigurationUserLevel.None);
        }
    }
    public T GetSectionSettings<T>(string sectionName) where T : class, new()
    {
        if (_config.GetSection(sectionName) is not CustomSection section)
            throw new ConfigurationErrorsException($"Section '{sectionName}' not found.");

        var obj = Activator.CreateInstance<T>();
        var accessLevel = BindingFlags.Public | BindingFlags.Instance;
        var properties = obj.GetType().GetProperties(accessLevel).ToArray();

        foreach (var property in properties)
        {
            if (!section.Parameters.TryGetValue(property.Name, out string? value))
                throw new ConfigurationErrorsException($"Setting '{property.Name}' not found.");

            property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
        }

        return obj;
    }
    public T GetSettings<T>() where T : class, new()
    {
        var obj = Activator.CreateInstance<T>();
        var accessLevel = BindingFlags.Public | BindingFlags.Instance;
        var properties = obj.GetType().GetProperties(accessLevel).ToArray();

        foreach (var property in properties)
        {
            var setting = FindConfig(property.Name);

            if (setting is null)
                continue;

            property.SetValue(obj, Convert.ChangeType(setting, property.PropertyType));
        }

        return obj;
    }
    public string FindConfig(string configName) => _config.AppSettings.Settings?[configName]?.Value ?? string.Empty;
}