using StudentChat.DAL.Specification;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.BLL.Specifications.AppUserSpecifications
{
    public class UserWithFileBaseByUsernameSpecification : BaseSpecification<AppUser>
    {
        public UserWithFileBaseByUsernameSpecification(string username) : base(x => x.UserName == username)
        {
            AddInclude(x => x.FileBase);
        }
    }
}
