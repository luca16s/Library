namespace Api.Extensions
{
    using Api.Models;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    public static class JwtExtensions
    {
        public static string CreateJwtToken(
            this ClaimsIdentity? _,
            JwtSettings jwtSettings,
            SigningSettings signSettings
        )
        {
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signSettings.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
