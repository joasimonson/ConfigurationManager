using System.Configuration;

namespace ConfigManager;

public class CustomElement : ConfigurationElement
{
    [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
    public string Key => (string)base["key"];

    [ConfigurationProperty("value", IsRequired = true)]
    public string Value => (string)base["value"];
}