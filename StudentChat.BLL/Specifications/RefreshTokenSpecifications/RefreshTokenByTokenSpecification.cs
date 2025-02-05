using StudentChat.DAL.Specification;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.BLL.Specifications.RefreshTokenSpecifications
{
    public class RefreshTokenByTokenSpecification : BaseSpecification<RefreshToken>
    {
        public RefreshTokenByTokenSpecification(string token) : base(x => x.Token == token)
        {

        }
    }
}
