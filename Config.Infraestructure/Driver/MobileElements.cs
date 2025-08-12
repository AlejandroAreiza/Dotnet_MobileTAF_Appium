namespace Config.Infraestructure.Driver;

public class MobileElements : IMobileElements
{
    public string Name { get; set; }
    public By By { get; set; }
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public MobileElements(string name, By by, AppiumDriver driver)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        By = by ?? throw new ArgumentNullException(nameof(by));
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        try
        {
            var wait = new DefaultWait<AppiumDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(5),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            var appiumElements = wait.Until(d =>
            {
                var elements = d.FindElements(by);
                return elements.Count > 0 ? elements : null;
            });

            if (appiumElements == null || !appiumElements.Any())
            {
                throw new TimeoutException($"No elements '{name}' found using locator: {by.Mechanism}");
            }

            Items = appiumElements.Select(e => new MobileElement(name, By, driver, e)).ToList();

            Logger.Info($"{Items.Count} '{name}' elements found");
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Failed to find elements '{name}' with {by}");
            throw;
        }
    }

    public int Count => Items.Count;

    public MobileElement this[int index] => Items[index];
    public IList<MobileElement> Items { get; }
}
