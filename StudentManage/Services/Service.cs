using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManage.Dal.Repository.Interface;
using StudentManage.Models;

namespace StudentManage.Services
{
    public class Service
    {
        StudentDataContext _studentData;
        IUnitOfWork _unitOfWork;

        public Service(StudentDataContext studentData, IUnitOfWork unitOfWork)
        {
            _studentData = studentData;
            _unitOfWork = unitOfWork;
        }

        public void AddDataTable(Operation operation)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    // var student = (from st in _unitOfWork.Student.Gets() where st.Email == operation.Email select st).FirstOrDefault();
                    var student = _unitOfWork.Student.Gets().FirstOrDefault(st => st.Email == operation.Email);

                    if (student == null)
                    {
                        _unitOfWork.Student.Add(new Student()
                        {
                            Name = operation.Name,
                            Address = operation.Address,
                            Email = operation.Email
                        });
                        _unitOfWork.Submit();

                        // var checkStudent = (from st in _unitOfWork.Student.Gets() where st.Email == operation.Email select st).FirstOrDefault();
                        var checkStudent = _unitOfWork.Student.Gets().FirstOrDefault(st => st.Email == operation.Email);

                        _unitOfWork.Subject.Add(new Subjects()
                        {
                            IDStudent = checkStudent.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _unitOfWork.Submit();
                    }

                    if (student != null)
                    {
                        _unitOfWork.Subject.Add(new Subjects()
                        {
                            IDStudent = student.ID,
                            Subject = operation.Subject,
                            Teacher = operation.Teacher,
                            Classroom = operation.Classroom,
                            Mark = operation.Mark
                        });
                        _unitOfWork.Submit();
                    }

                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteDataStudent(int id)
        {
            using (var dbStudentTransaction = _studentData.Database.BeginTransaction())
            {
                try
                {
                    // var student = (from st in _unitOfWork.Student.Gets() where st.ID == id select st).FirstOrDefault();
                    var student = _unitOfWork.Student.Gets().FirstOrDefault(st => st.ID == id);

                    if (student != null)
                    {
                        // var subject = (from sub in _unitOfWork.Subject.Gets() where sub.IDStudent == student.ID select sub).ToList();
                        var subject = _unitOfWork.Subject.Gets().Where(sub => sub.IDStudent == student.ID).ToList();

                        if (subject != null)
                        {
                            foreach (var sub in subject)
                            {
                                _unitOfWork.Subject.Delete(sub);
                            }
                            _unitOfWork.Submit();

                            _unitOfWork.Student.Delete(student);
                            _unitOfWork.Submit();
                        }

                        if (subject == null)
                        {
                            _unitOfWork.Student.Delete(student);
                            _unitOfWork.Submit();
                        }
                    }
                    dbStudentTransaction.Commit();
                }
                catch (Exception)
                {
                    dbStudentTransaction.Rollback();
                    throw;
                }
            }
        }

        public IEnumerable<Operation> FilterStudentByMark(double mark)
        {
            /*var student = from st in _studentData.tblStudent
                          join sub in _studentData.tblSubject on st.ID equals sub.IDStudent
                          where sub.Mark >= mark
                          select new Operation
                          {
                              Name = st.Name,
                              Address = st.Address,
                              Email = st.Email,
                              Subject = sub.Subject,
                              Teacher = sub.Teacher,
                              Classroom = sub.Classroom,
                              Mark = sub.Mark
                          };*/

            /*var student = _studentData.tblStudent.Join(_studentData.tblSubject.DefaultIfEmpty(), st => st.ID, sub => sub.IDStudent, (st, sub) => new Operation
            {
                Name = st.Name,
                Address = st.Address,
                Email = st.Email,
                Subject = sub.Subject,
                Teacher = sub.Teacher,
                Classroom = sub.Classroom,
                Mark = sub.Mark
            })
            .Where(sub => sub.Mark >= mark);*/

            var leftOuterJoin = _studentData.tblStudent.GroupJoin(_studentData.tblSubject, st => st.ID, sub => sub.IDStudent, (st, sub) => new { Student = st, Subject = sub })
                          .SelectMany(op => op.Subject.DefaultIfEmpty(), (st, sub) => new { st.Student, Subject = sub })
                          .Select(res => new Operation
                          {
                              Name = res.Student.Name,
                              Address = res.Student.Address,
                              Email = res.Student.Email,
                              Subject = res.Subject.Subject,
                              Teacher = res.Subject.Teacher,
                              Classroom = res.Subject.Classroom,
                              Mark = res.Subject.Mark
                          })
                          //.Where(sub => sub.Mark >= mark)
                          ;

            var rightOuterJoin = _studentData.tblSubject.GroupJoin(_studentData.tblStudent, sub => sub.IDStudent, st => st.ID, (sub, st) => new { Subject = sub, Student = st })
                          .SelectMany(op => op.Student.DefaultIfEmpty(), (sub, st) => new { sub.Subject, Student = st })
                          .Select(res => new Operation
                          {
                              Name = res.Student.Name,
                              Address = res.Student.Address,
                              Email = res.Student.Email,
                              Subject = res.Subject.Subject,
                              Teacher = res.Subject.Teacher,
                              Classroom = res.Subject.Classroom,
                              Mark = res.Subject.Mark
                          });

            var fullOuterJoin = leftOuterJoin.Union(rightOuterJoin);

            return fullOuterJoin;
        }
    }
}
