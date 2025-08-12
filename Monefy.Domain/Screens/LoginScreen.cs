namespace Monefy.Domain.Screens;

public class LoginScreen(IMobileDriver driver)
{
    public IMobileElement GetStartedButton =>
        driver.MobileElement("Start Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));

    public IMobileElement AmazingButton =>
        driver.MobileElement("Amazing Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));

    public IMobileElement YesPleaseButton =>
        driver.MobileElement("Yes Please Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));

    public IMobileElement AllowButton => driver.MobileElement("Allow Button",
        MobileBy.Id("com.android.permissioncontroller:id/permission_allow_button"));

    public IMobileElement DontAllowButton => driver.MobileElement("Dont Allow Button",
        MobileBy.Id("com.android.permissioncontroller:id/permission_deny_button"));

    public IMobileElement ImReadyButton =>
        driver.MobileElement("Im Ready Button", MobileBy.Id("com.monefy.app.lite:id/buttonContinue"));

    public IMobileElement CloseButton =>
        driver.MobileElement("Close Button", MobileBy.Id("com.monefy.app.lite:id/buttonClose"));

    public IMobileElement ExpenseToolTip => driver.MobileElement("Expense Tool Tip",
        MobileBy.AndroidUIAutomator("new UiSelector().text(\"Tap to add a new expense record\")"));

    public IMobileElement IconCategoryToolTip => driver.MobileElement("Category Tool Tip",
        MobileBy.AndroidUIAutomator("new UiSelector().text(\"Or tap the category icon to add a record faster\")"));

    public IMobileElement TransferToolTip => driver.MobileElement("Transfer Tool Tip",
        MobileBy.AndroidUIAutomator(
            "new UiSelector().text(\"Tap the 'Transfer' button to move money between accounts\")"));

    public IMobileElement CurrencyToolTip => driver.MobileElement("Currency Tool Tip",
        By.XPath("//android.widget.TextView[@text=\"Main currency can be changed here\"]"));

    public IMobileElement OptionsButton =>
        driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/overflow"));

    public IMobileElement OptionsPanel =>
        driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/right_drawer"));


    public void CompleteOnboardingFlow()
    {
        if (GetStartedButton.IsDisplayed()) GetStartedButton.Click();
        if (AmazingButton.IsDisplayed()) AmazingButton.Click();
        if (YesPleaseButton.IsDisplayed()) YesPleaseButton.Click();
        if (AllowButton.IsDisplayed()) AllowButton.Click();
        if (ImReadyButton.IsDisplayed()) ImReadyButton.Click();
        if (CloseButton.IsDisplayed()) CloseButton.Click();
        if (ExpenseToolTip.IsDisplayed()) ExpenseToolTip.Click();
        if (IconCategoryToolTip.IsDisplayed()) IconCategoryToolTip.Click();
        if (TransferToolTip.IsDisplayed()) TransferToolTip.Click();
        if (CurrencyToolTip.IsDisplayed())
        {
            CurrencyToolTip.Click();
            OptionsButton.Click();
        }
    }
}