using Core.Common.Entities;
using Core.Common.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public BaseRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public virtual async Task<T?> GetAsync(long id)
    {
        using var _context = _dbFactory.CreateDbContext();

        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    public virtual async Task<List<T>> GetListAsync()
    {
        using var _context = _dbFactory.CreateDbContext();

        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<long> AddAsync(T entity)
    {
        using var _context = _dbFactory.CreateDbContext();

        var entityEntry = await _context.Set<T>().AddAsync(entity);

        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        using var _context = _dbFactory.CreateDbContext();

        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<T>().Update(entity);

        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        using var _context = _dbFactory.CreateDbContext();

        _context.Set<T>().Remove(entity);

        await _context.SaveChangesAsync();
    }

    public async Task<int> GetCountAsync()
    {
        using var _context = _dbFactory.CreateDbContext();

        return await _context.Set<T>().CountAsync();
    }
}