using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Services
{
    interface ICrudRepository<T>
    {
        bool Save(T obj);
        bool Delete(T obj);
        bool Update(T obj);
        T findById(int id);
        List<T> findAll();
    }
}
