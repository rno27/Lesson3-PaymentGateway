using System;
using Common;

namespace Abstractions{

    public class BankPay: PaymentProcessor,PaymentInfo {

        private string Name = "";
        private string CardNumber = "";
        private string CVV = "";    
        private int OrderNumber = 0;
        private int OrderId = 0;

            public void ReadTransactionDetails(){
                Console.WriteLine();
                Name = DataReaderHelper.ReadStringValue("Customer Name: ");
                CardNumber = DataReaderHelper.ReadStringValue("Card Number: ");
                CVV = DataReaderHelper.ReadStringValue("CVV: ");
            }
            
            public void DisplayTransactionDetails(){
                Console.WriteLine("Succes payment !");
            }

            public void CallBackFunction(){

              
            }
            public void InitTranzaction(){
                ReadTransactionDetails();
                Random random = new Random();
                OrderNumber = random.Next(100000);
                OrderId = random.Next(100000);
            }
    }

}



