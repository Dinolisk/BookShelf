using BookQuotesApp.Api.Data;
using BookQuotesApp.Api.Dtos;
using BookQuotesApp.Api.Models;
using BookQuotesApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookQuotesApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext db, ITokenService tokens) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var username = request.Username.Trim();

        if (await db.Users.AnyAsync(u => u.Username == username))
            return Conflict(new { message = "Användarnamnet är upptaget." });

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        var (token, expiresAt) = tokens.CreateToken(user);
        return Ok(new AuthResponse(token, user.Username, expiresAt));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var username = request.Username.Trim();
        var user = await db.Users.SingleOrDefaultAsync(u => u.Username == username);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "Fel användarnamn eller lösenord." });

        var (token, expiresAt) = tokens.CreateToken(user);
        return Ok(new AuthResponse(token, user.Username, expiresAt));
    }
}
