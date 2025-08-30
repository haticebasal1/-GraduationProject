using System;
using PhoneCase.Entities.Abstract;

namespace PhoneCase.Data.Abstract;

public interface IUnitOfWork : IDisposable
{
    int Save();
    Task<int> SaveAsync();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class,IEntity;
}
