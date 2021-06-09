using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class CadocDAO
    {
        private readonly ICadocRepository repo;

        public CadocDAO(ICadocRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> GravarCadocAsync(List<string[]> lista)
        {
            List<string[]> linhasCosif = lista;
            return await repo.GravarCadocDBAsync(linhasCosif);
        }
    }
}
