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
        private ICrudRepository<ModeloDF> repo;

        public ModeloDAO(ICrudRepository<ModeloDF> repo)
        {
            this.repo = repo;
        }

        public async Task<List<ModeloDF>> FindAllModelosAsync(int? id)
        {
            return await repo.FindAll(id);
        }

        public ModeloDF FindByModeloID(int id)
        {
            ModeloDF modelo = repo.FindById(id);
            return modelo;
        }

        public async Task<bool> Salvar(ModeloDF modelo)
        {
            return await repo.Add(0, modelo);
        }

        public bool Update(ModeloDF modelo)
        {
            return repo.Update(modelo);
        }

    }
}

