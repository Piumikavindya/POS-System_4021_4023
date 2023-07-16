using System;
using System.Collections.Generic;

namespace POS_System_1.Models
{
    public class Sale
    {
        public int Id { get; set; }
        
      
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public string ProductNames { get; set; }
        //  public int SaleId { get; private set; }
        public ICollection<Product> Products { get; set; }
        

        public Sale()
        {
            Products = new List<Product>(); 
            TotalPrice = 0;
            TotalQuantity = 0;
            ProductNames = string.Empty;
            // SaleId = 1;


        }

        public class ShoppingCartItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int SellingQuantity { get; set; }
            public decimal ItemPrice => Price * SellingQuantity;
        }

    }
}