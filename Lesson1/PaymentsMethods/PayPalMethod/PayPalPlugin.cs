using System;
using Abstractions;
using Common;

namespace PaymentMethod
{
    public class PayPallPlugin : GenericPlugin<PayPal>
    {
        public PayPallPlugin():base("PayPal payment method")
        {
            

        }
    }
}