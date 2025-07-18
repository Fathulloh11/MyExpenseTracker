﻿using Microsoft.AspNetCore.Mvc;
using MyExpenseTracker.Models;
using MyExpenseTracker.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Username and password are required.");

        var token = await _authService.LoginAsync(request.Username, request.Password);
        return token == null ? Unauthorized("Login failed.") : Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Username and password are required.");

        var success = await _authService.RegisterAsync(request.Username, request.Password);
        return success ? Ok("User created") : BadRequest("Username already exists.");
    }
}
