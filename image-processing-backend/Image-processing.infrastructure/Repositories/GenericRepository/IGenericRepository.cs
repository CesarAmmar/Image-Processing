using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.infrastructure.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, Expression <Func<T,bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T,object>> includes = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
