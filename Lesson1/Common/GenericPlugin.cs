using System;
using Abstractions;

namespace Common
{
    public abstract class GenericPlugin<T> : PaymentPlugin where T : PaymentProcessor,PaymentInfo , new() 
    {
        private string PaymentMethod = "";

        public GenericPlugin(string PaymentMethod)
        {
            this.PaymentMethod = PaymentMethod;
        }
        public string GetName()
        {
            return PaymentMethod;
        }

        public PaymentProcessor Pay()
        {
            var newPay = new T();
            newPay.InitTranzaction();
            return newPay;
        }
    }
}