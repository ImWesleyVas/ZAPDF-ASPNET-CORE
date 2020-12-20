using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Controllers
{
    public class ModeloDFController : Controller
    {
        ModeloDAO modeloDAO = new ModeloDAO();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaModelos(int? id)
        {
            return View(modeloDAO.FindAllModelos(id));
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
        public IActionResult AddModelo(ModeloDF formularioModelo)
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
                        if (modeloDAO.Salvar(formularioModelo))
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
