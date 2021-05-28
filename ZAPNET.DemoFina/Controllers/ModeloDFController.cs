using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Controllers
{
    public class ModeloDFController : Controller
    {

        private readonly IModeloDFRepository _repo;
        private readonly IModeloSessions _sessions;
        ModeloDAO modeloDAO;

        public ModeloDFController(IModeloDFRepository repo, IModeloSessions sessions)
        {
            _repo = repo;
            _sessions = sessions;
            modeloDAO = new ModeloDAO(_repo, _sessions);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FindByIdModelo(int id)
        {
            return View(modeloDAO.FindByModeloID(id));
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> ListaModelos(string mesAno)
        {
            // PRECISA TIRAR O PERIODO (MESANO) DA TABELA DE MODELOS
            // ESTE DEVE CONTER APENAS NO PERIODOREF E NO RELA

            var periodo = (mesAno == null ? null : mesAno.Replace("-", ""));
            var listaModelos = await modeloDAO.FindAllModelosAsync(periodo);
            ViewData["MesAno"] = _sessions.GetPeriodo().Substring(0, 4) + "-" + _sessions.GetPeriodo().Substring(4, 2); 

            if(listaModelos.Count == 0)
            {
                // incluir mensagem option, para criar nova data, clonar de outro período ou cancelar
                return View(new List<ModeloDF>());
            }

            return View(listaModelos);
        }


        public IActionResult AddModelo()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddModelo(int id)
        {
            return View(modeloDAO.FindByModeloID(id));
        }


        [HttpPost]
        public async Task<IActionResult> AddModelo(ModeloDF formularioModelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (formularioModelo.Id > 0)
                    {
                        if (modeloDAO.Update(formularioModelo))
                        {
                            ViewData["Sucesso"] = "Modelo atualizado com sucesso!!!";
                            return RedirectToAction("ListaModelos");
                        }
                        else
                        {
                            ViewData["Falha"] = "O modelo não foi atualizado!!!";
                            return View();
                        }

                    }
                    else
                    {
                        if (await modeloDAO.Salvar(formularioModelo))
                        {
                            ViewData["Sucesso"] = "Modelo cadastrado com sucesso!!!";
                            return RedirectToAction("ListaModelos");
                        }
                        else
                        {
                            ViewData["Falha"] = "O modelo não foi cadastrado!!!";
                            return View();
                        }
                    }


                    // retorna e mantém os dados nos campos - não recarrega a pagina

                }
            }
            catch (Exception e)
            {

                ViewData["Falha"] = "[ERROR] - Verifique: " + e.Message;
            }

            // Redireciona para a ActionResult ListaEmpresas que responde
            // comando (verbo) do HttpGet
            return RedirectToAction("ListaModelos");
        }


    }
}
