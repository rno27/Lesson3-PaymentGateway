using System;
using System.Collections.Generic;
using Abstractions;

namespace Lesson3.UI
{
    public class PaymentContainer : PaymentProcessor
    {
        private List<PaymentProcessor> payments = new List<PaymentProcessor>();

        public void Add(PaymentProcessor payment)
        {
            payments.Add(payment);    
        }

        public void Remove(PaymentProcessor shape)
        {

        }
        
        public void Draw()
        {
            foreach(var payment in payments)
            {
                Console.WriteLine("New payment!");
            }
        }
        

        public void CallBackFunction(){

        }

        public void InitTranzaction(){

        }
    }
}