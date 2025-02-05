using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public int? Avatar {  get; set; }
        public DateTime LastSeen { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Description { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = null!;
        public List<Speciality> Specialities { get; set; } = null!;
        public List<Friendship> Friendships { get; set;} = null!;
        public List<Friendship> ToFriendships { get; set; } = null!;
        public List<UserGroup> UserGroups { get; set; } = null!;
        public List<Group> Groups { get; set; } = null!;
        public List<Notification> Notifications { get; set; } = null!;
        public List<Message> Messages { get; set; } = null!;
        public FileBase FileBase { get; set; } = null!;
    }
}
