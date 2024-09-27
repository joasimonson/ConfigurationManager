using FluentAssertions;
using System.Configuration;

namespace ConfigManager.Tests;

public class ConfigurationHelperTests
{
    private readonly ConfigurationHelper _sut = new("App.config");

    [Fact]
    public void GetSectionSettings_ShouldReturnCorrectValues_WhenSectionExists()
    {
        // Act
        var result = _sut.GetSectionSettings<TestSection>("testSection");

        // Assert
        result.Should().BeOfType<TestSection>().And.BeEquivalentTo(new TestSection
        {
            Setting1 = "Value1",
            Setting2 = "Value2"
        });
    }

    [Fact]
    public void GetSectionSettings_ShouldThrowConfigurationErrorsException_WhenSectionDoesNotExist()
    {
        // Act
        Action act = () => _sut.GetSectionSettings<TestSection>("nonExistentSection");

        // Assert
        act.Should().Throw<ConfigurationErrorsException>()
            .WithMessage("Section 'nonExistentSection' not found.");
    }

    [Fact]
    public void GetSectionSettings_ShouldThrowConfigurationErrorsException_WhenSettingIsMissing()
    {
        // Act
        Action act = () => _sut.GetSectionSettings<IncompleteSection>("testSectionWithMissingSetting");

        // Assert
        act.Should().Throw<ConfigurationErrorsException>()
            .WithMessage("Setting 'Setting2' not found.");
    }

    [Fact]
    public void GetSettings_ShouldReturnAppSettingsValues_WhenAppSettingsExist()
    {
        // Act
        var result = _sut.GetSettings<AppSettings>();

        // Assert
        result.Should().BeOfType<AppSettings>().And.BeEquivalentTo(new AppSettings
        {
            Setting1 = "Value1",
            Setting2 = "Value2"
        });
    }

    [Fact]
    public void GetSettings_ShouldReturnDefaultValues_WhenAppSettingIsMissing()
    {
        // Act
        var result = _sut.GetSettings<AppSettingsWithMissing>();

        // Assert
        result.Should().BeOfType<AppSettingsWithMissing>().And.BeEquivalentTo(new AppSettingsWithMissing
        {
            Setting1 = "Value1",
            Setting2 = "Value2",
            Setting3 = string.Empty
        });
    }

    [Fact]
    public void FindConfig_ShouldReturnCorrectValue_WhenSettingExists()
    {
        // Act
        var result = _sut.FindConfig("Setting1");

        // Assert
        result.Should().Be("Value1");
    }

    [Fact]
    public void FindConfig_ShouldReturnEmptyString_WhenSettingDoesNotExist()
    {
        // Act
        var result = _sut.FindConfig("NonExistentSetting");

        // Assert
        result.Should().BeEmpty();
    }
}

public class TestSection
{
    public string Setting1 { get; set; } = null!;
    public string Setting2 { get; set; } = null!;
}

public class IncompleteSection
{
    public string Setting1 { get; set; } = null!;
    public string Setting2 { get; set; } = null!;
}

public class AppSettings
{
    public string Setting1 { get; set; } = null!;
    public string Setting2 { get; set; } = null!;
}

public class AppSettingsWithMissing
{
    public string Setting1 { get; set; } = null!;
    public string Setting2 { get; set; } = null!;
    public string Setting3 { get; set; } = null!;
}
