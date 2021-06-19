using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Controllers
{
    public class BalanceteController : Controller
    {
        //private readonly ICADOCRepository _repo;

        //Define uma instância de IHostingEnvironment
        private readonly IHostingEnvironment _appEnvironment;
        // Atributo DAO
        private readonly CadocDAO cadocDAO;
        private readonly SaldoCosifDAO saldoCosifDAO;
        
        public BalanceteController(ICadocRepository repo, IHostingEnvironment appEnvironment, IPeriodoRefRepository periodoRef, ICosifRepository cosif, ISaldoRepository saldoRepository)
        {           
            _appEnvironment = appEnvironment;
            saldoCosifDAO = new SaldoCosifDAO(cosif, saldoRepository);
            cadocDAO = new CadocDAO(saldoCosifDAO, repo, periodoRef);            
        }

        public IActionResult MenuBalancete()
        {
            return View();
        }

        public IActionResult ImportarCADOC()
        {
            cadocDAO.DeleteCadocTmpDAO();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportarCADOC(List<IFormFile> arquivos)
        {
            ArquivoTXT cadoc = new ArquivoTXT();

            var lista = await cadoc.ReadFile(arquivos, _appEnvironment);

            await cadocDAO.GravarSaldoCadocDAOAsync(lista);

            return RedirectToAction("ListaCADOC");
        }

        public async Task<IActionResult> ListaCADOC()
        {
            var lista = await cadocDAO.ListaCadocDAOAsync();
            ViewBag.Sucesso = "Saldos importados com sucesso!";
            return View(lista);
        }
    }
}
