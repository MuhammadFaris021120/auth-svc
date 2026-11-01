using Microsoft.AspNetCore.Identity;
using MyApp.Auth.Application.Common.Interfaces;
using MyApp.Auth.Domain.Entities;

namespace MyApp.Auth.Infrastructure.Auth;

public class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password) => _hasher.HashPassword(null!, password);

    public bool Verify(string password, string hash)
        => _hasher.VerifyHashedPassword(null!, hash, password) != PasswordVerificationResult.Failed;
}