using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManage.Dal.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(int id);
        List<T> Gets();
        T Add(T obj);
        void Delete(T obj);
        void SaveChanges();
    }
}
