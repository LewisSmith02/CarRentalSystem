using System.Runtime.Serialization.Formatters.Binary;

public class VehicleManager
{
    // DATA
    private DisplayManager displayManager = new DisplayManager();
    private List<Vehicle> vehicles = new List<Vehicle>();
    private List<Vehicle> rentableVehicles = new List<Vehicle>();

    Vehicle chosenRentalVehicle = new Vehicle();
    Vehicle chosenReturnVehicle = new Vehicle();

    Dictionary<Vehicle, string> vehiclesInUse = new Dictionary<Vehicle, string>();
    List<Vehicle> userReturnableVehicles = new List<Vehicle>();

    // CONSTRUCTOR
    public VehicleManager()
    {
        // USAGE: Re-populate vehicle.dat file
        //CreateAndWriteVehicleDataToFile();
    }

    private void CreateAndWriteVehicleDataToFile()
    {
        Vehicle vehicle_001 = new("AJ05 AGG", "VOLKSWAGEN", "POLO", 30, 140, 1);
        Vehicle vehicle_002 = new("BD21 YCG", "VAUXHALL", "ASTRA", 20, 75, 1);
        Vehicle vehicle_003 = new("CA15 IAK", "RENAULT", "CLIO", 15, 75, 1);
        Vehicle vehicle_004 = new("DI56 AOD", "BMW", "6 SERIES", 40, 150, 1);
        Vehicle vehicle_005 = new("ET10 BHV", "AUDI", "A5", 45, 150, 1);

        vehicles.Add(vehicle_001);
        vehicles.Add(vehicle_002);
        vehicles.Add(vehicle_003);
        vehicles.Add(vehicle_004);
        vehicles.Add(vehicle_005);

        WriteVehicleDataToFile();
    }
    // RENTAL AND RETURN FUNCTIONS
    public void BeginUserRentalProcess(Account currentLoggedInUser)
    {
        ClearVehicles();
        ReadVehicleDataFromFile();

        bool isUserDev = currentLoggedInUser.GetAccountName() == "admin";
        displayManager.GetIsDevCheckResult(isUserDev);

        // CHECK IF ANY RENTABLE VEHICLES AVAILABLE BEFORE PROCEEDING
        Vehicle rentalAvailabilityCheck = GetVehiclesList().Find(x => x.GetAvailability() == 1);

        if (rentalAvailabilityCheck != null && currentLoggedInUser.isRentalLimitReached() == false)
        {
            Console.WriteLine();
            displayManager.ViewingMessage("RENT VEHICLES");
            currentLoggedInUser.ShowUserBalanceAndRentalInfo();

            Console.WriteLine("The available rentable vehicles are listed below:\n");
            DisplayRentableVehicles();
            GetRentableVehicles(currentLoggedInUser.GetAccountName());

            while (true)
            {
            RentalStart:

                string vehicleRentalChoiceStr;
                int vehicleRentalChoice;

                Console.WriteLine();
                displayManager.InputInfoMessage("Select a vehicle to rent using it's index number: ");
                vehicleRentalChoiceStr = Console.ReadLine();
                // BACK TO MENU
                if (vehicleRentalChoiceStr == "-b")
                {
                    ClearVehicles();
                    ReadVehicleDataFromFile();
                    goto RentalStart;
                }

                while (true)
                {
                    // PREVENTING INPUT OF ANYTHING OTHER THAN VALID INDEX NUMBER
                    try
                    {
                        vehicleRentalChoice = Convert.ToInt32(vehicleRentalChoiceStr);
                        if (vehicleRentalChoice > rentableVehicles.Count() - 1)
                        {
                            displayManager.ErrorMessage("The vehicle you have selected to rent doesn't exist! Please try again.");
                            displayManager.DevInfoMessage("Choice out of range!\n");

                            displayManager.InputInfoMessage("Select a vehicle to rent using it's index number: ");
                            vehicleRentalChoiceStr = Console.ReadLine();
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        displayManager.ErrorMessage("The vehicle you have selected to rent doesn't exist! Please try again.");
                        displayManager.DevInfoMessage("CATCH: FORMAT EXCEPTION\n");

                        displayManager.InputInfoMessage("Select a vehicle to rent using it's index number: ");
                        vehicleRentalChoiceStr = Console.ReadLine();
                    }
                    catch (OverflowException)
                    {
                        displayManager.ErrorMessage("The vehicle you have selected to rent doesn't exist! Please try again.");
                        displayManager.DevInfoMessage("CATCH: OVERFLOW EXCEPTION\n");

                        displayManager.InputInfoMessage("Select a vehicle to rent using it's index number: ");
                        vehicleRentalChoiceStr = Console.ReadLine();
                    }
                }

                if (isRentalChoiceInRange(vehicleRentalChoice) == true && currentLoggedInUser.isRentalLimitReached() == false)
                {
                    chosenRentalVehicle = rentableVehicles[vehicleRentalChoice];
                    displayManager.DisplayChosenVehicleRentalPriceAndDeposit(chosenRentalVehicle);

                    // USER DECIDING NUMBER OF RENTAL DAYS (WITHIN RANGE)
                    string vehicleRentalDurationStr;
                    int vehicleRentalDuration;
                    while (true)
                    {
                        Console.WriteLine();
                        displayManager.InputInfoMessage("Enter desired number of rental days: ");
                        vehicleRentalDurationStr = Console.ReadLine();
                        // BACK TO MENU
                        if (vehicleRentalChoiceStr == "-b")
                        {
                            ClearVehicles();
                            ReadVehicleDataFromFile();
                            goto RentalStart;
                        }
                        // PREVENTING INPUT OF ANYTHING OTHER THAN VALID RENTAL DAY AMOUNT
                        try
                        {
                            vehicleRentalDuration = Convert.ToInt32(vehicleRentalDurationStr);
                            if (vehicleRentalDuration < 0)
                            {
                                displayManager.ErrorMessage("The minimum vehicle rental time amount is '1' Day. Please choose a valid amount!");
                                displayManager.DevInfoMessage("vehicleRentalDuration < 0\n");
                            }
                            else if (vehicleRentalDuration > 100)
                            {
                                displayManager.ErrorMessage("Vehicle rental time cannot exceed 100 days. Please choose a valid amount!");
                                displayManager.DevInfoMessage("vehicleRentalDuration > 100\n");
                            }
                            break;
                        }
                        catch (FormatException)
                        {
                            displayManager.ErrorMessage("Cannot rent vehicle for that amount of days! Please check the amount and try again!");
                            displayManager.DevInfoMessage("CATCH: FORMAT EXCEPTION\n");
                        }
                        catch (OverflowException)
                        {
                            displayManager.ErrorMessage("Cannot rent vehicle for that amount of days! Please check the amount and try again!");
                            displayManager.DevInfoMessage("CATCH: OVERFLOW EXCEPTION\n");
                        }
                    }

                    displayManager.DisplayChosenVehicleFinalPriceAndDeposit(chosenRentalVehicle, vehicleRentalDuration);

                    while (true)
                    {
                        Console.Write("Are you sure you want to rent this vehicle? (y/n): ");
                        string userRentalChoiceStr = Console.ReadLine();
                        Console.WriteLine();
                        char userRentalChoice = 'x';

                        // BACK TO MENU
                        if (userRentalChoiceStr == "-b")
                        {
                            break;
                        }
                        else { }

                        // PREVENTING INPUT OF ANYTHING OTHER THAN VALID Y/N OPTION
                        try
                        {
                            userRentalChoice = Convert.ToChar(userRentalChoiceStr);
                        }
                        catch (FormatException) { }

                        if (char.ToLower(userRentalChoice) == 'y')
                        {
                            if (currentLoggedInUser.GetBalance() < chosenRentalVehicle.GetFinalRentalPrice(vehicleRentalDuration))
                            {
                                Console.WriteLine();
                                displayManager.ErrorMessage("Insufficient funds. Please add funds to your account and try again!");
                                Console.WriteLine();
                                break;
                            }
                            RentVehicle(currentLoggedInUser.GetAccountName(), chosenRentalVehicle);
                            currentLoggedInUser.IncreaseActiveRentalAmount();
                            currentLoggedInUser.DeductVehicleCosts(chosenRentalVehicle, vehicleRentalDuration);
                            currentLoggedInUser.ShowRemainingBalance();
                            break;
                        }
                        else if (char.ToLower(userRentalChoice) == 'n')
                        {
                            Console.WriteLine();
                            break;
                        }
                        else { displayManager.ErrorMessage("Please select one of the options available."); }
                    }
                    break;
                }
                else if (isRentalChoiceInRange(vehicleRentalChoice) == false)
                {
                    displayManager.ErrorMessage("The vehicle you have selected to rent doesn't exist! Please try again.");
                }
                else { displayManager.ErrorMessage("The vehicle you have selected to rent doesn't exist! Please try again."); }
            }
        }
        else if (currentLoggedInUser.isRentalLimitReached() == true)
        {
            Console.WriteLine();
            displayManager.ErrorMessage("Rental limit reached! Please return your active rented vehicle to rent a new one.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            displayManager.ErrorMessage("No available vehicles to rent, please try again later!");
            Console.WriteLine();
        }
    }
    public void BeginUserReturnProcess(Account currentLoggedInUser)
    {
        userReturnableVehicles.Clear();
        SetUserVehiclesInUse(currentLoggedInUser.GetAccountName());

        bool isUserDev = currentLoggedInUser.GetAccountName() == "admin";
        displayManager.GetIsDevCheckResult(isUserDev);

        // CHECK IF ANY RETURNABLE VEHICLES AVAILABLE BEFORE PROCEEDING
        if (vehiclesInUse.Count() != 0)
        {
            Console.WriteLine();
            displayManager.ViewingMessage("RETURN VEHICLES");
            currentLoggedInUser.ShowActiveRentals();

            Console.WriteLine("The available returnable vehicles are listed below:\n");

            DisplayUserVehiclesInUse();
            DevGetUserReturnableVehicles(currentLoggedInUser.GetAccountName());

            while (true)
            {
            ReturnStart:

                Console.WriteLine();
                displayManager.InputInfoMessage("Select a vehicle to return using it's index number: ");
                string vehicleReturnChoiceStr = Console.ReadLine();
                int vehicleReturnChoice = 0;

                // BACK TO MENU
                if (vehicleReturnChoiceStr == "-b")
                {
                    break;
                }
                else { }

                // PREVENTING INPUT OF ANYTHING OTHER THAN VALID INDEX NUMBER
                try
                {
                    vehicleReturnChoice = Convert.ToInt32(vehicleReturnChoiceStr);

                    if (vehicleReturnChoice > userReturnableVehicles.Count() -1)
                    {
                        displayManager.ErrorMessage("Choice out of range!");
                        Console.WriteLine();
                        goto ReturnStart;
                    }

                    // TODO: FIX VEHICLE RETURN CHOICE OUT OF RANGE. SHOULDN'T PASS inRange CHECK!!!!!!!!!!!!!!
                }
                catch (FormatException)
                {
                    displayManager.ErrorMessage("The vehicle you have selected to return doesn't exist! Please try again.");
                    displayManager.DevInfoMessage("CATCH: FORMAT EXCEPTION");
                    goto ReturnStart;
                }
                catch (OverflowException)
                {
                    displayManager.ErrorMessage("The vehicle you have selected to return doesn't exist! Please try again.");
                    displayManager.DevInfoMessage("CATCH: OVERFLOW EXCEPTION");
                    goto ReturnStart;
                }

                if (isReturnChoiceInRange(vehicleReturnChoice) == true)
                {
                    chosenReturnVehicle = userReturnableVehicles[vehicleReturnChoice]; // TODO: LINE 203 PROBLEM - SELECTING DESIRED VEHICLERETURNCHOICE FROM LIST
                    Console.WriteLine();
                    displayManager.ShowSelectedVehicleAndBalanceChanges(chosenReturnVehicle.GetVehicleMakeAndModel(), Convert.ToString(chosenReturnVehicle.GetDepositPrice()));

                    while (true)
                    {
                        Console.Write("Are you sure you want to return this vehicle? (y/n): ");
                        string userReturnChoiceStr = Console.ReadLine();
                        Console.WriteLine();
                        char userReturnChoice = 'x';

                        // BACK TO MENU
                        if (userReturnChoiceStr == "-b")
                        {
                            break;
                        }
                        else { }

                        // PREVENTING INPUT OF ANYTHING OTHER THAN VALID Y/N OPTION
                        try
                        {
                            userReturnChoice = Convert.ToChar(userReturnChoiceStr);
                        }
                        catch (FormatException) { }

                        if (userReturnChoice == 'y')
                        {
                            ReturnVehicle(chosenReturnVehicle);
                            currentLoggedInUser.DecreaseActiveRentalAmount();
                            currentLoggedInUser.ReturnDepositPrice(chosenReturnVehicle);
                            currentLoggedInUser.DisplayBalance(currentLoggedInUser.GetBalance());
                            break;
                        }
                        else if (userReturnChoice == 'n')
                        {
                            Console.WriteLine();
                            break;
                        }
                        else { displayManager.ErrorMessage("Please select one of the two available options!"); }
                    }
                    break;
                }
                else if (vehicleReturnChoice <= vehiclesInUse.Count() && vehiclesInUse.Count() != 0)
                {
                    displayManager.ErrorMessage("The vehicle you have selected to return doesn't exist! Please try again.");
                    Console.WriteLine();
                    break;
                }
                else { displayManager.ErrorMessage("The vehicle you have selected to return doesn't exist! Please try again."); }
            }
        }
        else if (currentLoggedInUser.GetCurrentActiveRentals() == 0)
        {
            Console.WriteLine();
            displayManager.ErrorMessage("No vehicles to return!");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine();
            displayManager.ErrorMessage("No available vehicles to return.");
            Console.WriteLine();
        }
    }
    public void BeginAdminVehicleManagement()
    {
        ClearVehicles();
        ReadVehicleDataFromFile();

        AdminAccount admin = new AdminAccount();

        Console.WriteLine();
        displayManager.ViewingMessage("ADMINISTRATOR PANEL");
        Console.WriteLine();

        Console.WriteLine("[1] VIEW VEHICLES");
        Console.WriteLine("[2] ADD VEHICLES");
        Console.WriteLine("[3] REMOVE VEHICLES");
        Console.WriteLine("[4] EDIT VEHICLES");

        Console.WriteLine();
        displayManager.InputInfoMessage("SELECT A MENU OPTION TO PROCEED: ");
        string adminMenuChoice = Console.ReadLine();
        Console.WriteLine();

        if (adminMenuChoice == "1")
        {
            DisplayVehicles();
        }
        else if (adminMenuChoice == "2")
        {
            admin.CreateNewVehicle();
        }
        else if (adminMenuChoice == "3")
        {
            RemoveVehicles();
        }
        else if (adminMenuChoice == "4")
        {
            EditVehicles();
        }
        else { displayManager.ErrorMessage("Please select a valid menu option."); }
    }

    // USAGE: [WriteVehicleDataToFile()] VehicleManager.cs RentVehicle(): LINE 517
    // USAGE: [WriteVehicleDataToFile()] VehicleManager.cs ReturnVehicle(): LINE 529
    // USAGE: [WriteVehicleDataToFile()] VehicleManager.cs AddVehicles(): LINE 564
    // USAGE: [WriteVehicleDataToFile()] Program.cs basicAccount before Logout(): LINE 64
    // USAGE: [WriteVehicleDataToFile()] Program.cs premiumAccount before Logout(): LINE 154
    // USAGE: [WriteVehicleDataToFile()] Program.cs adminAccount before Logout(): LINE 248
    public void WriteVehicleDataToFile()
    {
        FileStream file = File.Open("vehicles.dat", FileMode.Create);
        BinaryWriter bw = new BinaryWriter(file);

        foreach (Vehicle vehicle in vehicles)
        {
            bw.Write(vehicle.GetVehicleRegPlate());
            bw.Write(vehicle.GetVehicleMake());
            bw.Write(vehicle.GetVehicleModel());
            bw.Write(vehicle.GetRentalPrice());
            bw.Write(vehicle.GetDepositPrice());
            bw.Write(vehicle.GetAvailability());
        }
        bw.Close();
        file.Close();
    }

    // USAGE: [ReadVehicleDataFromFile()] VehicleManager.cs BeginUserRentalProcess(): LINE 37 [Get vehicles from file and populate Lists/Dictionary before rental start]
    // USAGE: [ReadVehicleDataFromFile()] VehicleManager.cs BeginUserRentalProcess(): LINE 69 [Clear and re-populate List/Dictionary from file before rental start afte -b]
    // USAGE: [ReadVehicleDataFromFile()] VehicleManager.cs BeginAdminVehicleManagement(): LINE 302 [Get vehicles from file and populate Lists/Dictionary before management start]
    // USAGE: [ReadVehicleDataFromFile()] VehicleManager.cs DisplayVehices(): LINE 489 [Display all system vehicles in Admin Menu]
    public void ReadVehicleDataFromFile()
    {
        ClearVehicles();
        FileStream file = File.Open("vehicles.dat", FileMode.Open);
        BinaryReader br = new BinaryReader(file);

        bool endOfFile()
        {
            var binaryStream = br.BaseStream;
            return (binaryStream.Position == binaryStream.Length);
        }

        while (endOfFile() == false)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.SetVehicleRegPlate(br.ReadString());
            vehicle.SetVehicleMake(br.ReadString());
            vehicle.SetVehicleModel(br.ReadString());
            vehicle.SetRentalPrice(br.ReadInt32());
            vehicle.SetDepositPrice(br.ReadInt32());
            vehicle.SetAvailability(br.ReadInt32());

            vehicles.Add(vehicle);
        }
        br.Close();
        file.Close();
        SetRentableVehicles();
    }

    // GET DATA FUNCTIONS
    public Vehicle GetVehicle(string regPlate)
    {
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.GetVehicleRegPlate() == regPlate)
            {
                return vehicle;
            }
        }
        return null;
    }
    public List<Vehicle> GetVehiclesList()
    {
        return vehicles;
    }
    public List<Vehicle> GetRentableVehicles(string username)
    {
        foreach (Vehicle vehicle in rentableVehicles)
        {
            displayManager.DevInfoMessage("RENTABLE VEHICLE: " + vehicle.GetVehicleMakeAndModel()); // TEST: Displaying list contents
        }
        return rentableVehicles;
    }

    // SET DATA FUNCTIONS
    public void SetUserVehiclesInUse(string username)
    {
        foreach (KeyValuePair<Vehicle, String> kvp in vehiclesInUse)
        {
            if (kvp.Value == username)
            {
                userReturnableVehicles.Add(kvp.Key);
            }
        }
    }
    public void SetRentableVehicles()
    {
        foreach (Vehicle vehicle in vehicles)
        {
            if (vehicle.GetAvailability() == 0)
            {
                userReturnableVehicles.Add(vehicle);
            }
            else { rentableVehicles.Add(vehicle); }
        }
    }
    public void DevGetUserReturnableVehicles(string username)
    {
        foreach  (KeyValuePair <Vehicle, string> kvp in vehiclesInUse)
        {
            if (kvp.Value == username && username == "admin")
            {
                displayManager.DevInfoMessage("RETURNABLE VEHICLE: " + kvp.Key.GetVehicleMakeAndModel()); // TEST: Displaying list contents
            }
        }
    }

    // LIST DISPLAYING FUNCTIONS
    public void DisplayUserVehiclesInUse()
    {
        displayManager.SetOptionNumber(0);
        foreach (Vehicle vehicle in userReturnableVehicles)
        {
            displayManager.FormatListIndex(vehicle.GetVehicleMakeAndModel());
        }
    }
    public void DisplayRentableVehicles()
    {
        foreach (Vehicle vehicle in rentableVehicles)
        {
            if (vehicle.GetAvailability() == 1)
            {
                displayManager.FormatListIndex(vehicle.GetVehicleMakeAndModel());
            }
        }
        displayManager.SetOptionNumber(0);
    }
    public void DisplayReturnableVehicles(string username)
    {
        foreach (KeyValuePair<Vehicle, string> kvp in vehiclesInUse)
        {
            if (kvp.Value == username)
            {
                displayManager.FormatListIndex(kvp.Key.GetVehicleMakeAndModel());
            }
        }
        displayManager.SetOptionNumber(0);
    }
    public void DisplayVehicles()
    
    {
        if (vehicles.Count() == 0)
        {
            ReadVehicleDataFromFile();
        }

        displayManager.GetIsDevCheckResult(true);
        foreach (Vehicle vehicle in vehicles)
        {
            displayManager.DevInfoMessage("VEHICLE");
            Console.WriteLine();
            displayManager.DevDataInfo("REGISTRATION PLATE");
            Console.WriteLine(vehicle.GetVehicleRegPlate());
            displayManager.DevDataInfo("VEHICLE MAKE");
            Console.WriteLine(vehicle.GetVehicleMake());
            displayManager.DevDataInfo("VEHICLE MODEL");
            Console.WriteLine(vehicle.GetVehicleModel());
            displayManager.DevDataInfo("RENTAL PRICE");
            Console.WriteLine("£" + vehicle.GetRentalPrice());
            displayManager.DevDataInfo("DEPOSIT PRICE");
            Console.WriteLine("£" + vehicle.GetDepositPrice());
            displayManager.DevDataInfo("AVAILABILITY");
            Console.WriteLine(vehicle.GetAvailability());
            Console.WriteLine();
        }
    }

    // MAIN SYSTEM FUNCTIONS
    public void RentVehicle(string username, Vehicle chosenRentalVehicle)
    {
        chosenRentalVehicle.SetAvailability(0); // SET AVAILABILITY
        rentableVehicles.Remove(chosenRentalVehicle); // REMOVE VEHICLE FROM RENTABLE VEHICLES LIST
        userReturnableVehicles.Add(chosenRentalVehicle); // ADD VEHICLE TO RETURNABLE VEHICLES LIST
        vehiclesInUse.Add(chosenRentalVehicle, username); // ADD VEHICLE TO VEHICLES IN USE LIST ALONG WITH USERNAME
        displayManager.SuccessMessage("Vehicle Rented Successfully!");
        rentableVehicles.OrderBy(vehicle => vehicle.GetVehicleMakeAndModel());

        WriteVehicleDataToFile();
    }
    public void ReturnVehicle(Vehicle chosenReturnVehicle)
    {
        chosenReturnVehicle.SetAvailability(1); // SET AVAILABILITY
        rentableVehicles.Add(chosenReturnVehicle); // ADD VEHICLE TO RENTABLE VEHICLES LIST
        userReturnableVehicles.Remove(chosenReturnVehicle); // REMOVE VEHICLE FROM RETURNABLE VEHICLES LIST
        vehiclesInUse.Remove(chosenReturnVehicle); // REMOVE VEHICLE FROM VEHICLES IN USE LIST
        displayManager.SuccessMessage("Vehicle Returned Successfully!");
        userReturnableVehicles.OrderBy(vehicle => vehicle.GetVehicleMakeAndModel());

        WriteVehicleDataToFile();
    }

    // BOOLEAN FUNCTIONS
    public bool isRentalChoiceInRange(int vehicleRentalChoice)
    {
        if (vehicleRentalChoice <= rentableVehicles.Count() -1 && vehicleRentalChoice > -1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isReturnChoiceInRange(int vehicleReturnChoice)
    {
        if ((vehicleReturnChoice <= userReturnableVehicles.Count() && vehicleReturnChoice > -1) && (userReturnableVehicles.Count() != 0))
        {
            return true;
        }
        else if ((vehicleReturnChoice >= userReturnableVehicles.Count() && vehicleReturnChoice > -1) && (userReturnableVehicles.Count() == 0))
        {
            return true;
        }
        return false;
    }

    // ADMIN VEHICLE FUNCTIONS
    public void AddVehicles(Vehicle vehicle)
    {
        vehicles.Add(vehicle);
        rentableVehicles.Add(vehicle);
        WriteVehicleDataToFile();
        displayManager.SuccessMessage("VEHICLE WITH PLATE [" + vehicle.GetVehicleRegPlate() + "] HAS BEEN ADDED SUCCESSFULLY!");
        Console.WriteLine();
    }
    public void RemoveVehicles()
    {
        displayManager.ViewingMessage("VEHICLE REMOVAL MENU");
        Console.WriteLine();

        while (true)
        {
            displayManager.InputInfoMessage("ENTER REGISTRATION OF VEHICLE YOU WANT TO REMOVE: ");
            string regPlate = Convert.ToString(Console.ReadLine());

            if (regPlate == "-b")
            {
                break;
            }

            if (GetVehicle(regPlate) != null)
            {
                Vehicle vehicle = new Vehicle();
                vehicle = GetVehicle(regPlate);

                vehicles.Remove(GetVehicle(regPlate));
                rentableVehicles.Remove(GetVehicle(regPlate));
                displayManager.SuccessMessage("VEHICLE WITH PLATE [" + vehicle.GetVehicleRegPlate() + "] REMOVED SUCCESSFULLY.");
                Console.WriteLine();
                WriteVehicleDataToFile();
                break;
            }
            else if (GetVehicle(regPlate) == null)
            {
                displayManager.ErrorMessage("Invalid registration plate entered!");
            }
            else { Console.WriteLine("other"); }
        }
    }
    public void EditVehicles()
    {
        displayManager.ViewingMessage("VEHICLE EDIT MENU");
        Console.WriteLine();

        displayManager.GetIsDevCheckResult(true);
        displayManager.InputInfoMessage("ENTER REGISTRATION OF VEHICLE YOU WANT TO EDIT: ");
        string regPlate = Convert.ToString(Console.ReadLine());

        if (regPlate == "all")
        {
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.SetAvailability(1);
            }
            Console.WriteLine();
            displayManager.SuccessMessage(vehicles.Count() + " VEHICLE(S) AVAILABILITY CHANGED TO [1]");
            Console.WriteLine();
        }
        else if (GetVehicle(regPlate) != null)
        {
            Vehicle vehicle = new Vehicle();
            vehicle = GetVehicle(regPlate);

            userReturnableVehicles.Remove(vehicle);
            userReturnableVehicles.Remove(vehicle);
            vehicle.SetAvailability(1);

            Console.WriteLine();
            displayManager.SuccessMessage("VEHICLE WITH PLATE [" + vehicle.GetVehicleRegPlate() + "] AVAILABILITY SET TO [1] SUCCESSFULLY");
            Console.WriteLine();
        }
        else if (GetVehicle(regPlate) == null)
        {
            displayManager.ErrorMessage("Invalid registration plate entered!");
        }
        else { Console.WriteLine("other"); }

        WriteVehicleDataToFile();
    }
    public void ClearVehicles()
    {
        vehicles.Clear();
        rentableVehicles.Clear();
        userReturnableVehicles.Clear();
    }
}