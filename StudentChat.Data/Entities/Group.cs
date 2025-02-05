using StudentChat.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public GroupType GroupType { get; set; }
        public int? ImageId { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; }
        public List<UserGroup> UserGroups { get; set; } = null!;
        public List<Message> Messages { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
        public FileBase FileBase { get; set; } = null!;
    }
}
