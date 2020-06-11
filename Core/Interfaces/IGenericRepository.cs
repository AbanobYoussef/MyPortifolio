using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetALL();

        T GetById(object id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
