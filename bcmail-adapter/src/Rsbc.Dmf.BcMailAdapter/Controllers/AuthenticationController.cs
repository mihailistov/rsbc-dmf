﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Rsbc.Dmf.BcMailAdapter.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Authentication Controller
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Returns a security token.
        /// </summary>
        /// <param name="secret"></param>
        /// <returns>A new JWT</returns>
        [HttpGet("token")]
        [AllowAnonymous]
        public string GetToken([FromQuery] string secret)
        {
            string result = "Invalid secret.";
            string configuredSecret = Configuration["JWT_TOKEN_KEY"];
            if (configuredSecret.Equals(secret))
            {
                byte[] key = Encoding.UTF8.GetBytes(Configuration["JWT_TOKEN_KEY"]);
                Array.Resize(ref key, 32);
                var symmetricSecurityKey = new SymmetricSecurityKey(key);
                
                var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    Configuration["JWT_VALID_ISSUER"],
                    Configuration["JWT_VALID_AUDIENCE"],
                    expires: DateTime.UtcNow.AddYears(1),
                    signingCredentials: creds
                    );
                result = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken); 
            }

            return result;
        }
    }
}
