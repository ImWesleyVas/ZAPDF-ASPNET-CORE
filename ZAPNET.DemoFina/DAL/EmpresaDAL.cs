using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class EmpresaDAL
    {
        private ICrudRepository<Empresa> repo = new EmpresaRepository();

        public List<Empresa> FindAllEmpresas()
        {
            return repo.FindAll();
        }

        public bool Salvar(Empresa empresa)
        {
            return repo.Add(empresa);
        }
    }
}
