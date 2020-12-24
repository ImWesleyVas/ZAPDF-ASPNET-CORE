using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;


namespace ZAPNET.DemoFina.Controllers
{
    public class RelacoesPeriodoRefController : Controller
    {
        RelacaoPeriodoRefModelView relacao;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RelacionarDFCosif(int id)
        {
            relacao = new RelacaoPeriodoRefModelView();
            relacao.PeriodoRef = new PeriodoRef();
            relacao.ContaDF = new ContaDF();

            relacao.ContaDF = new ContaDFDAO().findByIDContasDF(id);

            relacao.ListaCosifs = new RelacoesDFCosifDAO().CosifbyContaDFList(relacao.ContaDF.ModeloDF.Id, relacao.ContaDF.CodigoContaDF);

            ViewBag.Cosif = new CosifDAO().findAllCosif(null).Except(relacao.ListaCosifs);

            return View(relacao);
            
        }
    }
}
