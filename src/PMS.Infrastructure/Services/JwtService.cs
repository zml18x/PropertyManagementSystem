﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Core.Entities;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Exceptions;
using PMS.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;



        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public JwtDto CreateToken(User user)
        {
            if (user == null || user.Id == Guid.Empty)
                throw new ArgumentException("Invalid user");


            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Key")?.Value ?? 
                throw new MissingConfigurationException("JWT key configuration is missing or empty"));

            var now = DateTime.UtcNow;

            var expires = now.AddMinutes(int.Parse(_configuration.GetSection("JWT:ExpiryMinutes")?.Value ?? 
                throw new MissingConfigurationException("JWT expiry configuration is missing or empty")));

            var issuer = _configuration.GetSection("JWT:Issuer").Value;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,now.Ticks.ToString())
            };

            var signingCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken
                (
                issuer: issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
                ); ;

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto(token, expires.Ticks);
        }
    }
}