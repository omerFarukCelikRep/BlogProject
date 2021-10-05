using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IService<T> where T:class, IEntity
    {
        Task<bool> Delete(T entity);
        Task<bool> Delete(Guid id);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<T> GetByID(Guid id);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);
    }
}
