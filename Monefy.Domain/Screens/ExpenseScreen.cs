namespace Monefy.Domain.Screens;

public class ExpenseScreen(IMobileDriver driver)
{
    public IMobileElement AmounInput =>
        driver.MobileElement("Amount Input", MobileBy.Id("com.monefy.app.lite:id/amount_text"));

    public IMobileElement TextViewNote =>
        driver.MobileElement("Text View Note", MobileBy.Id("com.monefy.app.lite:id/textViewNote"));

    public IMobileElement KeyPad(string number) => driver.MobileElement($"Key Pad {number}",
        MobileBy.Id($"com.monefy.app.lite:id/buttonKeyboard{number}"));

    public IMobileElement ChooseCategoryButton => driver.MobileElement("Choose Category Button",
        MobileBy.Id("com.monefy.app.lite:id/keyboard_action_button"));

    public IMobileElement ExpenseCategory(ExpenseCategory expenseCategory) =>
        driver.MobileElement($"Expense {expenseCategory} Category",
            MobileBy.AndroidUIAutomator($"new UiSelector().text(\"{expenseCategory}\")"));

    public IMobileElement Calendar =>
        driver.MobileElement("Date View", MobileBy.Id("com.monefy.app.lite:id/textViewDate"));

    public IMobileElement EditCalendarDate =>
        driver.MobileElement("Edit Calendar Date", MobileBy.Id("com.monefy.app.lite:id/mtrl_picker_header_toggle"));

    public IMobileElement CalendarDateInput =>
        driver.MobileElement("Calendar Date Input", MobileBy.ClassName("android.widget.EditText"));

    public IMobileElement ConfirmButton =>
        driver.MobileElement("Confirm Button", MobileBy.Id("com.monefy.app.lite:id/confirm_button"));

    public void AddExpense(int amount, ExpenseCategory expenseCategory, string? note = null, string? date = null)
    {
        SendAmount(amount);
        if (!string.IsNullOrEmpty(note))
        {
            TextViewNote.Type(note);
        }

        if (date != null)
        {
            ChooseDate(date);
        }

        ChooseCategoryButton.Click();
        ExpenseCategory(expenseCategory).Click();
    }

    private void SendAmount(int amount)
    {
        foreach (var n in amount.ToString())
        {
            KeyPad(n.ToString()).Click();
        }
    }

    private void ChooseDate(string date)
    {
        Calendar.Click();
        EditCalendarDate.Click();
        CalendarDateInput.Type(date);
        ConfirmButton.Click();
    }
}
