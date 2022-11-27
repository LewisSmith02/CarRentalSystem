DisplayManager displayManager = new DisplayManager();
AccountManager accountManager = new AccountManager();
VehicleManager vehicleManager = new VehicleManager();
MenuManager menuManager = new MenuManager();
Account currentLoggedInUser = new Account();

// APPLICATION START
while (true)
{
    // USER LOGIN MENU
    while (true)
    {
        accountManager.ReadAccountDataFromFile();

        displayManager.ApplicationLaunchMessage();
        displayManager.ViewingMessage("LOGIN MENU");
        Console.WriteLine();
        displayManager.InputInfoMessage("Enter username:");
        string userInputUsername = Console.ReadLine();

        if (userInputUsername.ToLower() == "quit")
        {
            Environment.Exit(0);
        }
        Account basicAccount = accountManager.GetBasicAccount(userInputUsername);
        PremiumAccount premiumAccount = accountManager.GetPremiumAccount(userInputUsername);
        AdminAccount adminAccount = accountManager.GetAdminAccount(userInputUsername);

        // LOGGED IN AS BASICACCOUNT START
        if (basicAccount != null)
        {
            displayManager.InputInfoMessage("Enter password:");
            string basicUserPassword = Console.ReadLine();

            if (basicUserPassword != null && basicAccount.GetAccountPassword() == basicUserPassword)
            {
                currentLoggedInUser = basicAccount;
                string accountType = "Basic";

                displayManager.LoggedInSuccessfully(userInputUsername, accountType);

                // START OF APP
                while (true)
                {
                    displayManager.ShowMenu();

                    string userInput = Convert.ToString(Console.ReadLine());

                    // USER INFO COMMANDS
                    if (userInput == "-v")
                    {
                        menuManager.ShowVersion();
                    }
                    else if (userInput == "-help")
                    {
                        menuManager.ShowUserHelp();
                    }
                    else if (userInput == "-m")
                    {
                        menuManager.ShowMenu();
                    }
                    else if (userInput == "0")
                    {
                        vehicleManager.WriteVehicleDataToFile();
                        menuManager.Logout();
                        break;
                    }

                    // HELP MENU
                    else if (userInput == "1")
                    {
                        menuManager.ShowHelpMenu();
                    }

                    // RENT VEHICLE
                    else if (userInput == "2")
                    {
                        vehicleManager.BeginUserRentalProcess(currentLoggedInUser);
                    }

                    // RETURN VEHICLE
                    else if (userInput == "3")
                    {
                        vehicleManager.BeginUserReturnProcess(currentLoggedInUser);
                    }

                    // DEPOSIT / WITHDRAWL FUNDS
                    else if (userInput == "4")
                    {
                        displayManager.ShowDepositWithdrawalMenu();

                        string choice = Console.ReadLine();

                        if (choice == "0")
                        {
                            accountManager.BeginDepositProcess(currentLoggedInUser);

                        }
                        else if (choice == "1")
                        {
                            accountManager.BeginWithdrawalProcess(currentLoggedInUser);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        displayManager.ErrorMessage("Please choose an available menu option.");
                        Console.WriteLine();
                    }
                }
                // END OF APP
            }
            else
            {
                displayManager.ErrorMessage("Incorrect password!");
            }
        }

        // LOGGED IN AS PREMIUMACCOUNT START
        else if (premiumAccount != null)
        {
            displayManager.InputInfoMessage("Enter password:");
            string premiumUserPassword = Console.ReadLine();

            if (premiumUserPassword != null && premiumAccount.GetAccountPassword() == premiumUserPassword)
            {
                currentLoggedInUser = premiumAccount;
                string accountType = "Premium";

                displayManager.LoggedInSuccessfully(userInputUsername, accountType);

                // START OF APP
                while (true)
                {
                    displayManager.ShowMenu();

                    string userInput = Convert.ToString(Console.ReadLine());

                    // USER INFO COMMANDS
                    if (userInput == "-v")
                    {
                        menuManager.ShowVersion();
                    }
                    else if (userInput == "-help")
                    {
                        menuManager.ShowUserHelp();
                    }
                    else if (userInput == "-m")
                    {
                        menuManager.ShowMenu();
                    }
                    else if (userInput == "0")
                    {
                        vehicleManager.WriteVehicleDataToFile();
                        menuManager.Logout();
                        break;
                    }

                    // HELP MENU
                    else if (userInput == "1")
                    {
                        menuManager.ShowHelpMenu();
                    }

                    // RENT VEHICLE
                    else if (userInput == "2")
                    {
                        vehicleManager.BeginUserRentalProcess(currentLoggedInUser);
                    }

                    // RETURN VEHICLE
                    else if (userInput == "3")
                    {
                        vehicleManager.BeginUserReturnProcess(currentLoggedInUser);
                    }

                    // DEPOSIT / WITHDRAW FUNDS
                    else if (userInput == "4")
                    {
                        displayManager.ShowDepositWithdrawalMenu();

                        string choice = Console.ReadLine();

                        if (choice == "0")
                        {
                            accountManager.BeginDepositProcess(currentLoggedInUser);

                        }
                        else if (choice == "1")
                        {
                            accountManager.BeginWithdrawalProcess(currentLoggedInUser);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        displayManager.ErrorMessage("Please choose an available menu option.");
                        Console.WriteLine();
                    }
                }
                // END OF APP
            }
            else
            {
                displayManager.ErrorMessage("Incorrect password!");
            }
        }

        // LOGGED IN AS ADMINACCOUNT START
        else if (adminAccount != null)
        {
            displayManager.InputInfoMessage("Enter password:");
            string adminPassword = Console.ReadLine();

            if (adminPassword != null && adminAccount.GetAccountPassword() == adminPassword)
            {
                currentLoggedInUser = adminAccount;
                string accountType = "Admin";

                displayManager.LoggedInSuccessfully(userInputUsername, accountType);

                // START OF APP
                while (true)
                {
                    displayManager.ShowAdminMenu();

                    string userInput = Convert.ToString(Console.ReadLine());

                    // USER INFO COMMANDS
                    if (userInput == "-v")
                    {
                        menuManager.ShowVersion();
                    }
                    else if (userInput == "-m")
                    {
                        menuManager.ShowMenu();
                    }
                    else if (userInput == "-adminhelp")
                    {
                        menuManager.ShowAdminHelp();
                    }
                    else if (userInput == "-help")
                    {
                        menuManager.ShowUserHelp();
                    }
                    else if (userInput == "0")
                    {
                        vehicleManager.WriteVehicleDataToFile();
                        menuManager.Logout();
                        break;
                    }

                    // HELP MENU
                    else if (userInput == "1")
                    {
                        menuManager.ShowHelpMenu();
                    }

                    // RENT VEHICLE
                    else if (userInput == "2")
                    {
                        vehicleManager.BeginUserRentalProcess(currentLoggedInUser);
                    }

                    // RETURN VEHICLE
                    else if (userInput == "3")
                    {
                        vehicleManager.BeginUserReturnProcess(currentLoggedInUser);
                    }

                    // DEPOSIT / WITHDRAW FUNDS
                    else if (userInput == "4")
                    {
                        displayManager.ShowDepositWithdrawalMenu();

                        string choice = Console.ReadLine();

                        if (choice == "0")
                        {
                            accountManager.BeginDepositProcess(currentLoggedInUser);

                        }
                        else if (choice == "1")
                        {
                            accountManager.BeginWithdrawalProcess(currentLoggedInUser);
                        }
                    }

                    // ADMIN MENU
                    else if (userInput == "5")
                    {
                        vehicleManager.BeginAdminVehicleManagement();
                    }

                    else
                    {
                        Console.WriteLine();
                        displayManager.ErrorMessage("Please choose an available menu option.");
                        Console.WriteLine();
                    }
                }
                // END OF APP
            }
            else
            {
                displayManager.ErrorMessage("Incorrect password!");
            }
        }

        else
        {
            displayManager.ErrorMessage("No account with that username found.\n");
        }
    }
}