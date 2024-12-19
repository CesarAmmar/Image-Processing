using Image_processing.infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.infrastructure.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly ImageProcessingDbContext _dbContext;

        public GenericRepository(ImageProcessingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _ = _dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
            if(includes != null)
            {
                query = includes(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            if (includes != null)
            {
                query = includes(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _ = _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
