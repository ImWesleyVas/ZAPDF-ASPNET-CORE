using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class ModeloDAO
    {
        private ICrudRepository<ModeloDF> repo = new ModeloDFRepository();

        public List<ModeloDF> FindAllModelos(int? id)
        {
            return repo.FindAll(id);
        }

        public ModeloDF FindByModeloID(int id)
        {
            ModeloDF modelo = repo.FindById(id);
            return modelo;
        }

        public bool Salvar(ModeloDF modelo)
        {
            return repo.Add(modelo);
        }

        public bool Update(ModeloDF modelo)
        {
            return repo.Update(modelo);
        }
    }
}

