using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
        bool Remove(int id);
        bool Edit(int id, T entity);
        void SaveData();
    }
}
