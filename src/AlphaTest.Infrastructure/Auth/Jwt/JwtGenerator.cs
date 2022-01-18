using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AlphaTest.Core.Users;
using AlphaTest.Infrastructure.Auth.Security;
using AlphaTest.Application.UtilityServices.Authorization;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Infrastructure.Auth.JWT
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AlphaTestUser> _userManager;

        public JwtGenerator(IConfiguration configuration, UserManager<AlphaTestUser> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ALPHATEST:TOKEN_KEY"]));
            _userManager = userManager;
        }

        public async Task<string> GetTokenAsync(AlphaTestUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim("middle_name", user.MiddleName),
                new Claim("user_id", user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = TimeResolver.CurrentTime.AddDays(SecuritySettings.TokenLifetimeInDays)
            };
            JwtSecurityTokenHandler tokenHandler = new();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
