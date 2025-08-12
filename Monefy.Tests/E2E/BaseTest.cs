namespace Monefy.Tests.E2E;

public class BaseTest
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    private static readonly string SolutionRoot =
        Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName ??
        Directory.GetCurrentDirectory();

    private TestLaunchConfig _testLaunchConfigData;
    private AppManager _appManager;
    private IMobileDriver _mobileDriver;
    public Scenario Scenario;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        var logsFolder = Path.Combine(SolutionRoot, "TestLogs");
        var logFileName = $"{TestContext.CurrentContext.Test.DisplayName}_{DateTime.Now:yyyyMMdd_HHmmss}";
        LoggerManager.Setup(logsFolder, logFileName);
        _testLaunchConfigData =
            DataProvider.GetData<TestLaunchConfig>(Path.Combine(SolutionRoot, "Monefy.Tests/Config",
                "TestLaunchConfigData.json"));
        _appManager = new AppManager(_testLaunchConfigData.AppConfig);
        _appManager.Install();
    }

    [SetUp]
    public void Setup()
    {
        Logger.Info("=========================================================");
        _mobileDriver = new MobileDriver(_testLaunchConfigData);
        Logger.Info($"Test Case {TestContext.CurrentContext.Test.Name} started");
        Scenario = new Scenario(_mobileDriver);
        Scenario.LoginScreen.CompleteOnboardingFlow();
    }

    [TearDown]
    public void TearDown()
    {
        Logger.Info($"Test Case: {TestContext.CurrentContext.Result.Outcome.Status}");
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            Logger.Error($"Test Case {TestStatus.Failed} with message {TestContext.CurrentContext.Result.Message} ");
        }

        Logger.Info($"Test Case finished");
        _mobileDriver.TerminateApp();
        _mobileDriver?.Dispose();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        _appManager.Uninstall();
        Logger.Info("=========================================================");
        LoggerManager.Shutdown();
    }
}