using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Auth.Application.Common.Interfaces;

namespace MyApp.Auth.Application.Auth.Queries;

public record LoginQuery(string Email, string Password) : IRequest<string?>;

public class LoginHandler(
    IApplicationDbContext db,
    IPasswordHasher hasher,
    IJwtTokenGenerator jwtGenerator)
    : IRequestHandler<LoginQuery, string?>
{
    public async Task<string?> Handle(LoginQuery request, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == request.Email, ct);
        if (user is null || !hasher.Verify(request.Password, user.PasswordHash))
            return null;

        return jwtGenerator.GenerateToken(user);
    }
}