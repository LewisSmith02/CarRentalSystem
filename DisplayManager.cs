public class DisplayManager
{
    // DATA
    private int optionNumber = 0;
    private bool isDevCheckResult = false;

    // CONSTRUCTOR
    public DisplayManager()
    {

    }

    public void GetIsDevCheckResult(bool isDevCheck)
    {
        isDevCheckResult = isDevCheck;
    }
    public void ApplicationLaunchMessage()
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("[Vehicle Rental System]");
        Console.WriteLine();

        Console.ForegroundColor = before;
    }

    // MENU FORMATTERS
    public void ShowMenu()
    {
        ViewingMessage("MENU");
        OptionColour("LOGOUT");
        OptionColour("HELP");
        OptionColour("RENT VEHICLES");
        OptionColour("RETURN VEHICLES");
        OptionColour("DEPOSIT/WITHDRAW FUNDS");
        Console.WriteLine();
        InputInfoMessage("Select an above menu option to proceed:");
        SetOptionNumber(0);
    }
    public void ShowAdminMenu()
    {
        ViewingMessage("MENU");
        OptionColour("LOGOUT");
        OptionColour("HELP");
        OptionColour("RENT VEHICLES");
        OptionColour("RETURN VEHICLES");
        OptionColour("DEPOSIT/WITHDRAW FUNDS");
        OptionColour("ADMIN PANEL");
        Console.WriteLine();
        InputInfoMessage("Select an above menu option to proceed:");
        SetOptionNumber(0);
    }
    public void ShowDepositWithdrawalMenu()
    {
        Console.WriteLine();
        ViewingMessage("DEPOSIT / WITHDRAW FUNDS");
        OptionColour("DEPOSIT FUNDS");
        OptionColour("WITHDRAW FUNDS");
        SetOptionNumber(0);
        Console.WriteLine();
        InputInfoMessage("Select an option above to proceed: ");
    }
    public void LoggedInSuccessfully(string userInputUsername, string accountType)
    {
        Console.WriteLine();
        SuccessMessage("LOGGED IN AS: (" + accountType + " Account) " + userInputUsername);
        Console.WriteLine();
    }

    // USER MESSAGE FORMATTERS
    public void UserInfo(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("[USER INFO]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }

    public void ViewingMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[VIEWING]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void ErrorMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[ERROR]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void HelpMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[HELP]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void InputInfoMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[INPUT INFO]: ");

        Console.ForegroundColor = before;
        Console.Write(message + " ");

        Console.ForegroundColor = before;
    }
    public void SuccessMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[SUCCESS]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void SelectedMessage(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[SELECTED]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }

    public void OptionColour(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[" + optionNumber++ + "]: ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void FormatListIndex(string message)
    {
        var before = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[" + optionNumber++ + "] ");

        Console.ForegroundColor = before;
        Console.WriteLine(message);

        Console.ForegroundColor = before;
    }
    public void DevInfoMessage(string message)
    {
        if (isDevCheckResult == true)
        {
            var before = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n[DEV INFO]: ");

            Console.ForegroundColor = before;
            Console.Write(message);

            Console.ForegroundColor = before;
        }
        else {}
    }
    public void DevSpacing()
    {
        Console.WriteLine();
    }
    public void DevDataInfo(string message)
    {
        if (isDevCheckResult == true)
        {
            var before = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("["+message+"] ");
            Console.Write(": ");

            Console.ForegroundColor = before;
        }
        else { }
    }

    // RENTAL & RETURN DISPLAY FORMATTERS
    public void ShowSelectedVehicleAndBalanceChanges(string chosenReturnVehicleMakeAndModel, string chosenReturnVehicleDeposit)
    {
        SelectedMessage(chosenReturnVehicleMakeAndModel + " | Balance: +£" + chosenReturnVehicleDeposit + " (Deposit Return)");
    }
    public void DisplayChosenVehicleFinalPriceAndDeposit(Vehicle chosenRentalVehicle, int vehicleRentalDuration)
    {
        string chosenRentalVehicleMakeAndModel = chosenRentalVehicle.GetVehicleMakeAndModel();
        string chosenRentalVehiclePrice = Convert.ToString(chosenRentalVehicle.GetRentalPrice());
        string chosenRentalVehicleFinalPrice = Convert.ToString(chosenRentalVehicle.GetFinalRentalPrice(vehicleRentalDuration));
        string chosenRentalVehicleDeposit = Convert.ToString(chosenRentalVehicle.GetDepositPrice());
        int totalPrice = chosenRentalVehicle.GetFinalRentalPrice(vehicleRentalDuration) + chosenRentalVehicle.GetDepositPrice();

        SelectedMessage(chosenRentalVehicleMakeAndModel + " | Daily Rental Price: £" + chosenRentalVehiclePrice + " | Deposit Price: £" + chosenRentalVehicleDeposit + " | Total Rental Price: £" + chosenRentalVehicleFinalPrice + " | Final Price: £" + totalPrice);
    }
    public void DisplayChosenVehicleRentalPriceAndDeposit(Vehicle chosenRentalVehicle)
    {
        string chosenRentalVehicleMakeAndModel = chosenRentalVehicle.GetVehicleMakeAndModel();
        string chosenRentalVehiclePrice = Convert.ToString(chosenRentalVehicle.GetRentalPrice());
        string chosenRentalVehicleDeposit = Convert.ToString(chosenRentalVehicle.GetDepositPrice());

        SelectedMessage(chosenRentalVehicleMakeAndModel + " | Daily Rental Price: £" + chosenRentalVehiclePrice + " | Deposit Price: £" + chosenRentalVehicleDeposit);
    }

    public void SetOptionNumber(int optionNumber)
    {
        this.optionNumber = optionNumber;
    }
}