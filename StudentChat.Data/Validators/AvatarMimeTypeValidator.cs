using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.Data.Validators
{
    public class AvatarMimeTypeValidator : ValidationAttribute
    {
        private readonly string[] mimeTypes = { "jpeg", "png", "gif" };
        public override bool IsValid(object value)
        {
            var str = value.ToString();
            if (mimeTypes.Contains(str))
            {
                return true;
            }
            return false;

        }
    }
}
