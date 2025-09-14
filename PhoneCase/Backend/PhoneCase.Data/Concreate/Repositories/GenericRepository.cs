using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneCase.Data.Abstract;
using PhoneCase.Entities.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Dtos.FavoriteDtos;

namespace PhoneCase.Data.Concreate.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly AppDbContext _appDbContext;

    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _appDbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }
    public void BulkUpdate(IEnumerable<TEntity> entities)
    {
        _appDbContext.Set<TEntity>().UpdateRange(entities);
    }

    public async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool includeDeleted = false,
         params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _appDbContext.Set<TEntity>();
        if (includeDeleted)
        {
            query = query.IgnoreQueryFilters();
        }
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => include(current));
        }
        var count = await query.CountAsync();
        return count;
    }

    public void Delete(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _appDbContext.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
    Expression<Func<TEntity, bool>>? predicate = null,
    int? top = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    bool? isDeleted = null,
    params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _appDbContext.Set<TEntity>();

        if (isDeleted.HasValue && typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => ((ISoftDeletable)e).IsDeleted == isDeleted.Value);
        }

        if (predicate is not null)
            query = query.Where(predicate);

        if (orderBy is not null)
            query = orderBy(query);

        if (top is not null)
            query = query.Take(top.Value);

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => include(current));

        return await query.ToListAsync();
    }


    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
    {
        IQueryable<TEntity> query = _appDbContext.Set<TEntity>();
        if (includeDeleted)
        {
            query = query.IgnoreQueryFilters();
        }
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }
        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => include(current));
        }
        var entity = await query.FirstOrDefaultAsync();
        return entity!;
    }

    public void Update(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Update(entity);
    }

    void IGenericRepository<TEntity>.BulkUDelete(IEnumerable<TEntity> entities)
    {
        _appDbContext.Set<TEntity>().RemoveRange(entities);
    }
}
