using SoftwareDesignPatternsProject.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Command
{
    class Deposit : ITransaction
    {
        private readonly double _amount;
        private readonly Account _account;
        private readonly LanguageSingleton _translator; 
        public bool IsCompleted { get; set; }

        public Deposit(Account account, double amount)
        {
            _account = account;
            _amount = amount;
            _translator = LanguageSingleton.Instance; 
            IsCompleted = false; 
        }

        public void Execute()
        {
            var transactionPrice = _account.TransactionStrategy.GetTransactionFee();
            var amountToDeposit = _amount - transactionPrice;
            _account.Balance += amountToDeposit;

            Console.WriteLine(_translator.GetTranslation("TransactionPrice") + transactionPrice);

            if (_account.Balance >= 10000.00)
            {
                _account.TransactionStrategy = new ProAccountStrategy();
            }
            else
            {
                _account.TransactionStrategy = new NormalAccountStrategy();
            }

            IsCompleted = true;  
        }
    }
}
