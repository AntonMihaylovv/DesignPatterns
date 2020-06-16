using SoftwareDesignPatternsProject.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject
{
    public class Account
    {
        public string Name { get; set; }
        public bool CreditCard { get; set; }
        public bool BusinessAccount { get; set; }
        public double Balance { get; set; }
        public ITransactionStrategy TransactionStrategy { get; set; }

        public class Builder
        {
            private string _name = "No name";
            private bool _creditCard = false;
            private bool _businessAccount = false;
            private double _amount = 0.00;
            private ITransactionStrategy _transactionStrategy = new NormalAccountStrategy(); 

            public Builder() { }
            
            public Builder WithName(string name)
            {
                _name = name;
                return this; 
            }

            public Builder WithCreditCard(bool creditCard)
            {
                _creditCard = creditCard;
                return this; 
            }

            public Builder AsBusinessAccount(bool businessAccount)
            {
                _businessAccount = businessAccount;
                return this; 
            }

            public Builder WithAmount(double amount)
            {
                _amount = amount;

                if (_amount >= 10000.00)
                {
                    _transactionStrategy = new ProAccountStrategy(); 
                }

                return this; 
            }

            public Account Build()
            {
                Account account = new Account();
                account.Name = _name;
                account.CreditCard = _creditCard;
                account.BusinessAccount = _businessAccount;
                account.Balance = _amount;
                account.TransactionStrategy = _transactionStrategy; 
                return account; 
            }
        }

        // Private constructor
        private Account() { } 

        public string Show()
        {
            var accountType = (Balance < 10000) ? "Normal" : "Pro"; 
            return $"Name: {Name}, CreditCard: {CreditCard}, BusinessAccount: {BusinessAccount}, Amount ($): {Balance}, Account type: {accountType}"; 
        }
    }
}
