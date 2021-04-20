using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.Controllers
{
    public class RelacoesPeriodoRefController : Controller
    {
        RelacaoPeriodoRefModelView relacao;
        ICrudRepository<ContaDF> repoContaDF;
        IRelaContaDFCosifRepository repoRelaContaDFCosif;
        ICosifRepository repoCosif;

        public RelacoesPeriodoRefController(ICrudRepository<ContaDF> repoContaDF, IRelaContaDFCosifRepository repoRelaContaDFCosif, ICosifRepository repoCosif)
        {
            this.repoContaDF = repoContaDF;
            this.repoRelaContaDFCosif = repoRelaContaDFCosif;
            this.repoCosif = repoCosif;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RelacionarDFCosifAsync(int id)
        {
            relacao = new RelacaoPeriodoRefModelView();
            relacao.PeriodoRef = new PeriodoRef();
            relacao.ContaDF = new ContaDF();
            relacao.ContaDF = new ContaDFDAO(repoContaDF).findByIDContasDF(id);
            relacao.ListaCosifs = await (new RelacoesDFCosifDAO(repoRelaContaDFCosif)).CosifbyContaDFList(relacao.ContaDF.ModeloDF.Id, relacao.ContaDF.CodigoContaDF);

            var contasCosif = (await new CosifDAO(repoCosif).findAllCosifAsync())
                    .Except(relacao.ListaCosifs)
                    .Select(c => new
                    {
                        IdCosif = c.Id,
                        ContaCosif = c.ContaCosif + "  " + c.Descricao,
                        Descricao = c.Descricao,
                        Tipo = c.Tipo,
                        Nivel = c.Nivel,
                    })
                    .ToList();

            ViewBag.Cosif = new MultiSelectList(contasCosif, "ContaCosif", "ContaCosif") ;

            return View(relacao);
            
        }
    }
}
