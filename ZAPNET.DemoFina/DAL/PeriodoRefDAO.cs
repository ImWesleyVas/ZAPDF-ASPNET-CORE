using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class PeriodoRefDAO
    {
        private IPeriodoRefRepository repo;

        public PeriodoRefDAO(IPeriodoRefRepository repo)
        {
            this.repo = repo;
        }

        public async Task<PeriodoRef> FindByPeriodoRefDAO(string periodo)
        {
            var periodoRef = await repo.FindPeriodoRef(periodo);

            if (periodoRef == null)
                return new PeriodoRef();

            return periodoRef;
        }

        public bool DeletePeriodoRefDAO(PeriodoRef periodo)
        {
            var result = repo.DeletePeriodoRefDAO(periodo);
            return result;
        }

        public async Task<bool> AddPeriodoRefDAO(PeriodoRef periodo)
        {
            var result = await repo.AddPeriodo(periodo);
            return result;
        }

    }
}
