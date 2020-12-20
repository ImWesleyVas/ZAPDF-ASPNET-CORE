using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Services
{
    interface ICrudRepository<T>
    {
        bool Add(T obj);
        bool Delete(T obj);
        bool Update(T obj);
        T FindById(int id);
        List<T> FindAll(int? id);
    }
}
