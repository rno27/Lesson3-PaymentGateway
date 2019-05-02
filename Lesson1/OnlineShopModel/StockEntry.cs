using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson1
{
    public class StockEntry
    {
        public int Id {get; set;}
        public Product Product
        {
            get; 
            private set;
        }
        public ulong Qty
        {
            get;
            private set;
        }
        public StockEntry(Product product, ulong availableQty)
        {
            Product = product;
            Qty = availableQty;

        }

        public void AddQty( ulong quantityToAdd)
        {
            Qty += quantityToAdd;
        }

        public void RemoveQty(ulong qtyToRemove)
        {
            if (qtyToRemove > Qty)
            {
                throw new ArgumentException($"Insufficient stock. ");
            }
            
            Qty -= qtyToRemove;
        }

    }
}