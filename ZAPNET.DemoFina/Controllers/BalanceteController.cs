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
            foreach (var item in validacao)
            {
                if (item[3].Trim() == "ERROR")
                    return RedirectToAction("ListaCADOC");
            }

            // verificar se há algum registro cadoc inconsistente


            // se estiver Ok redirecionar gravar saldos Cosif
            // e redirecinar para Balancete Saldos Cosif
            var listaCadoc = await cadocDAO.ListaCadocDAOAsync();
            var linhaCadoc = listaCadoc.First();
            var periodo = linhaCadoc[5];
            // precisa chamar o metodo para passar a lista de cadoc -- GravarSaldosDAOAsync

            return RedirectToAction("Balancete");

        }

        public async Task<IActionResult> ListaCADOC()
        {
            var lista = await cadocDAO.ListaCadocDAOAsync();
            ViewBag.Sucesso = "CADOC importado apresenta erros, por favor corrija-os e o importe novavamente!";
            return View(lista);
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
