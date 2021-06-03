using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Controllers
{
    public class ContaDFController : Controller
    {
        private readonly ICrudRepository<ContaDF> _repoContaDF;
        private readonly IModeloDFRepository _repoModeloDF;
        private readonly IModeloSessions _sessions;

        public ContaDFController(ICrudRepository<ContaDF> repoContaDF, IModeloDFRepository repoModeloDF, IModeloSessions sessions)
        {
            _repoContaDF = repoContaDF;
            _repoModeloDF = repoModeloDF;
            _sessions = sessions;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaContasDF()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaContasDF(int Id, int pagina = 1)
        {
            var periodo = _sessions.GetPeriodo();

            List<ContaDF> contasDF = new List<ContaDF>();
            contasDF = await new ContaDFDAO(_repoContaDF).findAllContasDFAsync(Id);
            var pageListContaDF = contasDF.ToPagedList(pagina, 12);
            
            ViewData["MesAno"] = periodo.Substring(0, 4) + "-" + periodo.Substring(4, 2);

            //resolvendo o problema da lista vazia... 
            if (contasDF.Count == 0)
            {
                ViewBag.Modelo = new ModeloDAO(_repoModeloDF, _sessions).FindByModeloID(Id);
                return View();
            }

            ViewBag.Modelo = new ModeloDAO(_repoModeloDF, _sessions).FindByModeloID(Id);
            return View(pageListContaDF);
        }


        [HttpGet]
        public IActionResult AddContaDF(int Id)
        {
            ModeloDFModelView modelo = new ModeloDFModelView();
            modelo.ModeloDF = new ModeloDAO(_repoModeloDF, _sessions).FindByModeloID(Id);
            modelo.ContaDF = new ContaDF();
            return View(modelo);
        } // fim get AddContaDF

        [HttpPost]
        public async Task<IActionResult> AddContaDF(ModeloDFModelView modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (modelo.ContaDF.Id == 0 && modelo.ModeloDF.Id > 0)
                    {
                        await (new ContaDFDAO(_repoContaDF)).SalvarAsync(modelo.ModeloDF, modelo.ContaDF);
                        return RedirectToAction("ListaContasDF", new { id = modelo.ModeloDF.Id });
                    }
                }

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        } // fim post AddContaDF

        [HttpGet]
        public IActionResult ExcluiContaDF(int id)
        {
            ContaDF conta = new ContaDFDAO(_repoContaDF).findByIDContasDF(id);

            try
            {
                if (ModelState.IsValid)
                {
                    if (conta.Id > 0 && conta.ModeloDF.Id > 0)
                    {
                        new ContaDFDAO(_repoContaDF).Excluir(conta);
                        return RedirectToAction("ListaContasDF", new { id = conta.ModeloDF.Id });
                    }
                }

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        } // fim get AddContaDF


    }
}
