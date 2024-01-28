using System;
namespace Domain.Service

{
    public interface IPasswordService
    {
        bool VerifyPassword(string hashedPassword, string providedPassword);
        string HashPassword(string password);
    }
}
