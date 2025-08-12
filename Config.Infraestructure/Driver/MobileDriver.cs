namespace Config.Infraestructure.Driver;

public class MobileDriver(TestLaunchConfig testLaunchConfigData) : IMobileDriver
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    private readonly AppManager _appManager = new AppManager(testLaunchConfigData.AppConfig);

    public AppiumDriver Driver { get; } =
        new MobileDriverFactory().CreateDriver(testLaunchConfigData.DesiredCapabilities);

    public IMobileElement MobileElement(string name, By by) => new MobileElement(name, by, Driver);
    public IMobileElements MobileElements(string name, By by) => new MobileElements(name, by, Driver);

    public void InstallApp()
    {
        try
        {
            _appManager.Install();
            Logger.Info("App Installed");
        }
        catch (Exception ex)
        {
            Logger.Error($"Installing App failed with message {ex.Message}");
        }
    }

    public void Uninstall()
    {
        try
        {
            _appManager.Uninstall();
            Logger.Info("App Uninstalled");
        }
        catch (Exception ex)
        {
            Logger.Error($"Uninstalling App failed with message {ex.Message}");
        }
    }

    public void TerminateApp()
    {
        try
        {
            Driver.TerminateApp(_appManager.AppConfig.AppName);
            Logger.Info("Closing app...");
        }
        catch (Exception ex)
        {
            Logger.Error($"Error Closing app with message: {ex.Message}");
        }
    }

    public void Dispose()
    {
        Logger.Info("Disposing driver...");
        Driver.Quit();
        Driver.Dispose();
    }

    public void GoToScreen(string screenName)
    {
        switch (testLaunchConfigData.DesiredCapabilities.PlatformName)
        {
            case DTOs.PlatformType.Android:
                GoToAndroidScreen(screenName);
                break;
            case DTOs.PlatformType.iOS:
                throw new NotImplementedException("iOS direct screen navigation not supported yet");
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GoToAndroidScreen(string activityName)
    {
        var fullComponent = $"{testLaunchConfigData.DesiredCapabilities.AppPackage}/{activityName}";
        var args = new Dictionary<string, object>
        {
            ["command"] = "am",
            ["args"] = new List<string> { "start", "-n", fullComponent }
        };
        try
        {
            Driver.ExecuteScript("mobile: shell", args);
            Logger.Info($"{args["appActivity"]} Screen loaded");
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed loading Screen {args["appActivity"]} with message: {ex.Message}");
            throw new ArgumentException($"Failed loading Screen {args["appActivity"]} with message: {ex.Message}");
        }
    }
}
