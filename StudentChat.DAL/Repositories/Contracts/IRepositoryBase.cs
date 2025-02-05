using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using StudentChat.DAL.Specification;

namespace StudentChat.DAL.Repositories.Contracts
{
    public interface IRepositoryBase<T>
        where T : class
    {

        T Create(T entity);

        Task<T> CreateAsync(T entity);

        Task CreateRangeAsync(IEnumerable<T> items);

        EntityEntry<T> Update(T entity);

        public void UpdateRange(IEnumerable<T> items);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> items);

        void Attach(T entity);

        void Detach(T entity);

        EntityEntry<T> Entry(T entity);

        public Task ExecuteSqlRaw(string query);

        Task<IEnumerable<T>> GetAllAsync(IBaseSpecification<T> specification = null);

        Task<T> GetFirstOrDefaultAsync(IBaseSpecification<T> specification = null);
    }
}
