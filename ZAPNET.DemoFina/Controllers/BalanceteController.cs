using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Controllers
{
    public class BalanceteController : Controller
    {
        public IActionResult MenuBalancete()
        {
            return View();
        }
    }
}
