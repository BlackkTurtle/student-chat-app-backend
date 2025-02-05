using StudentChat.Data.Entities;
using System.Threading.Tasks;

namespace StudentChat.BLL.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(AppUser user);
    }
}
