using Microsoft.AspNetCore.Mvc; 
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace  WebApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase

{
    private readonly PasswordService _passwordService;

    public AuthController(PasswordService passwordService)
    {
        _passwordService = passwordService;
    }

    [HttpPost("register")]

    public IActionResult Register(RegisterRequest request)
    {
        //checks if user exists

        var exists = UserStore.Users.Any(u => u.Email == request.Email);
        if (exists)
            return BadRequest("User already exists");

        var user = new User
        {
            Id = UserStore.Users.Count + 1,
            Email = request.Email,
            PasswordHash = _passwordService.Hash(request.Password)
        };

        UserStore.Users.Add(user);

        return Ok(new { message = "User registered"});
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var user = UserStore.Users.FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
            return Unauthorized("Invalid credentials");

        var valid = _passwordService.Verify(user.PasswordHash, request.Password);
        if (!valid)
            return Unauthorized("Invalid credentials");

        return Ok(new
        {
            message = "Login successful",
            user = user.Email
        });
    }
}