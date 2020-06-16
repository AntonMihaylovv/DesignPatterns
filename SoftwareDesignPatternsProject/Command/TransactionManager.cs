using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareDesignPatternsProject.Command
{
    public class TransactionManager
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>(); 

        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction); 
        }

        public void ProcessTransactions()
        {
            foreach(var v in _transactions.Where(x => !x.IsCompleted))
            {
                v.Execute(); 
            }
        }
    }
}
