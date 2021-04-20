using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public interface IRelaContaDFCosifRepository
    {
        Task<List<Cosif>> ListaCosifRelaByContaDF(int idModelo, int contaDF);
    }
}