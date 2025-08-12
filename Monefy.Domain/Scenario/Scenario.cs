namespace Monefy.Domain.Scenario;

public class Scenario(IMobileDriver driver)
{
    public LoginScreen LoginScreen => new(driver);
    public RingWidgetScreen RingWidgetScreen => new(driver);
    public SlideUpScreen SlideUpScreen => new(driver);
    public OptionsScreen Options => new(driver);
}