using System;
using System.Collections.Generic;
using System.Text;

namespace SoftwareDesignPatternsProject.Command
{
    public interface ITransaction
    {
        bool IsCompleted { get; set; }
        void Execute(); 
    }
}
