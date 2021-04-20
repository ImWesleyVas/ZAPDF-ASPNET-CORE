using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Services
{
    public interface ICrudRepository<T>
    {
        Task<bool> Add(int? id, T obj);
        bool Delete(T obj);
        bool Update(T obj);
        T FindById(int id);
        Task<List<T>> FindAll(int? id);
        
    }
}
