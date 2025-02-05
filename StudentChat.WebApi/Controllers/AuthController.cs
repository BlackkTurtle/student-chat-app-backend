using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentChat.BLL.Configuration;
using StudentChat.BLL.Services.Contracts;
using StudentChat.BLL.Specifications.AppUserSpecifications;
using StudentChat.BLL.Specifications.FileBaseSpecifications;
using StudentChat.DAL.Repositories.Contracts;
using StudentChat.Data.DTOs.AuthDTOs;
using StudentChat.Data.DTOs.FileBaseDTOs;
using StudentChat.Data.DTOs.UserDtos;
using StudentChat.Data.Entities;
using StudentChat.WebApi.Controllers.Base;
using StudentChat.WebApi.Extensions;

namespace StudentChat.WebApi.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private IOptions<JwtConfiguration> _jwtConfiguration;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IBlobService _blobService;
        private readonly IUnitOfWork unitOfWork;
        public AuthController(ITokenService tokenService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JwtConfiguration> jwtConfiguration, IRefreshTokenService refreshTokenService, IBlobService blobService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfiguration = jwtConfiguration;
            _refreshTokenService = refreshTokenService;
            _blobService = blobService;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                Description = registerDto.Description,
                BirthDate = registerDto.BirthDate,
            };

            if (registerDto.CreateAvatar != null)
            {
                string blobName = _blobService.SaveFileInStorage(registerDto.CreateAvatar.Base64, registerDto.CreateAvatar.FileName, registerDto.CreateAvatar.MimeType);

                var FileBaseEntity = new FileBase
                {
                    BlobName = blobName,
                    MimeType = registerDto.CreateAvatar.MimeType,
                };

                user.FileBase = FileBaseEntity;
            }

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            var token = await _tokenService.GenerateTokenAsync(user);

            var refreshToken = await _refreshTokenService.CreateRefreshToken(user.Id);

            HttpContext.AppendTokenToCookie(token, _jwtConfiguration);

            HttpContext.AppendRefreshTokenToCookie(refreshToken, _jwtConfiguration);

            return Ok(new { Token = token, RefreshToken = refreshToken });
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            if (Request.Cookies.TryGetValue("RefreshToken", out var cookieValue))
            {
                try
                {
                    var refreshToken = await _refreshTokenService.UpdateRefreshToken(cookieValue);

                    var user = await _userManager.FindByIdAsync(refreshToken.UserId.ToString());

                    if (user == null)
                        return Unauthorized();

                    var token = await _tokenService.GenerateTokenAsync(user!);


                    HttpContext.AppendTokenToCookie(token, _jwtConfiguration);

                    HttpContext.AppendRefreshTokenToCookie(refreshToken.Token, _jwtConfiguration);

                    return Ok(new { Token = token, RefreshToken = refreshToken.Token });
                }
                catch (Exception ex)
                {
                    await _refreshTokenService.DeleteRefreshToken(cookieValue);

                    return Unauthorized(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserData()
        {
            var username = User.FindFirst("userName")?.Value;
            if (username == null)
            {
                return Forbid();
            }

            var user = await unitOfWork.AppUserRepository.GetFirstOrDefaultAsync(new UserWithFileBaseByUsernameSpecification(username));

            var resultDto = new GetUserDataDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Description = user.Description,
            };

            if (user.FileBase != null)
            {
                var base64 = _blobService.FindFileInStorageAsBase64(user.FileBase.BlobName);

                var fileBasedto = new GetFileBaseDto()
                {
                    Id = user.FileBase.Id,
                    Base64 = base64,
                    MimeType = user.FileBase.MimeType,
                };

                resultDto.AvatarDto = fileBasedto;
            }

            return Ok(resultDto);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }
    }
}
