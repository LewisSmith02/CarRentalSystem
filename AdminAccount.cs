public class AdminAccount : Account
{
    // DATA
    DisplayManager displayManager = new DisplayManager();
    VehicleManager vehicleManager = new VehicleManager();

    // CONSTRUCTORS
    public AdminAccount (string accountType, string username, string password)
    {
        this.accountType = accountType;
        this.username = username;
        this.password = password;
        this.balance = 10000;
    }
    public AdminAccount()
    {

    }
    // FUNCTIONS
    public override int GetRentalLimit()
    {
        return 10000;
    }
    public void CreateNewVehicle()
    {
        displayManager.GetIsDevCheckResult(true);

        displayManager.ViewingMessage("VEHICLE CREATION MENU");
        Console.WriteLine();

        Vehicle newVehicle = new Vehicle();
        newVehicle.SetAvailability(1);

        displayManager.DevInfoMessage("ENTER VEHICLE REGISTRATION: ");
        newVehicle.SetVehicleRegPlate(Convert.ToString(Console.ReadLine()));

        displayManager.DevInfoMessage("ENTER VEHICLE MAKE: ");
        newVehicle.SetVehicleMake(Convert.ToString(Console.ReadLine()));

        displayManager.DevInfoMessage("ENTER VEHICLE MODEL: ");
        newVehicle.SetVehicleModel(Convert.ToString(Console.ReadLine()));

        while (true)
        {
            try
            {
                displayManager.DevInfoMessage("ENTER RENTAL PRICE: ");
                newVehicle.SetRentalPrice(Convert.ToInt32(Console.ReadLine()));
                displayManager.DevInfoMessage("ENTER DEPOSIT PRICE: ");
                newVehicle.SetDepositPrice(Convert.ToInt32(Console.ReadLine()));
                break;
            }
            catch (FormatException)
            {
                displayManager.ErrorMessage("Invalid data entered. Please enter an integer.");
            }
        }
        vehicleManager.AddVehicles(newVehicle);
    }
}