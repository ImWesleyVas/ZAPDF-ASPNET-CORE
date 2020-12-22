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

        ContaDFRepository repo = new ContaDFRepository();

        public List<ContaDF> findAllContasDF(int? id)
        {
            return repo.FindAll(id);
        }

        public ContaDF findByIDContasDF(int id)
        {
            return repo.FindById(id);
        }

        public bool Salvar(ModeloDF modelo, ContaDF conta)
        {
           return repo.AddContaDF(modelo.Id, conta);
        }

        public bool Excluir(ContaDF conta)
        {
            return repo.DeleteContaDF( conta);
        }

    }
}
