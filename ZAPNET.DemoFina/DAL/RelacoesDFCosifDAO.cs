using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class RelacoesDFCosifDAO
    {
        private IRelaContaDFCosifRepository repo;

        public RelacoesDFCosifDAO(IRelaContaDFCosifRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<Cosif>> CosifbyContaDFList(int modeloId, int contaDFId )
        {

            return await repo.ListaCosifRelaByContaDF(modeloId, contaDFId);
        }

    }
}
