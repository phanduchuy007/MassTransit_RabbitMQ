using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManage.Models;

namespace StudentManage.Dal.Repository.Interface
{
    public interface IStudentData
    {
        List<Student> GetStudents();
        Student GetStudent(int id);
        Student AddStudent(Student student);
        void DeleteStudent(Student student);
        Student EditStudent(Student student);
    }
}
