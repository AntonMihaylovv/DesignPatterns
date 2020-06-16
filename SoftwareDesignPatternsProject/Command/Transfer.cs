using SoftwareDesignPatternsProject.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Command
{
    public class Transfer : ITransaction
    {
        private readonly double _amount;
        private readonly Account _fromAccount;
        private readonly Account _toAccount;
        private readonly LanguageSingleton _translator; 

        public bool IsCompleted { get; set; } 

        public Transfer(Account fromAccount, Account toAccount, double amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;
            _translator = LanguageSingleton.Instance; 
            IsCompleted = false; 
        }

        public void Execute()
        {
            var transactionPrice = _fromAccount.TransactionStrategy.GetTransactionFee();
            var totalPrice = _amount + transactionPrice;

            Console.WriteLine(_translator.GetTranslation("TransactionPrice") + transactionPrice);

            if (_fromAccount.Balance >= totalPrice)
            {
                _fromAccount.Balance -= totalPrice;
                _toAccount.Balance += _amount;
            }

            if (_fromAccount.Balance >= 10000.00)
            {
                _fromAccount.TransactionStrategy = new ProAccountStrategy();
            }
            else
            {
                _fromAccount.TransactionStrategy = new NormalAccountStrategy();
            }

            IsCompleted = true;
        }
    }
}
