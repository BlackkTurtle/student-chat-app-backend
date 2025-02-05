using System;

namespace StudentChat.Data.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresOn { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
    }
}
