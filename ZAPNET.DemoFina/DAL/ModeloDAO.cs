using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.DAL
{
    public class ModeloDAO
    {
        private IModeloDFRepository repo;
        private IModeloSessions sessions;
        public string _periodo { get; set; }


        public ModeloDAO(IModeloDFRepository repo, IModeloSessions sessions)
        {
            this.repo = repo;
            this.sessions = sessions;
        }
        public async Task<List<ModeloDF>> FindAllModelosAsync(string periodo)
        {
            var listaModelosDAO = await repo.FindAll(periodo);

            if (listaModelosDAO.Count == 0) return listaModelosDAO;

            if (periodo == null || periodo != sessions.GetPeriodo())
                periodo = listaModelosDAO.First()._periodo;

            sessions.SetPeriodo(periodo);
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

