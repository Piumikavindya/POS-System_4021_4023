using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System_1.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Product.Price * Quantity;

        public int ProductId { get; internal set; }
        public decimal TotalPrice { get; internal set; }
        public decimal Price { get; internal set; }
    }
}
