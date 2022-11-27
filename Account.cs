public class Account
{
    // DATA
    protected string accountType;
    protected string username;
    protected string password;
    protected int currentActiveRentals = 0;
    protected int balance = 500;

    DisplayManager display = new DisplayManager();

    // CONSTRUCTORS
    public Account (string accountType, string username, string password)
    {
        this.accountType = accountType;
        this.username = username;
        this.password = password;
    }
    public Account()
    {

    }

    // FUNCTIONS
    public string GetAccountType() 
    {
        return accountType;
    }
    public string GetAccountName()
    {
        return username;
    }
    public string GetAccountPassword()
    {
        return password;
    }
    public void SetAccountType(string accountType)
    {
        this.accountType = accountType;
    }
    public void SetAccountName(string username)
    {
        this.username = username;
    }
    public void SetAccountPassword(string password)
    {
        this.password = password;
    }
    public virtual int GetRentalLimit()
    {
        return 1;
    }
    public virtual int GetCurrentActiveRentals()
    {
        return currentActiveRentals;
    }
    public virtual int GetBalance()
    {
        return balance;
    }
    public void DeductVehicleCosts(Vehicle vehicle, int vehicleRentalDuration)
    {
        if (vehicle.GetFinalRentalPrice(vehicleRentalDuration) + vehicle.GetDepositPrice() <= balance)
        {
            balance -= vehicle.GetFinalRentalPrice(vehicleRentalDuration);
            balance -= vehicle.GetDepositPrice();
        }
    }
    public void ReturnDepositPrice(Vehicle vehicle)
    {
        balance += vehicle.GetDepositPrice();
    }
    public virtual void IncreaseActiveRentalAmount()
    {
        currentActiveRentals++;
    }
    public virtual void DecreaseActiveRentalAmount()
    {
        currentActiveRentals--;
    }
    public bool WithdrawFunds(int withdrawalAmount)
    {
        if (withdrawalAmount <= balance && withdrawalAmount > 0)
        {
            balance -= withdrawalAmount;
            Console.WriteLine();
            display.SuccessMessage("WITHDRAW OF £" + withdrawalAmount + " SUCCESSFUL!");
            display.UserInfo("REMAINING BALANCE: £" + balance);
            Console.WriteLine();
            return true;
        }
        else if (withdrawalAmount <= 0)
        {
            display.ErrorMessage("INVALID WITHDRAWAL AMOUNT ENTERED!");
            return false;
        }
        else if (withdrawalAmount > balance)
        {
            display.ErrorMessage("INSUFFICIENT FUNDS!");
            return false;
        }
        else
        {
            display.ErrorMessage("ERROR REQUESTING WITHDRAWAL OF FUNDS!");
            return false;
        }
    }
    public bool DepositFunds(int depositAmount)
    {
        if (depositAmount > 0 && depositAmount < 100000)
        {
            balance += depositAmount;
            Console.WriteLine();
            display.SuccessMessage("DEPOSIT OF £" + depositAmount + " SUCCESSFUL!");
            display.UserInfo("NEW BALANCE: £" + balance);
            Console.WriteLine();
            return true;
        }
        else if (depositAmount <= 0)
        {
            display.ErrorMessage("ENTER AN AMOUNT ABOVE £0 TO DPEOSIT!");
            return false;
        }
        else
        {
            display.ErrorMessage("ERROR DEPOSITING FUNDS!");
            return false;
        }
    }

    public void ShowUserBalanceAndRentalInfo()
    {
        Console.WriteLine();
        display.UserInfo("Current balance: £" + GetBalance());
        display.UserInfo("Current rental limit: " + GetRentalLimit());
        display.UserInfo("Current active rentals: " + GetCurrentActiveRentals());
        Console.WriteLine();
    }
    public void ShowActiveRentals()
    {
        Console.WriteLine();
        display.UserInfo("Current active rentals: " + GetCurrentActiveRentals());
        Console.WriteLine();
    }
    public void ShowRemainingBalance()
    {
        display.UserInfo("REMAINING BALANCE: £" + Convert.ToString(GetBalance()));
        Console.WriteLine();
    }
    public void DisplayBalance(int balance)
    {
        display.UserInfo("CURRENT BALANCE: £" + Convert.ToString(balance));
        Console.WriteLine();
    }
    public bool isRentalLimitReached()
    {
        if (GetRentalLimit() > GetCurrentActiveRentals())
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}