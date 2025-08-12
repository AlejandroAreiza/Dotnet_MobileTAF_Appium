namespace Monefy.Tests.E2E;

[TestFixture]
[AllureNUnit]
[AllureFeature("Monefy Tests")]
public class MonefyE2ETests : BaseTest
{
    [AllureFeature("Regression")]
    [TestCase(5000, 2500, 2500, IncomeCategory.Salary, ExpenseCategory.House, ExpenseCategory.Car, "Salary Income",
        "House", "Car", "en-US")]
    public void WhenUserAddsValidIncomesAndExpenses_Then_TheBalanceShouldBeReflecteProperl(
        int income,
        int expense1,
        int expense2,
        IncomeCategory incomeCategory,
        ExpenseCategory expenseCategory1,
        ExpenseCategory expenseCategory2,
        string incomeNote,
        string expenseNote1,
        string expenseNote2,
        string culture)
    {
        var expenses = expense1 + expense2;
        var balance = income - expenses;

        Scenario.RingWidgetScreen.AddIncome(income, incomeCategory, incomeNote);
        Scenario.RingWidgetScreen.AddExpense(expense1, expenseCategory1, expenseNote1);
        Scenario.RingWidgetScreen.AddExpense(expense2, expenseCategory2, expenseNote2);

        Scenario.RingWidgetScreen.TotalIncomeAmount.Text
            .Should().BeEquivalentTo(income.ToString().FormatCurrency(culture));

        Scenario.RingWidgetScreen.TotalExpenseAmount.Text
            .Should().BeEquivalentTo(expenses.ToString().FormatCurrency(culture));

        Scenario.RingWidgetScreen.BalanceAmount.Text
            .Should().Contain(balance.ToString().FormatCurrency(culture));
    }

    [AllureFeature("Regression")]
    [TestCase(99999999, 10, IncomeCategory.Salary, ExpenseCategory.Bills, "Salary", "Bills", "en-US")]
    public void WhenUserInitiatesDeleteTransaction_AndCancels_ThenDeletionIsAborted(
        int expense,
        int income,
        ExpenseCategory expenseCategory,
        IncomeCategory incomeCategory,
        string incomeNote,
        string expenseNote,
        string culture)
    {
        var balance = income - expense;
        Scenario.RingWidgetScreen.AddExpense(expense, expenseCategory, expenseNote);
        Scenario.RingWidgetScreen.AddIncome(income, incomeCategory, incomeNote);
        Scenario.RingWidgetScreen.LeftMenuButton.Click();
        Scenario.RingWidgetScreen.BalanceAmount.Text
            .Should().Contain(balance.ToString().FormatCurrency(culture));
        Scenario.SlideUpScreen.GroupingTransactions.Items.First().Click();
        Scenario.SlideUpScreen.GroupingTransactionsAmounts.Items.ToList().ForEach(x => x.ClickAndHold());
        Scenario.SlideUpScreen.DeleteIcon.IsDisplayed().Should().BeTrue();
        Scenario.SlideUpScreen.SortingModeButton.Click();
        Scenario.RingWidgetScreen.LeftMenuButton.Click();
        Scenario.SlideUpScreen.DeleteIcon.IsDisplayed().Should().BeFalse();
    }

    [AllureFeature("Regression")]
    [TestCase(500, ExpenseCategory.Bills, "Bills", "01/01/2020", AccountCategory.Cash)]
    public void WhenAccountIsDeleted_ThenAccountDate_ShouldNotBeReflectedOnOtherAccounts(
        int expense,
        ExpenseCategory expenseCategory,
        string expenseNote,
        string date,
        AccountCategory accountName
    )
    {
        var formattedDateByMonth =
            DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMMM yyyy");
        var todayFormattedByMonth = DateTime.Today.ToString("MMMM yyyy");
        Scenario.RingWidgetScreen.AddExpense(expense, expenseCategory, expenseNote, date);
        Scenario.RingWidgetScreen.MonthElement.Text.Should().Contain(formattedDateByMonth);
        Scenario.RingWidgetScreen.OptionsButton.Click();
        Scenario.Options.Accounts.DeleteAccount(accountName);
        Scenario.RingWidgetScreen.SnackBar.WaitUntilItDissapear();
        Scenario.RingWidgetScreen.MonthElement.Text.Should().Contain(todayFormattedByMonth);
    }
}