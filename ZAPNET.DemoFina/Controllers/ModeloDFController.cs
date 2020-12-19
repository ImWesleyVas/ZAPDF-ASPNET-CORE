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

        public IActionResult ListaModelos()
        {
            return View(modeloDAO.FindAllModelos());
        }

        public IActionResult AddModelo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddModelo(ModeloDF formularioModelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (modeloDAO.Salvar(formularioModelo))
                    {
                        ViewData["Sucesso"] = "Empresa cadastrada com sucesso!!!";
                        return RedirectToAction("ListaModelos");
                    }
                    else
                    {
                        ViewData["Falha"] = "O cadastro da empresa não foi realizado!!!";
                        return View();
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
