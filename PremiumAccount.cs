public class PremiumAccount : Account
{
    // DATA
    new private int currentActiveRentals = 0;

    // CONSTRUCTORS
    public PremiumAccount(string accountType, string username, string password)
    {
        this.accountType = accountType;
        this.username = username;
        this.password = password;
    }
    public PremiumAccount()
    {

    }
    // FUNCTIONS
    public override int GetCurrentActiveRentals()
    {
        return currentActiveRentals;
    }
    public override int GetRentalLimit()
    {
        return 3;
    }
    public override void IncreaseActiveRentalAmount()
    {
        currentActiveRentals++;
    }
    public override void DecreaseActiveRentalAmount()
    {
        currentActiveRentals--;
    }
}