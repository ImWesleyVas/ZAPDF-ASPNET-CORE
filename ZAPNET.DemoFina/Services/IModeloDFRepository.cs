using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public interface IModeloDFRepository
    {
        Task<bool> Add(ModeloDF obj);
        bool Delete(ModeloDF obj);
        Task<List<ModeloDF>> FindAll(string periodo);
        ModeloDF FindById(int id);
        bool Update(ModeloDF obj);
    }
}