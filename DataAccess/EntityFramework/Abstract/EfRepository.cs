using DataAccess.Abstract;
using DataAccess.EntityFramework.Context;
using Entity.Abstract;
using Entity.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework.Abstract
{
    public abstract class EfRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly BlogDbContext _context;

        protected DbSet<T> _table;
        protected EfRepository(BlogDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _table.AddAsync(entity);
            return await Save() > 0;
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _table.AnyAsync(expression);
        }

        public virtual async Task<bool> Delete(T entity)
        {
            entity.Status = Status.Deleted;
            return await Update(entity);
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var entity = await _table.FindAsync(id);
            return await Delete(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _table.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null)
        {
            return await _table.Where(expression).ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByID(Guid id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> Update(T entity)
        {
            _table.Update(entity);
            return await Save() > 0;
        }
    }
}
