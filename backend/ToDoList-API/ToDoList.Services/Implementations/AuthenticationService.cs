﻿using System;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using ToDoList.Services.Helpers;
using System.Collections.Generic;
using ToDoList.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ToDoList.Configurations.Configurations;

namespace ToDoList.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfiguration _options;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IUserRepository userRepository, IOptions<JwtConfiguration> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            if (user is null)
                return null!;

            bool isPasswordValid = HashDataHelper.VerifyPassword(password, user.Password);
            if(!isPasswordValid)
                return null!;

            string token = GenerateToken(user);

            return token;
        }

        private string GenerateToken(UserEntity entity)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userid", entity.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, entity.Email),
                new Claim(JwtRegisteredClaimNames.Email, entity.Email)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}