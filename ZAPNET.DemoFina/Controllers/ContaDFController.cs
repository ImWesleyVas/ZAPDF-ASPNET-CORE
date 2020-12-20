
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Controllers
{
    public class ContaDFController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaContasDF(int modeloId)
        {
            return View(new ContaDFDAO().findAllContasDF(modeloId));
        }

        
        [HttpGet]
        public IActionResult AddContaDF(int modeloId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContaDF(ContaDF contaDF, int modeloId)
        {
            return View();
        }
    }
}
