using StudentChat.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class Friendship
    {
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public FrienshipStatus FrienshipStatus { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public AppUser ToAppUser { get; set; } = null!;
    }
}
