using StudentChat.BLL.Services.Contracts;
using StudentChat.BLL.Specifications.RefreshTokenSpecifications;
using StudentChat.DAL.Repositories.Contracts;
using StudentChat.Data.Entities;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace StudentChat.BLL.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork unitOfWork;
        public RefreshTokenService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> CreateRefreshToken(int UserId)
        {
            var token = GenerateRefreshTokenString();

            var refreshToken = new RefreshToken
            {
                Token = token,
                ExpiresOn = DateTime.UtcNow.AddMinutes(10),
                UserId = UserId
            };

            await unitOfWork.RefreshTokenRepository.CreateAsync(refreshToken);

            var isSuccessResult = await unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccessResult)
            {
                const string errorMsg = "Cannot save changes in the database after refreshtoken creation!";
                throw new Exception(errorMsg);
            }

            return token;
        }

        public async Task<bool> DeleteRefreshToken(string token)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetFirstOrDefaultAsync(new RefreshTokenByTokenSpecification(token));

            unitOfWork.RefreshTokenRepository.Delete(refreshToken);

            var isSuccess = await unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccess)
            {
                const string errorMsg = "Cannot save changes in the database after refreshtoken delete!";
                throw new Exception(errorMsg);
            }

            return true;
        }

        public async Task<RefreshToken> UpdateRefreshToken(string token)
        {
            var refreshToken = await unitOfWork.RefreshTokenRepository.GetFirstOrDefaultAsync(new RefreshTokenByTokenSpecification(token));

            if (refreshToken.ExpiresOn < DateTime.UtcNow)
            {
                const string errorMsg = "Token has expired";
                throw new Exception(errorMsg);
            };

            string newToken = GenerateRefreshTokenString();

            refreshToken.Token = newToken;
            refreshToken.ExpiresOn = DateTime.UtcNow.AddMinutes(10);

            unitOfWork.RefreshTokenRepository.Update(refreshToken);

            var isSuccess = await unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccess)
            {
                const string errorMsg = "Cannot save changes in the database after refreshtoken update!";
                throw new Exception(errorMsg);
            }

            return refreshToken;
        }

        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];

            using (var numberGenerator = RandomNumberGenerator.Create())
            {
                numberGenerator.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}
