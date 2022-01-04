using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManage.Dal.Repository.Interface;
using StudentManage.Models;

namespace StudentManage.Dal.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        StudentDataContext _studentContext;

        public GenericRepository(StudentDataContext studentContext)
        {
            _studentContext = studentContext;
        }

        public T Add(T obj)
        {
            _studentContext.Set<T>().Add(obj);

            return obj;
        }

        public void Delete(T obj)
        {
            _studentContext.Set<T>().Remove(obj);
        }

        public T Get(int id)
        {
            return _studentContext.Set<T>().Find(id);
        }

        public List<T> Gets()
        {
            return _studentContext.Set<T>().ToList();
        }

        public void SaveChanges()
        {
            _studentContext.SaveChanges();
        }
    }
}
