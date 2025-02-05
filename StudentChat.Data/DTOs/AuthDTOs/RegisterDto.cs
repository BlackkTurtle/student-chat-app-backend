using StudentChat.Data.DTOs.FileBaseDTOs;
using StudentChat.Data.Entities;
using StudentChat.Data.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentChat.Data.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public CreateAvatarDto CreateAvatar { get; set; } = null!;

        [MinLength(8)]
        [Required]
        [PasswordValidator(ErrorMessage = "Password field must contain at least 1 digit,1 Upper Case and 1 Lower Case Letter")]
        public string Password { get; set; } = null!;
    }
}
