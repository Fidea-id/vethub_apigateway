using Domain.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Utils
{
    public static class JwtUtil
    {
        public static string Issuer = "VetHubAPIAuthentication";
        public static string Audience = "VetHubAPIClient";
        public static string Key = "wD7q7RPi9OjQAGP16Bi49hwiUmE2fpUW";

        public static string SetSessionToken(Users user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "JwtUser_" + user.Id.ToString()),
                new Claim (ClaimTypes.Role, user.Roles),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim ("Entity", user.Entity),
                new Claim("Id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(Issuer,
                Audience,
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: signIn
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
