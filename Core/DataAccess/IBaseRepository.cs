﻿using Core.Entity.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

    }
}
