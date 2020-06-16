using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Strategy
{
    public class NormalAccountStrategy : ITransactionStrategy
    {
        public double GetTransactionFee()
        {
            return 5.00; 
        }
    }
}
