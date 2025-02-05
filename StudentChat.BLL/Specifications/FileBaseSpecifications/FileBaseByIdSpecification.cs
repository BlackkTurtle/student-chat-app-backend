using StudentChat.DAL.Specification;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.BLL.Specifications.FileBaseSpecifications
{
    public class FileBaseByIdSpecification : BaseSpecification<FileBase>
    {
        public FileBaseByIdSpecification(int Id) : base(x => x.Id == Id) 
        { }
    }
}
