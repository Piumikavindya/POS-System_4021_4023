using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using POS_System_1.DatabaseContext;

namespace POS_System_1.Models
{
    public class UserRepository
    {
        private readonly POSContext dbContext;

        public UserRepository()
        {
            dbContext = new POSContext();
        }

        public bool VerifyPassword(string username, string password)
        {
            User user = dbContext.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                // Verify the entered password against the stored password
                string hashedPassword = HashPassword(password, user.Salt);

                return hashedPassword == user.Password;
            }

            return false;
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(20);
                byte[] combinedBytes = new byte[36];

                Array.Copy(saltBytes, 0, combinedBytes, 0, 16);
                Array.Copy(hashBytes, 0, combinedBytes, 16, 20);

                return Convert.ToBase64String(combinedBytes);
            }
        }
    }
}
