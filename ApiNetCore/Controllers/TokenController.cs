using Api.Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult AuthenticationPost(LoginUsuario loginUsuario)
        {
            //if valid user
            if (IsValidUser(loginUsuario))
            {
                var token = GenerarToken();
                return Ok(new { token });
            }
            return NotFound();
        }
        private bool IsValidUser(LoginUsuario loginUsuario)
        {
            return true;
        }
        private string GenerarToken()
        {
            //header
            var symmetricSegurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSegurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "Nico"),
                new Claim(ClaimTypes.Email, "nico@gmail.com"),
                new Claim(ClaimTypes.Role, "Administrador")
            };

            //Payload
            var payload = new JwtPayload
            (
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claims,
               DateTime.Now,
               DateTime.UtcNow.AddMinutes(2)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
