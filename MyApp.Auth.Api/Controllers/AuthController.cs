using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Auth.Application.Auth.Commands;
using MyApp.Auth.Application.Auth.Queries;

namespace MyApp.Auth.Api.Controllers;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password);

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken ct)
    {
        var id = await sender.Send(new RegisterCommand(request.Email, request.Password), ct);
        return Ok(new { id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
    {
        var token = await sender.Send(new LoginQuery(request.Email, request.Password), ct);
        return token is null ? Unauthorized() : Ok(new { token });
    }
}