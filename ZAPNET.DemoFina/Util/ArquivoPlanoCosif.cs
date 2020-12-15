using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    public class ArquivoPlanoCosif : Arquivo
    {
        public ArquivoPlanoCosif()
        {                
        }
        public ArquivoPlanoCosif(string path) : base(path)
        {
        }

        public override StreamReader ReadFile()
        {
            try
            {                
                    using (StreamReader sr = File.OpenText(Path))
                    {
                        return sr;
                    }                
            }
            catch (IOException e)
            {
                throw e;
            }
        }
    }
}
