namespace Monefy.Domain.Screens;

public class RingWidgetScreen(IMobileDriver driver)
{
    public IMobileElement LeftMenuButton =>
        driver.MobileElement("Left Menu Button", MobileBy.Id("com.monefy.app.lite:id/leftLinesImageView"));

    public IMobileElement RightMenuButton =>
        driver.MobileElement("Right Menu Button", MobileBy.Id("com.monefy.app.lite:id/rightLinesImageView"));

    public IMobileElement BalanceAmount =>
        driver.MobileElement("Balance Amount Container", MobileBy.Id("com.monefy.app.lite:id/balance_amount"));

    public IMobileElement ExpenseButton =>
        driver.MobileElement("Expense Button", MobileBy.Id("com.monefy.app.lite:id/expense_button"));

    public IMobileElement ExpenseButtonTitle =>
        driver.MobileElement("Expense Button Title", MobileBy.Id("com.monefy.app.lite:id/expense_button_title"));

    public IMobileElement IncomeButton =>
        driver.MobileElement("Income Button", MobileBy.Id("com.monefy.app.lite:id/income_button"));

    public IMobileElement IncomeButtonTitle =>
        driver.MobileElement("Income Button Title", MobileBy.Id("com.monefy.app.lite:id/income_button_title"));

    public IMobileElement AddExpenseInput =>
        driver.MobileElement("Add Expense Input", By.XPath("//*[contains(@text, 'Tap to add a new expense record')]"));


    public IMobileElement LeftMenuButtonAlternate =>
        driver.MobileElement("Left Menu Button Alternate",
            MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.ImageView\").instance(0)"));


    public IMobileElement IncomeButtonAlternate =>
        driver.MobileElement("Income Button Alternate",
            MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"com.monefy.app.lite:id/income_button\")"));

    public IMobileElement SnackBarCancelButton => driver.MobileElement("Snack Bar Cancel Button",
        MobileBy.Id("com.monefy.app.lite:id/snackbar_action"));

    public IMobileElement SnackBar =>
        driver.MobileElement("Snack Bar Cancel Button", MobileBy.Id("com.monefy.app.lite:id/snackbar_text"));

    public IMobileElement TotalIncomeAmount => driver.MobileElement("Total Income Amount",
        MobileBy.Id("com.monefy.app.lite:id/income_amount_text"));

    public IMobileElement TotalExpenseAmount => driver.MobileElement("Total Expense Amount",
        MobileBy.Id("com.monefy.app.lite:id/expense_amount_text"));

    public IMobileElement OptionsButton =>
        driver.MobileElement("Options Button", MobileBy.Id("com.monefy.app.lite:id/overflow"));

    IMobileElement MonefyPremiumToolTip => driver.MobileElement("Transfer Tool Tip",
        MobileBy.AndroidUIAutomator(
            "new UiSelector().text(\"Recurring records are now available in Monefy Premium!\")"));

    private AppiumElement Recycler => driver.Driver.FindElement(MobileBy.Id("com.monefy.app.lite:id/pts_main"));

    public IMobileElement MonthElement => new MobileElement("MonthItem",
        MobileBy.ClassName("android.widget.TextView"),
        driver.Driver,
        Recycler.FindElement(MobileBy.ClassName("android.widget.TextView"))
    );


    public void AddIncome(int amount, IncomeCategory incomeCategory, string? note = null)
    {
        IncomeButton.Click();
        var incomeScreen = new IncomeScreen(driver);
        if (MonefyPremiumToolTip.IsDisplayed()) MonefyPremiumToolTip.Click();
        incomeScreen.AddIncome(amount, incomeCategory, note);
        SnackBar.WaitUntilItDissapear();
    }

    public void AddExpense(int amount, ExpenseCategory incomeCategory, string? note = null, string date = null)
    {
        ExpenseButton.Click();
        var expenseScreen = new ExpenseScreen(driver);
        if (MonefyPremiumToolTip.IsDisplayed()) MonefyPremiumToolTip.Click();
        expenseScreen.AddExpense(amount, incomeCategory, note, date);
        SnackBar.WaitUntilItDissapear();
    }
}