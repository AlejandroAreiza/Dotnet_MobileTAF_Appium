namespace Monefy.Domain.Screens;

public class SlideUpScreen(IMobileDriver driver)
{
    public IMobileElement SortingModeButton => driver.MobileElement("Sorting Mode Button",
        MobileBy.Id("com.monefy.app.lite:id/buttonChooseListSortingMode"));

    public IMobileElements GroupingTransactions => driver.MobileElements("Transaction Group",
        MobileBy.Id("com.monefy.app.lite:id/transaction_group_header"));

    public IMobileElements GroupingTransactionsAmounts => driver.MobileElements("Transaction Amounts",
        MobileBy.Id("com.monefy.app.lite:id/textViewTransactionAmount"));

    public IMobileElement DeleteIcon =>
        driver.MobileElement("Delete Icon", MobileBy.Id("com.monefy.app.lite:id/delete"));
}
