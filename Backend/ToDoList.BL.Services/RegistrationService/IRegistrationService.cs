using System.Threading.Tasks;
using ToDoList.Domain.Contracts.Request;
using ToDoList.Domain.Contracts.Response;

namespace ToDoList.BL.Services.RegistrationService;

public interface IRegistrationService
{
    Task<GetUserResponse> RegistrationAsync(RegistrationRequest request);
}