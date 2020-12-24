
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;


namespace ZAPNET.DemoFina.Controllers
{
    public class ContaDFController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaContasDF()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListaContasDF(int Id)
        {
            List<ContaDF> contasDF = new List<ContaDF>();
            contasDF = new ContaDFDAO().findAllContasDF(Id);

            //resolvendo o problema da lista vazia... 
            if (contasDF.Count == 0)
            {
                ViewBag.Modelo = new ModeloDAO().FindByModeloID(Id);
                return View();
            }

            ViewBag.Modelo = new ModeloDAO().FindByModeloID(Id);
            return View(contasDF);
        }


        [HttpGet]
        public IActionResult AddContaDF(int Id)
        {
            ModeloDFModelView modelo = new ModeloDFModelView();
            modelo.ModeloDF = new ModeloDAO().FindByModeloID(Id);
            modelo.ContaDF = new ContaDF();
            return View(modelo);
        } // fim get AddContaDF

        [HttpPost]
        public IActionResult AddContaDF(ModeloDFModelView modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (modelo.ContaDF.Id == 0 && modelo.ModeloDF.Id > 0)
                    {
                        new ContaDFDAO().Salvar(modelo.ModeloDF, modelo.ContaDF);
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
            ContaDF conta = new ContaDFDAO().findByIDContasDF(id);

            try
            {
                if (ModelState.IsValid)
                {
                    if (conta.Id > 0 && conta.ModeloDF.Id > 0)
                    {
                        new ContaDFDAO().Excluir(conta);
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
