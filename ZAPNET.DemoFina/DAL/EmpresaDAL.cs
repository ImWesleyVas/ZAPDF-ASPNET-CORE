using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.DAL
{
    public class EmpresaDAL
    {
        private ICrudRepository<Empresa> repo;

        public EmpresaDAL(ICrudRepository<Empresa> repo)
        {
            this.repo = repo;
        }

        public async Task <List<Empresa>> FindAllEmpresas(int? id)
        {
            return await repo.FindAll(id);
        }

        public async Task<bool> Salvar(Empresa empresa)
        {
            return await repo.Add(0, empresa);
        }
    }
}
