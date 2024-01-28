using System;
using Microsoft.AspNetCore.Identity;
using Domain.Model;
using Domain.Service;

namespace JeBalance.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<object> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<object>();
    }

    public string HashPassword(string password)
    {

        return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        return verificationResult == PasswordVerificationResult.Success ||
               verificationResult == PasswordVerificationResult.SuccessRehashNeeded;
    }
}