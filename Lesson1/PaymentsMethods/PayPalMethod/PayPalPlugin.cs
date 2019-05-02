using System;
using Abstractions;
using Common;

namespace Lesson2
{
    public class PayPallPlugin : GenericPlugin<PayPal>
    {
        public PayPallPlugin():base("PayPal payment method")
        {
            

        }
    }
}