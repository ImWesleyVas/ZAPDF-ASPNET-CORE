using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Controllers
{
    public class ContaDFController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaContaDF()
        {
            return View();
        }
    }
}
