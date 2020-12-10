using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Services
{
    interface ICrudRepository<T>
    {
        T Save(T Obj);
        void Delete(T obj);
        T findById(int id);
        List<T> findAll();
    }
}
