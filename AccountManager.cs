public class AccountManager
{
    // DATA
    DisplayManager displayManager = new DisplayManager();
    List<Account> basicAccounts = new List<Account>();
    List<Account> premiumAccounts = new List<Account>();
    List<Account> adminAccounts = new List<Account>();

    // CONSTRUCTORS
    public AccountManager()
    {
        //USAGE: Re-populate accounts.dat file
        //CreateAndWriteAccountDataToFile();
    }

    private void CreateAndWriteAccountDataToFile()
    {
        AdminAccount admin = new AdminAccount("admin", "admin", "admin");
        Account basicAccount_001 = new Account("basic", "buser001", "b001");
        Account basicAccount_002 = new Account("basic", "buser002", "b002");
        PremiumAccount premiumAccount_001 = new PremiumAccount("premium", "puser001", "p001");
        PremiumAccount premiumAccount_002 = new PremiumAccount("premium", "puser002", "p002");

        adminAccounts.Add(admin);
        basicAccounts.Add(basicAccount_001);
        basicAccounts.Add(basicAccount_002);
        premiumAccounts.Add(premiumAccount_001);
        premiumAccounts.Add(premiumAccount_002);

        WriteAccountDataToFile();
    }
    public Account GetBasicAccount(string username)
    {
        return basicAccounts.Find(Account => Account.GetAccountName() == username);
    }
    public PremiumAccount GetPremiumAccount(string username)
    {
        return (PremiumAccount)premiumAccounts.Find(PremiumAccount => PremiumAccount.GetAccountName() == username);
    }
    public AdminAccount GetAdminAccount(string username)
    {
        return (AdminAccount)adminAccounts.Find(AdminAccount => AdminAccount.GetAccountName() == username);    
    }

    // FUNCTIONS
    public void WriteAccountDataToFile()
    {
        FileStream file = File.Open("accounts.dat", FileMode.Create);
        BinaryWriter bw = new BinaryWriter(file);

        foreach (Account account in basicAccounts)
        {
            bw.Write(account.GetAccountType());
            bw.Write(account.GetAccountName());
            bw.Write(account.GetAccountPassword());
        }
        foreach (PremiumAccount account in premiumAccounts)
        {
            bw.Write(account.GetAccountType());
            bw.Write(account.GetAccountName());
            bw.Write(account.GetAccountPassword());
        }
        foreach (AdminAccount account in adminAccounts)
        {
            bw.Write(account.GetAccountType());
            bw.Write(account.GetAccountName());
            bw.Write(account.GetAccountPassword());
        }
        bw.Close();
        file.Close();
    }

    // USAGE: [ReadAccountDataFromFile()] Program.cs LINE 13 [Start of User login process]
    public void ReadAccountDataFromFile()
    {
        FileStream file = File.Open("accounts.dat", FileMode.Open);
        BinaryReader br = new BinaryReader(file);

        bool endOfFile()
        {
            var binaryStream = br.BaseStream;
            return (binaryStream.Position == binaryStream.Length);
        }

        while (endOfFile() == false)
        {
            Account account = new Account();
            account.SetAccountType(br.ReadString());
            account.SetAccountName(br.ReadString());
            account.SetAccountPassword(br.ReadString());

            if (account.GetAccountType() == "admin")
            {
                AdminAccount adminAccount = new AdminAccount();
                adminAccount.SetAccountType("admin");
                adminAccount.SetAccountName(account.GetAccountName());
                adminAccount.SetAccountPassword(account.GetAccountPassword());

                adminAccounts.Add(adminAccount);
            }
            else if (account.GetAccountType() == "basic")
            {
                basicAccounts.Add(account);
            }
            else if (account.GetAccountType() == "premium")
            {
                PremiumAccount premiumAccount = new PremiumAccount();
                premiumAccount.SetAccountType("premium");
                premiumAccount.SetAccountName(account.GetAccountName());
                premiumAccount.SetAccountPassword(account.GetAccountPassword());

                premiumAccounts.Add(premiumAccount);
            }
        }
        br.Close();
        file.Close();
    }
    public void BeginDepositProcess(Account currentLoggedInUser)
    {
        bool isDepositSuccessful = false;
        while (isDepositSuccessful == false)
        {
            displayManager.InputInfoMessage("Enter desired deposit amount: ");
            int depositAmount = Convert.ToInt32(Console.ReadLine());

            isDepositSuccessful = currentLoggedInUser.DepositFunds(depositAmount);
        }
    }
    public void BeginWithdrawalProcess(Account currentLoggedInUser)
    {
        bool isWithdrawalSuccessful = false;
        while (isWithdrawalSuccessful == false)
        {
            displayManager.InputInfoMessage("Enter desired withdrawal amount: ");
            string withdrawalAmountStr = Console.ReadLine();
            int withdrawalAmount = 0;

            if (withdrawalAmountStr == "-b")
            {
                break;
            }
            else
            {
                try
                {
                    withdrawalAmount = Convert.ToInt32(withdrawalAmountStr);
                }
                catch (FormatException)
                {
                    displayManager.DevInfoMessage("FORMAT EXCEPTION");
                }
                catch (OverflowException)
                {
                    displayManager.DevInfoMessage("OVERFLOW EXCEPTION");
                }
            }
            isWithdrawalSuccessful = currentLoggedInUser.WithdrawFunds(withdrawalAmount);
        }
    }
}