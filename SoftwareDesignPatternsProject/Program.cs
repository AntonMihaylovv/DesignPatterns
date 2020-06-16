using SoftwareDesignPatternsProject.Command;
using SoftwareDesignPatternsProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareDesignPatternsProject
{
    // Using Singleton, Builder, Command and Strategy pattern... 

    class Program
    {
        public static LanguageSingleton Translator = LanguageSingleton.Instance;
        public static List<Account> AccountList = new List<Account>();

        static void Main(string[] args)
        {
            AccountList.Add(new Account.Builder().WithName("Main").WithCreditCard(true).AsBusinessAccount(false).WithAmount(1000.00).Build());
            AccountList.Add(new Account.Builder().WithName("Savings").WithCreditCard(false).AsBusinessAccount(false).WithAmount(11000.00).Build());

            // Langauge loop
            while (true)
            {
                Console.WriteLine("Choose your language (BG/EN):");
                var language = Console.ReadLine();

                if (language.ToLower().Equals("en"))
                {
                    Translator.ToEnglish();
                    break;
                }
                else if (language.ToLower().Equals("bg"))
                {
                    Translator.ToBulgarian();
                    break;
                }
            }

            // Login loop
            while (true)
            {
                // Get username
                Console.WriteLine(Translator.GetTranslation("Username"));
                var username = Console.ReadLine();
                
                while (username.ToLower() != "test")
                {
                    Console.WriteLine("Invalid username, try again./Нелавидно потребителско име, опитайте отново.");
                    username = Console.ReadLine();
                }

                // Get password
                Console.WriteLine(Translator.GetTranslation("Password"));
                var password = Console.ReadLine();

                while (password != "test123")
                {
                    Console.WriteLine("Invalid password, try again./Невалидна парола, опитайте отново.");
                    password = Console.ReadLine();
                }
                break;
            }

            Console.Clear();
            Console.WriteLine(Translator.GetTranslation("AccountInformation") + AccountList.Count);
            SeeCommands();

            // Banking loop
            while (true)
            {
                var command = Console.ReadLine();

                switch (command)
                {
                    case "-newaccount":
                        CreateAccount();
                        break;
                    case "-seeaccounts":
                        SeeAccounts();
                        break;
                    case "-tobg":
                        Translator.ToBulgarian();
                        Console.WriteLine("Езикът е сменен на български.");
                        break;
                    case "-toen":
                        Translator.ToEnglish();
                        Console.WriteLine("Language changed to english.");
                        break;
                    case "-clear":
                        Console.Clear();
                        break;
                    case "-createtransaction":
                        CreateTransaction();
                        break;
                    case "-help":
                        SeeCommands();
                        break;
                    default:
                        Console.WriteLine(Translator.GetTranslation("CommandError"));
                        break;
                }
            }
        }

        private static void CreateTransaction()
        {
            if (AccountList.Count != 0)
            {
                var transactionManager = new TransactionManager();
                Account account = null;

                while (account == null)
                {
                    Console.WriteLine(Translator.GetTranslation("ChooseAccount"));
                    var accountName = Console.ReadLine();

                    if (AccountList.Any(x => x.Name.ToLower() == accountName.ToLower()))
                        account = AccountList.SingleOrDefault(x => x.Name.ToLower() == accountName.ToLower());
                }

                // Get transaction type
                while (true)
                {
                    Console.WriteLine(Translator.GetTranslation("TransactionType"));
                    var type = Console.ReadLine();
                    if (type.ToLower().Equals("transfer"))
                    {
                        Account accountToTransferTo = null;
                        while (accountToTransferTo == null)
                        {
                            Console.WriteLine(Translator.GetTranslation("ChooseAccount"));
                            var accountName = Console.ReadLine();
                            if (AccountList.Any(x => x.Name.ToLower() == accountName.ToLower()))
                                accountToTransferTo = AccountList.SingleOrDefault(x => x.Name.ToLower() == accountName.ToLower());
                        }
                        Console.WriteLine(Translator.GetTranslation("AmountToTransfer"));
                        var amount = Convert.ToDouble(Console.ReadLine());
                        var transfer = new Transfer(account, accountToTransferTo, amount);
                        transactionManager.AddTransaction(transfer);

                        Console.WriteLine(Translator.GetTranslation("TransactionPrice") + account.TransactionStrategy.GetTransactionFee() + '$');

                        Console.WriteLine(Translator.GetTranslation("TransactionComplete"));
                        var complete = (Console.ReadKey().KeyChar == 'y') ? true : false;
                        Console.WriteLine();

                        if (complete)
                            transactionManager.ProcessTransactions();

                        SeeAccounts();
                        break;
                    }
                    else if (type.ToLower().Equals("deposit"))
                    {
                        Console.WriteLine(Translator.GetTranslation("AmountToDeposit"));
                        var amount = Convert.ToDouble(Console.ReadLine());

                        var deposit = new Deposit(account, amount);
                        transactionManager.AddTransaction(deposit);

                        Console.WriteLine(Translator.GetTranslation("TransactionPrice") + account.TransactionStrategy.GetTransactionFee() + '$');

                        Console.WriteLine(Translator.GetTranslation("TransactionComplete"));
                        var complete = (Console.ReadKey().KeyChar == 'y') ? true : false;
                        Console.WriteLine();

                        if (complete)
                            transactionManager.ProcessTransactions();

                        SeeAccounts();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(Translator.GetTranslation("AccountMissing"));
            }
        }

        public static void SeeCommands()
        {
            Console.WriteLine(Translator.GetTranslation("CommandList"));
            Console.WriteLine("-help");
            Console.WriteLine("-tobg");
            Console.WriteLine("-toen");
            Console.WriteLine("-clear");
            Console.WriteLine("-newaccount");
            Console.WriteLine("-seeaccounts");
            Console.WriteLine("-createtransaction");
        }

        public static void CreateAccount()
        {
            var accountBuilder = new Account.Builder();

            // Get name
            Console.WriteLine(Translator.GetTranslation("AccountName"));
            var name = Console.ReadLine();
            accountBuilder.WithName(name);
            Console.WriteLine();

            // CreditCard
            Console.WriteLine(Translator.GetTranslation("AccountCreditCard"));
            var creditCard = (Console.ReadKey().KeyChar == 'y') ? true : false;
            accountBuilder.WithCreditCard(creditCard);
            Console.WriteLine();

            // Business Account
            Console.WriteLine(Translator.GetTranslation("AccountBusiness"));
            var businessAccount = (Console.ReadKey().KeyChar == 'y') ? true : false;
            accountBuilder.AsBusinessAccount(businessAccount);
            Console.WriteLine();

            Console.WriteLine(Translator.GetTranslation("AccountAmount"));
            var amount = Convert.ToDouble(Console.ReadLine());
            accountBuilder.WithAmount(amount);
            Console.WriteLine();

            var account = accountBuilder.Build();
            AccountList.Add(account);
            Console.WriteLine(account.Show());
            Console.WriteLine(Translator.GetTranslation("AccountCreated"));
            Console.WriteLine(Translator.GetTranslation("AccountInformation") + AccountList.Count);
        }

        public static void SeeAccounts()
        {
            foreach (var v in AccountList)
            {
                Console.WriteLine(v.Show());
            }
        }

    }
}
