using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class EmpresaRepository<T> : ICrudRepository<T>
    {
        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> findAll()
        {
            throw new NotImplementedException();
        }

        public T findById(int id)
        {
            throw new NotImplementedException();
        }

        public T Save(T Obj)
        {
            throw new NotImplementedException();
        }
    }
}
