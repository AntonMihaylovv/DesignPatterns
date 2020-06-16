using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Strategy
{
    public interface ITransactionStrategy
    {
        double GetTransactionFee(); 
    }
}
