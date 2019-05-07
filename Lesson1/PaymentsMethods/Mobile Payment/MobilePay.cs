using System;
using Common;
using Abstractions;
using Lesson1;

namespace PaymentMethod{

    public class MobilePay: PaymentProcessor,PaymentInfo {

        private string Name = "";
        private string CardNumber = "";
        private string CVV = "";    
        private int OrderNumber = 0;
        private int OrderId = 0;
        private decimal amount = 0;

            public void ReadTransactionDetails(){
                Console.WriteLine();
                Console.WriteLine("Order Number:{0}",OrderNumber,OrderId);
                Name = DataReaderHelper.ReadStringValue("Phone number: ");
                CardNumber = DataReaderHelper.ReadStringValue("Amount: ");
                CVV = DataReaderHelper.ReadStringValue("Security code: ");
            }
            
            public void DisplayTransactionDetails(){
               
            }
            
            public void CallBackFunction(){
                Console.Clear();
                Console.WriteLine("Successful Payment !");
                Console.ReadLine();
              
            }
            public void InitTranzaction(){
                Random random = new Random();
                OrderNumber = random.Next(100000);
                OrderId = random.Next(100000);
                ReadTransactionDetails();
                CallBackFunction();
               
            }
    }

}
