using Microsoft.EntityFrameworkCore;
using MyApp.Auth.Domain.Entities;

namespace MyApp.Auth.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}