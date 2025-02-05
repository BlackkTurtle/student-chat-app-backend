using StudentChat.DAL.Caching.RedisCache;
using StudentChat.DAL.Persistence;
using StudentChat.DAL.Repositories.Contracts;
using StudentChat.Data.Entities;

namespace StudentChat.DAL.Repositories
{
    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(AppDbContext context, IRedisCacheService redisCacheService)
            : base(context, redisCacheService)
        {
        }
    }
}
