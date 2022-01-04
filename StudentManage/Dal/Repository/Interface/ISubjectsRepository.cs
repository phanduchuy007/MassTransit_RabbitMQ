using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManage.Models;

namespace StudentManage.Dal.Repository.Interface
{
    public interface ISubjectsRepository : IGenericRepository<Subjects>
    {
        IEnumerable<Subjects> GetSubjectByIDStudent(int id);
    }
}
