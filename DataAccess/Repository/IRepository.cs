using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
