using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Domain.Service;
using System.Threading.Tasks;

namespace Domain.Test.Users

{
    public class PasswordServiceUser : IPasswordService
    {
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return hashedPassword == HashPassword(providedPassword);
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
