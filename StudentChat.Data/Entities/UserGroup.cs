using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool Role {  get; set; }
        public AppUser AppUser { get; set; } = null!;
        public Group Group { get; set; } = null!;
    }
}
