using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    public abstract class Arquivo<T> : IArquivo<List<string[]>>
    {
        protected string PathFile { get; set; }

        public Arquivo()
        {
        }

        public Arquivo(string path)
        {
            PathFile = path;
        }

        public abstract Task<List<string[]>> ReadFile(List<IFormFile> arquivos, IHostingEnvironment appEnvironment);

        public async Task<string> UploadFiles(List<IFormFile> arquivos, IHostingEnvironment appEnvironment)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in arquivos)
            {                
                // < define a pasta onde vamos salvar os arquivos >
                string pasta = "UserFiles";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "File_Cosif_" + DateTime.Now.Millisecond.ToString();
                //verifica qual o tipo de arquivo : txt ou csv
                if (arquivo.FileName.Contains(".txt"))
                    nomeArquivo += ".txt";
                if (arquivo.FileName.Contains(".bc"))
                    nomeArquivo += ".bc";
                else
                    nomeArquivo += ".csv";
                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = appEnvironment.WebRootPath;
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

                return caminhoDestinoArquivoOriginal;
            }

            return null;

        }
    }

}
