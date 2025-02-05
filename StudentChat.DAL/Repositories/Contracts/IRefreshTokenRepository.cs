using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.DAL.Repositories.Contracts
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
    }
}
