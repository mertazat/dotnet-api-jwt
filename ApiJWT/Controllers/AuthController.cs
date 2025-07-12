using ApiJWT.Models;
using ApiJWT.Repositories;
using ApiJWT.Services;
using ApiJWT.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiJWT.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService authService;
    private readonly IUsersRepository usersRepository;

    public AuthController(AuthService authService, IUsersRepository usersRepository)
    {
        this.authService = authService;
        this.usersRepository = usersRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Models.LoginRequest request)
    {
        try
        {
            var user = await usersRepository.GetUserByEmail(request.Email);

            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized(new { message = "E-posta veya şifreniz hatalı." });
            }

            var token = authService.GenerateToken(user.Id, user.Email);
            return Ok(new { Token = token });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Bilinmeyen hata." });
        }
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignUpRequest request)
    {
        try
        {
            var user = await usersRepository.CreateUser(request);

            var token = authService.GenerateToken(user.Id, user.Email);

            return Ok(new { Token = token });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Bilinmeyen hata." });
        }
    }
}