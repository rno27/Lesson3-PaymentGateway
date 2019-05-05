using System;
using Common;
using Abstractions;
using Lesson1;


 namespace PaymentMethod{

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