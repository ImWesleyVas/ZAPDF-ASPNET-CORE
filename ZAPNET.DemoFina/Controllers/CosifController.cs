using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Controllers
{
    public class CosifController : Controller
    {
        public IActionResult ListaCosif()
        {
            List<Cosif> planoCosif = new List<Cosif>();
            return View(planoCosif);
        }


        public IActionResult ImportarCosif()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImpartarCosif()
        {
            return View();
        }
    }
}
