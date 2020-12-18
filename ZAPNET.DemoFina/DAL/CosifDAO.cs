using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class CosifDAO
    {

        CosifRepository repo = new CosifRepository();

        public bool SalvaArquivoCosif(List<string[]> lista)
        {
            List<string[]> contasCosif = lista.Where(x => x[0] != "#PLANO").ToList();
            return repo.ImportaCosifCSV(contasCosif);
        }

        public List<Cosif> findAllCosif()
        {
            return repo.FindAll();
        }
    }
}
