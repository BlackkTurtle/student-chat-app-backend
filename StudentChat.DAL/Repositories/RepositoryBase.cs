using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentChat.DAL.Caching.RedisCache;
using StudentChat.DAL.Persistence;
using StudentChat.DAL.Repositories.Contracts;
using StudentChat.DAL.Specification;
using StudentChat.DAL.Specification.Evaluator;

namespace StudentChat.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private readonly AppDbContext _dbContext;
        private readonly IRedisCacheService _redisCacheService;

        protected RepositoryBase(AppDbContext context, IRedisCacheService redisCacheService = null!)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
            _redisCacheService = redisCacheService;
        }

        public T Create(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var tmp = await _dbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;
        }

        public Task CreateRangeAsync(IEnumerable<T> items)
        {
            return _dbContext.Set<T>().AddRangeAsync(items);
        }

        public EntityEntry<T> Update(T entity)
        {
            return _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().UpdateRange(items);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbContext.Set<T>().RemoveRange(items);
        }

        public void Attach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
        }

        public EntityEntry<T> Entry(T entity)
        {
            return _dbContext.Entry(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public Task ExecuteSqlRaw(string query)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(query);
        }

        public async Task<IEnumerable<T>> GetAllAsync(IBaseSpecification<T> specification = null)
        {
            if (specification != null && specification.CacheKey != string.Empty)
            {
                var dataFromCache = await _redisCacheService.GetCachedDataAsync<IEnumerable<T>>(specification.CacheKey);
                if (dataFromCache != null)
                {
                    return dataFromCache;
                }

                var dataFromDb = ApplySpecificationForList(specification);

                if (!dataFromDb.Any())
                {
                    return dataFromDb!;
                }

                await _redisCacheService.SetCachedDataAsync(specification.CacheKey, dataFromDb, specification.CacheMinutes);

                return dataFromDb;
            }
            else
            {
                return ApplySpecificationForList(specification);
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(IBaseSpecification<T> specification = null)
        {
            if (specification != null && specification.CacheKey != string.Empty)
            {
                var dataFromCache = await _redisCacheService.GetCachedDataAsync<T>(specification.CacheKey);
                if (dataFromCache != null)
                {
                    return dataFromCache;
                }

                var dataFromDb = await ApplySpecificationForList(specification).FirstOrDefaultAsync();

                if (dataFromDb == null)
                {
                    return dataFromDb!;
                }

                await _redisCacheService.SetCachedDataAsync(specification.CacheKey, dataFromDb, specification.CacheMinutes);

                return dataFromDb;
            }
            else
            {
                return await ApplySpecificationForList(specification).FirstOrDefaultAsync();
            }
        }

        private IQueryable<T> ApplySpecificationForList(IBaseSpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specification);
        }
    }
}