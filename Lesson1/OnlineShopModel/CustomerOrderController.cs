using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1
{

    public class CustomerOrderController
    {
        private Stock currentStock;
        
        public decimal TotalIncome
        {
            get;
            private set;
        }

        private void VerifyCurrentOrderExists(Customer currentCustomer)
        {
            if (currentCustomer.getOrder() == null)
            {
                throw new InvalidOperationException("Order not started");
            }
        }

        public CustomerOrderController(Stock stock)
        {
            this.currentStock = stock;
        }
        
        public void StartNewOrder(Customer customer)
        {
            if (customer.getOrder() != null)
            {
                throw new InvalidOperationException("Cannot start a new order while another is still in progress");
            }     
            customer.Order = new Order(currentStock);
        }
        public void AddItemToOrder(Product productToAdd, ulong qty, Customer customer)
        {            
            VerifyCurrentOrderExists(customer);
            customer.AddItemToOrder(productToAdd, qty);                          
        }

        public void RemoveEntryFromOrder(Customer customer,int entryIndex)
        {
            VerifyCurrentOrderExists(customer);
            
            try
            {
               customer.RemoveOrderWithIndex(entryIndex,customer.Order);
            }
            catch(IndexOutOfRangeException ex)
            {
                   throw new InvalidOperationException("This entry does not exist in the order.", ex);
            }              
        }

        public void RemoveItemFromOrder(Customer customer, int productId, ulong qtyToRemove)
        {
            VerifyCurrentOrderExists(customer);
            var currentEntry = customer.getOrderEntries().Where(entry => entry.ProductId == productId)
                                                        .SingleOrDefault();
            if (currentEntry == null)
            {
                throw new ArgumentException("The given product is not in the order");   
            }

            if (qtyToRemove > currentEntry.Qty)
            {
                throw new ArgumentException("Too many products to remove.", "qtyToRemove");
            }

            customer.Order.UpdateProductQty(productId, currentEntry.Qty - qtyToRemove);
        }

        public IEnumerable<OrderEntry> ListOrderEntries(Customer customer)
        {
          return  customer.getOrderEntries();            
        }

        public OrderSummary GetOrderSummary(Customer customer)
        {
            return new OrderSummary{Price = customer.Order.Amount, VAT = customer.Order.VAT, TotalValue = customer.Order.TotalValue };
        }

        public void FinalizeOrder(Customer customer)
        {
            VerifyCurrentOrderExists(customer);
            var orderEntries = customer.getOrderEntries();
            
            foreach(var orderEntry in orderEntries)
            {
                currentStock.GetFromStock(orderEntry.ProductId, orderEntry.Qty);                
            }

            TotalIncome += customer.TotalValue();
            customer.Order = null;
            if( customer.Order ==  null)
                Console.Write("order deleted");
        }

        public void CancelOrder(Customer customer)
        {
            customer.Order = null;
        }

    }
}