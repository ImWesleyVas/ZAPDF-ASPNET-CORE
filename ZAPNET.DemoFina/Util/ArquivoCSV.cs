using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    public class ArquivoCSV : Arquivo<List<string[]>>
    {
        public ArquivoCSV()
        {
        }
        public ArquivoCSV(string path) : base(path)
        {
        }

        public override async Task<List<string[]>> ReadFile(List<IFormFile> arquivos, IHostingEnvironment appEnvironment)
        {
            try
            {
                PathFile = await UploadFiles(arquivos, appEnvironment);

                using (StreamReader sr = File.OpenText(PathFile))
                {
                    List<string[]> lines = new List<string[]>();
                    string[] line = null;

                    while (!sr.EndOfStream)
                    {    
                        string linha = await sr.ReadLineAsync();
                        line = linha.Split(';');
                        lines.Add(line);                        
                    }
                    return lines;
                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
        }
    }
}
