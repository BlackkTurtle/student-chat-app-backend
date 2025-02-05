using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Entities
{
    public class FileBase
    {
        public int Id { get; set; }
        public string BlobName { get; set; } = null!;
        [NotMapped]
        public string Base64 { get; set; } = null!;
        public string MimeType { get; set; } = null!;
        public List<AppUser> AppUsers { get; set; } = null!;
        public List<Group> Groups { get; set; } = null!;
        public List<Message> Messages { get; set; } = null!;
    }
}
