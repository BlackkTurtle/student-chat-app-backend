using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.DTOs.FileBaseDTOs
{
    public class GetFileBaseDto
    {
        public int Id { get; set; }
        public string Base64 { get; set; } = null!;

        public string MimeType { get; set; } = null!;
    }
}
