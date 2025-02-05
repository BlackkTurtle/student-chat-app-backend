using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }
        public bool IsRead { get; set; }
        public AppUser AppUser { get; set; } = null!;
        public Message Message { get; set; } = null!;
    }
}
