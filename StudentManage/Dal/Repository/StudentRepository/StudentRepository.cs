using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManage.Dal.Repository.Generic;
using StudentManage.Dal.Repository.Interface;
using StudentManage.Models;

namespace StudentManage.Dal.Repository.StudentRepository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        StudentDataContext _studentContext;
        public StudentRepository(StudentDataContext studentContext) : base(studentContext)
        {
            _studentContext = studentContext;
        }

        public IEnumerable<Student> GetListStudentAscending()
        {
            var ListStudent = _studentContext.Set<Student>().ToList();
            var student = ListStudent.OrderBy(st => st.Name);

            return student;
        }

        public Student UpdateStudent(Student student, Student studentUpdate)
        {
            student.Name = studentUpdate.Name;
            student.Email = studentUpdate.Email;
            student.Address = studentUpdate.Address;

            _studentContext.Set<Student>().Update(student);
            _studentContext.SaveChanges();

            return studentUpdate;
        }
    }

}
