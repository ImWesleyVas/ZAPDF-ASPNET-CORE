using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;



namespace ZAPNET.DemoFina.Controllers
{
    public class CosifController : Controller
    {
        private readonly ICosifRepository _repo;

        //Define uma instância de IHostingEnvironment
        private readonly IHostingEnvironment _appEnvironment;

        private readonly CosifDAO _cosifDao;
        public CosifController(ICosifRepository repo, IHostingEnvironment appEnvironment)
        {
            _repo = repo;
            _appEnvironment = appEnvironment;
            _cosifDao = new CosifDAO(_repo);
        }



        //Injeta a instância no construtor para poder usar os recursos


        //USANDO BIBLIOTECA X.PAGEDLIST.MVC.CORE PARA PAGINAÇÃO
        public async Task<IActionResult> ListaCosif(int pagina = 1)  // recebendo a pagina como parâmetro, iniciando por pagina 1
        {
            var listaCosif = await _cosifDao.findAllCosifAsync();
            var pageListCosif = listaCosif.ToPagedList(pagina, 10);  // transformar o List em PagedList
            return View(pageListCosif);  // passando o pagedList para View
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


        [HttpPost]
        public async Task<IActionResult> EnviarArquivo(List<IFormFile> arquivos)
        {
            ArquivoCSV _arquivo = new ArquivoCSV();
            List<string[]> _listaCosif;

            // Fazendo Upload e Lendo arquivo utilizando a camada UTIL e gerando um lista com os dados
            _listaCosif = await _arquivo.ReadFile(arquivos, _appEnvironment);
            // passando a lista para o camada DAL
            await _cosifDao.SalvaArquivoCosifAsync(_listaCosif);

            return RedirectToAction("ListaCosif");
        }
    }
}
