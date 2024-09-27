using System.Configuration;

namespace ConfigManager;

public class CustomCollection : ConfigurationElementCollection
{
    protected override ConfigurationElement CreateNewElement() => new CustomElement();
    protected override object GetElementKey(ConfigurationElement element) => ((CustomElement)element).Key;
}
