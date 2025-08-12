namespace Monefy.Domain.Screens;

public class OptionsScreen(IMobileDriver driver)
{
    public IMobileElement AccountsButton =>
        driver.MobileElement("Accounts", MobileBy.Id("com.monefy.app.lite:id/accounts_button"));

    public AccountsScreen Accounts => new AccountsScreen(driver);
}