using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public interface IPeriodoRefRepository
    {
        Task<bool> AddPeriodo(PeriodoRef periodo);
        bool DeletePeriodoRefDAO(PeriodoRef periodo);
        List<PeriodoRef> FindAllPeriodosRef();
        Task<PeriodoRef> FindPeriodoRef(string periodo);
    }
}