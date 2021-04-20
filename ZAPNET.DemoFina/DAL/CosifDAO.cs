using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class CosifDAO
    {

        private readonly ICosifRepository  repo;

        public CosifDAO(ICosifRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> SalvaArquivoCosifAsync(List<string[]> lista)
        {
            List<string[]> contasCosif = lista.Where(x => x[0] != "#PLANO").ToList();
            return await repo.ImportaCosifCSVAsync(contasCosif);
        }

        public async Task<List<Cosif>> findAllCosifAsync()
        {            
            return await repo.FindAllCosifAsync();
        }
    }
}
