using System;
using Abstractions;
using Common;

namespace Lesson2
{
    public class BankPayPlugin : GenericPlugin<BankPay>
    {
        public BankPayPlugin():base("BankPay")
        {
            

        }
    }
}