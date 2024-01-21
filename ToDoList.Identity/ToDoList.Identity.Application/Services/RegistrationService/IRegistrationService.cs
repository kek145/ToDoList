using System.Threading.Tasks;
using ToDoList.Identity.Domain.Interfaces;
using ToDoList.Identity.Domain.Requests;
using ToDoList.Identity.Domain.Responses;

namespace ToDoList.Identity.Application.Services.RegistrationService;

public interface IRegistrationService
{
    Task<IBaseResponse<UserResponse>> RegistrationUserAsync(RegistrationRequest request);
}