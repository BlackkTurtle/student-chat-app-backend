﻿using StudentChat.DAL.Caching.RedisCache;
using StudentChat.DAL.Persistence;
using StudentChat.DAL.Repositories.Contracts;
using StudentChat.Data.Entities;

namespace StudentChat.DAL.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context, IRedisCacheService redisCacheService)
            : base(context, redisCacheService)
        {
        }
    }
}
