using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public interface ICosifRepository
    {
        Task<List<Cosif>> FindAllCosifAsync();
        Task<bool> ImportaCosifCSVAsync(List<string[]> contasCosif);
    }
}