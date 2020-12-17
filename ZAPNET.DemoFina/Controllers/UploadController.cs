using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Controllers
{
    public class UploadController : Controller
    {
        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public UploadController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        //método para enviar os arquivos usando a interface IFormFile
        [HttpPost]
        public async Task<IActionResult> EnviarArquivo(List<IFormFile> arquivos)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in arquivos)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return RedirectToAction("ImportarCosif", "Cosif", ViewData["Erro"]);
                }
                // < define a pasta onde vamos salvar os arquivos >
                string pasta = "UserFiles";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "File_Cosif_" + DateTime.Now.Millisecond.ToString();
                //verifica qual o tipo de arquivo : txt ou csv
                if (arquivo.FileName.Contains(".txt"))
                    nomeArquivo += ".txt";
                else
                    nomeArquivo += ".csv";
                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _appEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\Files\\" + pasta + "\\";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "\\Upload\\" + nomeArquivo;
                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }


            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";
            //retorna a viewdata
            return RedirectToAction("ImportarCosif", "Cosif", ViewData["Resultado"]); 






        }
    }
}
