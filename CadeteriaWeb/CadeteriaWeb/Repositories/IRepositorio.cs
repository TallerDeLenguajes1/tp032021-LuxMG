using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorio<T>
    {
        void Delete(int id);
        List<T> GetAll();
        T GetItemById(int id);
        void Insert(T item);
        void Update(T item);
    }
}
