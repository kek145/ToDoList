﻿using System;
using System.Net;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        public RegistrationService( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var result = await _userRepository.FindByEmailAsync(email);
            if(result != null)
                return true;
            return false;
        }

        public async Task<HttpStatusCode> RegisterAsync(string username, string email, string password)
        {
            try
            {
                var entity = new UserEntity
                {
                    UserName = username,
                    Email = email,
                    Password = password
                };
                await _userRepository.CreateUserAsync(entity);

                return HttpStatusCode.OK;
            }
            catch(Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}
