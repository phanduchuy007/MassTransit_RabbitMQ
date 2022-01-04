using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManage.Models;

namespace StudentManage.Dal.Repository.Interface
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IEnumerable<Student> GetListStudentAscending();
        Student UpdateStudent(Student student, Student studentUpdate);
    }
}
