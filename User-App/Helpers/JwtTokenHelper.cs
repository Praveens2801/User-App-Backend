namespace User_App.Helpers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public static class JwtTokenHelper
    {
        public static string GenerateToken(string username, int userId, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");

            var keyString = jwtSettings.GetValue<string>("Key");

            if (string.IsNullOrWhiteSpace(keyString))
                throw new Exception("JWT key is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyString));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, username)
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
