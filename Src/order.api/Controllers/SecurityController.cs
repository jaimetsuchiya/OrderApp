using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using order.api.Domain.Commands;
using order.api.Domain.Entities;
using order.api.Infrastructure;

namespace order.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        [HttpGet]
        [Route("guest")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GuestToken()
        {
            return Ok(CreateToken("Guest", ""));
        }

        [HttpGet]
        [Route("admin")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult AdminToken()
        {
            return Ok( CreateToken("Admin", "Admin") );
        }

        private string CreateToken(string userName, string role)
        {
            //Gera o token JWT
            var audience = "b5efeeaf2d46854a78cbe4a3ca50ad6b";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("e3c74e196132b86680eb4f1a9105d481a0fc772a5eef05a1bf3f341618361af6");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "SWBrasil",
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMonths(6),
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
