using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.DAL.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUserRepository { get; }
        IFileBaseRepository FileBaseRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }
        IGroupRepository GroupRepository { get; }
        IMessageRepository MessageRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        ISpecialityRepository SpecialityRepository { get; }
        IUserGroupRepository UserGroupRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
