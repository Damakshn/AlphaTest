using AlphaTest.Infrastructure.Auth.Security;
using AlphaTest.Infrastructure.Auth.UserManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AlphaTest.Infrastructure.Auth.JWT
{
    public class JwtGenerator
    {
        private readonly SymmetricSecurityKey _key;

        public JwtGenerator(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ALPHATEST_TOKEN_KEY"]));
        }

        public string GetToken(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim("middle_name", user.MiddleName),
                new Claim("user_id", user.Id.ToString())
            };
            SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(SecuritySettings.TokenLifetimeInDays)
            };
            JwtSecurityTokenHandler tokenHandler = new();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
