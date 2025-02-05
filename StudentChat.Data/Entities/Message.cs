using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? MessageId { get; set; }
        public int GroupId { get; set; }
        public bool Pinned { get; set; }
        public bool Forwarded { get; set; }
        public int? ForwardedMessageId { get; set; }
        public List<Notification> Notifications { get; set; } = null!;
        public List<FileBase> FileBases { get; set; }
        public Group Group { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public Message MessageParent { get; set; } = null!;
        public List<Message> Messages { get; set; } = null!;
        public Message ForwardedMessage { get; set; } = null!;
        public List<Message> ForwardedMessages { get; set; } = null!;
    }
}
