using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Application.UtilityServices.Security;

namespace AlphaTest.Infrastructure.Auth.Security
{
    public class PasswordGenerator : IPasswordGenerator
    {

        private readonly PasswordOptions _passwordOptions;


        public PasswordGenerator(PasswordOptions passwordOptions)
        {
            _passwordOptions = passwordOptions;
        }

        // https://www.ryadel.com/en/c-sharp-random-password-generator-asp-net-core-mvc/
        // ToDo review
        public string GeneratePassword()
        {
            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            List<char> chars = new();
            // ToDo secure random
            Random rand = new();

            if (_passwordOptions.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (_passwordOptions.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (_passwordOptions.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (_passwordOptions.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < _passwordOptions.RequiredLength
                || chars.Distinct().Count() < _passwordOptions.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
