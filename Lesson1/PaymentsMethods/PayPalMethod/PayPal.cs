using System;
using Common;
 namespace Abstractions{

     public class PayPal: PaymentProcessor,PaymentInfo{
        private string email = "";
        private double amount = 0;

            public void ReadTransactionDetails(){
                Console.WriteLine();
                email = DataReaderHelper.ReadStringValue("Email: ");
                amount = DataReaderHelper.ReadDoubleValue("Amount: ");
            }

            public void CallBackFunction(){
              
            }
            public void DisplayTransactionDetails(){
                Console.WriteLine("Success payment!");
            }
            public void InitTranzaction(){
                ReadTransactionDetails();
            }
     }

 }