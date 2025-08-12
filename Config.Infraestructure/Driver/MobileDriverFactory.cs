namespace Config.Infraestructure.Driver;

public class MobileDriverFactory
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public AppiumDriver CreateDriver(DesiredCapabilities config)
    {
        if (config == null)
        {
            throw new ArgumentNullException(nameof(config), "AppiumConfig cannot be null.");
        }

        Logger.Info($"Creating Appium driver for platform: {config.PlatformName}");
        var options = new AppiumOptions
        {
            PlatformName = config.PlatformName.ToString(),
            DeviceName = config.DeviceName,
            AutomationName = config.AutomationName
        };

        if (!string.IsNullOrEmpty(config.AppPackage))
        {
            options.AddAdditionalAppiumOption("appPackage", config.AppPackage);
        }

        if (!string.IsNullOrEmpty(config.AppActivity))
        {
            options.AddAdditionalAppiumOption("appActivity", config.AppActivity);
        }

        if (config.NoReset)
        {
            options.AddAdditionalAppiumOption("noReset", config.NoReset);
        }

        if (config.NewCommandTimeout > 0)
        {
            options.AddAdditionalAppiumOption("newCommandTimeout", config.NewCommandTimeout);
        }

        options.AddAdditionalAppiumOption("udid", config.Udid);

        try
        {
            AppiumDriver driver = config.PlatformName switch
            {
                DTOs.PlatformType.Android => new AndroidDriver(new Uri("http://localhost:4723"), options),
                DTOs.PlatformType.iOS => new IOSDriver(new Uri("http://localhost:4723/wd/hub"), options),
                _ => throw new NotSupportedException($"Platform {config.PlatformName} is not supported.")
            };
            Logger.Info($"{config.PlatformName} driver created");
            return driver;
        }
        catch (Exception ex)
        {
            Logger.Error($"{config.PlatformName} driver failed to be created with message: {ex.Message}");
            throw new ArgumentException(
                $"{config.PlatformName} driver failed to be created with message: {ex.Message}");
        }
    }
}