using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class ContaDFDAO
    {
        ICrudRepository<ContaDF> repo;

        public ContaDFDAO(ICrudRepository<ContaDF> repo)
        {
            this.repo = repo;
        }

        //ContaDFRepository repo = new ContaDFRepository();

        public async Task<List<ContaDF>> findAllContasDFAsync(int? id)
        {
            return await repo.FindAll(id);
        }

        public ContaDF findByIDContasDF(int id)
        {
            return repo.FindById(id);
        }

        public async Task<bool> SalvarAsync(ModeloDF modelo, ContaDF conta)
        {
           return await repo.Add(modelo.Id, conta);
        }

        public bool Excluir(ContaDF conta)
        {
            return repo.Delete(conta);
        }

    }
}
