using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Models;
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
        private readonly PeriodoRefDAO periodoRefDAO;
        private readonly ContaDFDAO contaDFDAO;
        private readonly ModeloDAO modeloDAO;

        public BalanceteController(ICadocRepository cadocRepository, IHostingEnvironment appEnvironment, IPeriodoRefRepository periodoRef, ICosifRepository cosif,
            ISaldoRepository saldoRepository, ICrudRepository<ContaDF> contaDFRepository, IModeloDFRepository modeloDFRepository)
        {
            _appEnvironment = appEnvironment;
            saldoCosifDAO = new SaldoCosifDAO(cosif, saldoRepository, cadocRepository, periodoRef);
            cadocDAO = new CadocDAO(saldoCosifDAO, cadocRepository, periodoRef);
            periodoRefDAO = new PeriodoRefDAO(periodoRef);
            contaDFDAO = new ContaDFDAO(contaDFRepository);
            modeloDAO = new ModeloDAO(modeloDFRepository);
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

            var linhasCadoc = lista.FindAll(l => l[0] != "CHECK");

            var linhasValidacao = lista.FindAll(l => l[0] == "CHECK");

            await cadocDAO.GravarSaldoCadocDAOAsync(linhasCadoc);

            await cadocDAO.GravarValidacaoCadocDAOAsync(linhasValidacao);

            var validacao = await cadocDAO.FindAllCadocValidacaoAsyncDAO();

            //// verificar se há algum erro na importação do cadoc
            ///  se houver redirecionar para ListaCADOC e mostrar erros            
            var existeErro = validacao.FindAll(e => e[3] == "ERROR");
            if (existeErro.Count > 0)
                return RedirectToAction("ListaCADOC");

            // verificar se há algum registro cadoc inconsistente
            // (falta construir métodos de tabela de criticas do bacen)
            /*****************************************/

            // se cadoc estiver tudo Ok, trasformar cadoc em saldos Cosif
            // e redirecinar para Balancete Saldos Cosif
            await saldoCosifDAO.TransformarSaldoCADOCEmSaldoCosif();

            var linha  = linhasCadoc.Find(l => l[1] == "#A1");
            var periodo = linha[5].Substring(2, 4) + linha[5].Substring(0, 2);
           
            //passando a action, controller e um novo objeto de parametro
            return RedirectToAction("Balancete", "Balancete", new { periodo } );

        }

        public async Task<IActionResult> ListaCADOC()
        {
            var lista = await cadocDAO.ListaCadocDAOAsync();
            ViewBag.Erros = "Não foi possível importar os saldos. CADOC possui erros, por favor corrija-os e importe o novo arquivo!";
            return View(lista);
        }


        public async Task<IActionResult> Balancete(string periodo)
        {
            var saldosCosif = await saldoCosifDAO.ListaSaldosCosifDAOAsync(periodo);
            
            BalanceteModelView balancete = new BalanceteModelView(await periodoRefDAO.FindByPeriodoRefDAO(periodo), saldosCosif);

            ViewBag.Sucesso = "Saldos importados com sucesso!";

            return View(balancete);
        }


        public async Task<JsonResult> ChecarPeriodo(string periodo)
        {
            PeriodoRef periodoRef = await periodoRefDAO.FindByPeriodoRefDAO(periodo);
            var result = Json(periodoRef);

            // verifica se há registro no periodo refe e se esta fechado
            if (periodoRef != null)
                if (periodoRef.StatusPeriodo == 'F')
                    return result;

            // verifica se há conta no periodo refe
            var modelos = await modeloDAO.FindAllModelosAsync(periodo);
            if (modelos.Count > 0)
            {
                PeriodoRef periodoMod = new PeriodoRef(periodo);
                periodoMod.StatusPeriodo = 'M';
                result = Json(periodoMod);
                return result;
            }

            // verifica se há saldo Cosif no periodo refe
            var saldos = await saldoCosifDAO.ListaSaldosCosifDAOAsync(periodo);
            if (saldos.Count() > 0)
            {
                PeriodoRef periodoSaldo = new PeriodoRef(periodo);
                periodoSaldo.StatusPeriodo = 'C';
            }

            return result;
        }
    }
}
