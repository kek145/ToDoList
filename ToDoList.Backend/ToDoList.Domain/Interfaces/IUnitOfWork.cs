using System;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    INoteRepository Notes { get; set; }
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}