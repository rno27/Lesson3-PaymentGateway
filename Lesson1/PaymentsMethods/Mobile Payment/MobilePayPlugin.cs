using System;
using Abstractions;
using Common;

namespace PaymentMethod
{
    public class MobilePayPlugin : GenericPlugin<MobilePay>
    {
        public MobilePayPlugin():base("Mobile Pay")
        {
            

        }
    }
}