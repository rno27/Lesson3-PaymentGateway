using System;
using System.Collections.Generic;


namespace Lesson1
{
    public class Customer
    {
        public int Id{ get; set;}
        public string Name{get; set;}
        public string PhoneNumber{ get; set;}
        public string Password{get;set;}
        public Order Order{get;set;}

        public void RemoveOrderWithIndex(int index, Order order){
                    order.RemoveEntryWithIndex(index);            
        }    
        
        public Order getOrder(){
            return Order;
        }

        public void AddItemToOrder (Product p, ulong qty){
            Order.AddItem(p,qty);
        }

        public List<OrderEntry> getOrderEntries(){
            return Order.retriveOrderEntries();
        }

        public decimal TotalValue(){
            return Order.TotalValue;
        }
    
    }
}