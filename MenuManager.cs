class MenuManager
{
    DisplayManager displayManager = new DisplayManager();

    public void ShowHelpMenu()
    {
        Console.WriteLine();
        displayManager.HelpMessage("-help : SEE LIST OF HELPFUL USER COMMANDS");
        displayManager.HelpMessage("-adminhelp: SEE LIST OF HELPFUL ADMIN COMMANDS");
        displayManager.HelpMessage("-m : SEE MAIN APPLICATION MENU");
        displayManager.HelpMessage("-v : SEE CURRENT APPLICATION VERSION");
        Console.WriteLine();
    }
    public void ShowAdminHelp()
    {
        Console.WriteLine();
        displayManager.ViewingMessage("ADMIN HELP MENU");
        displayManager.HelpMessage("TO ADD A VEHICLE: add [VEHICLEPLATE] [VEHICLEMAKE] [VEHICLEMODEL]");
        displayManager.HelpMessage("TO REMOVE A VEHICLE: remove [VEHICLE PLATE]");
        Console.WriteLine();
    }
    public void ShowUserHelp()
    {
        Console.WriteLine();
        displayManager.ViewingMessage("USER HELP MENU");
        displayManager.HelpMessage("-m to view main menu options");
        displayManager.HelpMessage("-v to view current software version");
        Console.WriteLine();
    }
    public void Logout()
    {
        Console.WriteLine();
        displayManager.SuccessMessage("Successfully logged out!");
        Console.WriteLine();
    }
    public void ShowVersion()
    {
        Console.WriteLine();
        Console.WriteLine("Version 0.0.1");
        Console.WriteLine();
    }
    public void ShowMenu()
    {
        Console.WriteLine();
        displayManager.ShowMenu();
        Console.WriteLine();
    }
}