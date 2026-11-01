using MyApp.Auth.Domain.Entities;

namespace MyApp.Auth.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}