using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_System_1.Models
{
    public enum UserRole
    {
        Admin,
        Normal
    }

    public class User
    {
        internal bool IsAdmin;
        internal string Salt;

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Product> Products { get; set; }
        public int SaleId { get; internal set; }
    }


}