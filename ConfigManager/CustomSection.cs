using System.Configuration;

namespace ConfigManager;

public class CustomSection : ConfigurationSection
{
    [ConfigurationProperty("", IsDefaultCollection = true)]
    private CustomCollection Collection => (CustomCollection)base[""];

    public Dictionary<string, string> Parameters => Collection.Cast<CustomElement>().ToDictionary(x => x.Key, x => x.Value);
}
