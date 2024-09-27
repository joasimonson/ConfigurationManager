# ConfigurationHelper Class

The `ConfigurationHelper` class is a utility designed to simplify working with `.config` files in .NET applications, providing functionality to read app settings and custom configuration sections.
It supports both standard app settings and custom configuration sections using a flexible, strongly-typed approach.

## Key Features

- **Load Configuration Files**: Supports loading the default or a custom configuration file via the constructor.
- **Read App Settings**: The `GetSettings<T>()` method allows the reading of app settings into an instance of any class, mapping the settings based on property names.
- **Read Custom Configuration Sections**: The `GetSectionSettings<T>()` method reads settings from custom configuration sections, mapping them into a specified class type.
- **Flexible Type Conversion**: Automatically converts the configuration values to the correct types based on the properties of the target class.

## Classes and Methods

### `ConfigurationHelper`
- **Constructor**: Accepts an optional configuration file path. If none is provided, it loads the default configuration.
- **`GetSectionSettings<T>(string sectionName)`**: Reads settings from a custom configuration section and maps them to the properties of the specified class type `T`.
- **`GetSettings<T>()`**: Reads app settings and maps them to an instance of type `T`.
- **`FindConfig(string configName)`**: Retrieves a single app setting by key.

### `CustomSection`, `CustomCollection`, and `CustomElement`
- These classes define a custom configuration section schema that allows for key-value pairs to be defined within the config file.
- **`CustomSection`**: Represents a custom section and exposes the configuration as a dictionary of parameters.
- **`CustomCollection`**: Manages a collection of custom elements.
- **`CustomElement`**: Represents a key-value pair within the custom section.

## Usage

To use the `ConfigurationHelper`, instantiate it with or without a configuration file path and call the appropriate method to retrieve settings.
Check `ConfigurationHelperTests` for implementation details.