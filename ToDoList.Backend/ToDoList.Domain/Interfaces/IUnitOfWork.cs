using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Domain.DbSet;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    DbSet<Note> Notes { get; set; }
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}