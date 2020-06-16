using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Strategy
{
    public class ProAccountStrategy : ITransactionStrategy
    {
        public double GetTransactionFee()
        {
            return 0.5; 
        }
    }
}
