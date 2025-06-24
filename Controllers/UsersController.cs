using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsersController(AppDbContext context) => _context = context;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Username and password are required");

        var lowerUsername = user.Username.ToLower();

        var existUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == lowerUsername);
        if (existUser != null)
            return BadRequest("Username already exists");

        user.Username = lowerUsername;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username.ToLower() && u.Password == user.Password);
        if (dbUser == null)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(new { username = dbUser.Username });
    }

}
