﻿using System.Threading.Tasks;
using ToDoList.Domain.Result;
using ToDoList.Domain.Request;
using ToDoList.Domain.Abstractions;

namespace ToDoList.Application.Services.RegistrationService;

public interface IRegistrationService
{
    Task<IBaseResponse<UserResponse>> RegistrationAsync(RegistrationRequest request);
}