using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiJWT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiJWT.Services;

public class AuthService
{
    private readonly IConfiguration config;

    public AuthService(IConfiguration config)
    {
        this.config = config;
    }
    

    // Kullanıcı id ve email bilgisiyle JWT token üretir
    public string GenerateToken(int userId, string email)
    {
                // Token içinde taşınacak kullanıcı bilgileri (claims)
        var jwtSettings = config.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.UniqueName, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Token'ın oluşturulma zamanı
        var date = DateTime.UtcNow;

        // JWT token nesnesini oluştur
        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],  // Token'ı yayınlayan
            audience: jwtSettings["Audience"],    // Hedef kitle
            claims: claims,  // Claims bilgileri
            notBefore: date, // Ne zamandan itibaren geçerli
            expires: date.AddMinutes(60), // Ne zamana kadar geçerli
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));  // İmzalama algoritması

        return new JwtSecurityTokenHandler().WriteToken(token);        // Token'ı string olarak yazdır ve döndür
    }
}
