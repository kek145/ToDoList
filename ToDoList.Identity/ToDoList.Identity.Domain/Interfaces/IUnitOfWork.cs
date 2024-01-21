using System;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoList.Identity.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}