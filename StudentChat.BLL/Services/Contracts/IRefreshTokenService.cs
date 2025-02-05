using StudentChat.Data.Entities;
using System.Threading.Tasks;

namespace StudentChat.BLL.Services.Contracts
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshToken(int UserId);
        Task<RefreshToken> UpdateRefreshToken(string token);
        Task<bool> DeleteRefreshToken(string token);
    }
}
