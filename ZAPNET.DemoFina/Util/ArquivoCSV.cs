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

        public override List<string[]> ReadFile()
        {
            try
            {
                using (StreamReader sr = File.OpenText(Path))
                {
                    List<string[]> lines = new List<string[]>();
                    string[] line = null;

                    while (!sr.EndOfStream)
                    {                        
                        line = sr.ReadLine().Split(';');
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
