using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Auth.Application.Common.Interfaces;
using MyApp.Auth.Domain.Entities;

namespace MyApp.Auth.Application.Auth.Commands;

public record RegisterCommand(string Email, string Password) : IRequest<int>;

public class RegisterHandler(IApplicationDbContext db, IPasswordHasher hasher)
    : IRequestHandler<RegisterCommand, int>
{
    public async Task<int> Handle(RegisterCommand request, CancellationToken ct)
    {
        var exists = await db.Users.AnyAsync(u => u.Email == request.Email, ct);
        if (exists)
            throw new InvalidOperationException("Email already registered.");

        var user = new User
        {
            Email = request.Email,
            PasswordHash = hasher.Hash(request.Password)
        };

        db.Users.Add(user);
        await db.SaveChangesAsync(ct);
        return user.Id;
    }
}