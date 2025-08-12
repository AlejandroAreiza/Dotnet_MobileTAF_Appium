namespace Monefy.Domain.Screens;

public class AccountsScreen(IMobileDriver driver)
{
    public IMobileElements Accounts => driver.MobileElements("Accounts", MobileBy.Id("com.monefy.app.lite:id/relativeLayoutManageCategoriesListItem"));

    public IMobileElement AccountsPanel => driver.MobileElement("Accounts", MobileBy.Id("com.monefy.app.lite:id/accounts_panel"));

    public IMobileElement DeleteButton => driver.MobileElement("Delete Button", MobileBy.Id("com.monefy.app.lite:id/delete"));

    public IMobileElement OkButton => driver.MobileElement("Delete Button", MobileBy.XPath("//android.widget.Button[@text='OK']"));

    public void DeleteAccount(AccountCategory accountName)
    {
        AccountsPanel.Click();
        Accounts.Items.First().Click();
        DeleteButton.Click();
        OkButton.Click();
    }
}