using StudentChat.DAL.Caching.RedisCache;
using StudentChat.DAL.Persistence;
using StudentChat.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRedisCacheService redisCacheService;
        private readonly AppDbContext _dbContext;

        private IAppUserRepository appUserRepository;
        private IFileBaseRepository fileBaseRepository;
        private IFriendshipRepository friendshipRepository;
        private IGroupRepository groupRepository;
        private IMessageRepository messageRepository;
        private INotificationRepository notificationRepository;
        private IRefreshTokenRepository refreshTokenRepository;
        private ISpecialityRepository specialityRepository;
        private IUserGroupRepository userGroupRepository;

        public UnitOfWork(IRedisCacheService redisCacheService, AppDbContext dbContext)
        {
            this.redisCacheService = redisCacheService;
            _dbContext = dbContext;
        }

        public IAppUserRepository AppUserRepository
        {
            get
            {
                if (appUserRepository is null)
                {
                    appUserRepository = new AppUserRepository(_dbContext, redisCacheService);
                }

                return appUserRepository;
            }
        }

        public IFileBaseRepository FileBaseRepository
        {
            get
            {
                if (fileBaseRepository is null)
                {
                    fileBaseRepository = new FileBaseRepository(_dbContext, redisCacheService);
                }

                return fileBaseRepository;
            }
        }

        public IFriendshipRepository FriendshipRepository
        {
            get
            {
                if (friendshipRepository is null)
                {
                    friendshipRepository = new FriendshipRepository(_dbContext, redisCacheService);
                }

                return friendshipRepository;
            }
        }

        public IGroupRepository GroupRepository
        {
            get
            {
                if (groupRepository is null)
                {
                    groupRepository = new GroupRepository(_dbContext, redisCacheService);
                }

                return groupRepository;
            }
        }

        public IMessageRepository MessageRepository
        {
            get
            {
                if (messageRepository is null)
                {
                    messageRepository = new MessageRepository(_dbContext, redisCacheService);
                }

                return messageRepository;
            }
        }

        public INotificationRepository NotificationRepository
        {
            get
            {
                if (notificationRepository is null)
                {
                    notificationRepository = new NotificationRepository(_dbContext, redisCacheService);
                }

                return notificationRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (refreshTokenRepository is null)
                {
                    refreshTokenRepository = new RefreshTokenRepository(_dbContext, redisCacheService);
                }

                return refreshTokenRepository;
            }
        }

        public ISpecialityRepository SpecialityRepository
        {
            get
            {
                if (specialityRepository is null)
                {
                    specialityRepository = new SpecialityRepository(_dbContext, redisCacheService);
                }

                return specialityRepository;
            }
        }

        public IUserGroupRepository UserGroupRepository
        {
            get
            {
                if (userGroupRepository is null)
                {
                    userGroupRepository = new UserGroupRepository(_dbContext, redisCacheService);
                }

                return userGroupRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
