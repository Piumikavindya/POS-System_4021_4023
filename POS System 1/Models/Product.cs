using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System_1.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public ICollection<User> Users { get; set; }

        public int? SaleId { get; set; }

        // Updated property type to nullable int

        [ForeignKey("SaleId")] // Added foreign key attribute
        public Sale Sale { get; set; } // Navigation property to Sale entity
        public int ProductId { get; set; }
        public int SellingQuantity { get; internal set; }
    }
}
