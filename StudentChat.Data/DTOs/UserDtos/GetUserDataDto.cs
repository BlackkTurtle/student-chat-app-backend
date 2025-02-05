using StudentChat.Data.DTOs.FileBaseDTOs;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.DTOs.UserDtos
{
    public class GetUserDataDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Description { get; set; }
        public GetFileBaseDto AvatarDto { get; set; }
    }
}
