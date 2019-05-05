using System;
using Abstractions;
using Common;

namespace PaymentMethod
{
    public class BankPayPlugin : GenericPlugin<BankPay>
    {
        public BankPayPlugin():base("BankPay")
        {
            

        }
    }
}