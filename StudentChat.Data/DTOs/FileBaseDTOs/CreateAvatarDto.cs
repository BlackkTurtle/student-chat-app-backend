using StudentChat.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.DTOs.FileBaseDTOs
{
    public class CreateAvatarDto
    {
        [Required]
        [Base64String]
        public string Base64 { get; set; } = null!;

        [Required]
        public string FileName { get; set; }

        [Required]
        [AvatarMimeTypeValidator(ErrorMessage = "Profile picture extension should be png, jpeg or gif!")]
        public string MimeType { get; set; } = null!;
    }
}
