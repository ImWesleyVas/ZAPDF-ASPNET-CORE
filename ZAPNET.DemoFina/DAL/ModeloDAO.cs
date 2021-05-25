using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;


namespace ZAPNET.DemoFina.DAL
{
    public class ModeloDAO
    {
        private IModeloDFRepository repo;

        public ModeloDAO(IModeloDFRepository repo)
        {
            this.repo = repo;
        }

        public string _periodo { get; set; }


        public ModeloDAO(IModeloDFRepository repo, string periodo)
        {
            this.repo = repo;
            this._periodo = periodo;
        }


        public async Task<List<ModeloDF>> FindAllModelosAsync(string periodo)
        {
            var listaModelosDAO = await repo.FindAll(periodo);
            return listaModelosDAO;
            // AQUI É PRECISO DAR TRATAMENTO, QUANDO A DATA SOLICITADA NÃO EXISTIR
            //if (listaModelosDAO.Count == 0)
            //    return new List<ModeloDF>();
        }



        public ModeloDF FindByModeloID(int id)
        {
            ModeloDF modelo = repo.FindById(id);
            return modelo;
        }

        public async Task<bool> Salvar(ModeloDF modelo)
        {
            return await repo.Add(modelo);
        }

        public bool Update(ModeloDF modelo)
        {
            return repo.Update(modelo);
        }

    }
}

