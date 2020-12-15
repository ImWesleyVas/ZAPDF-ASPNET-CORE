using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Util
{
    public abstract class Arquivo : IArquivo
    {
        protected string Path { get; set; }

        public Arquivo()
        {
        }

        public Arquivo(string path)
        {
            Path = path;
        }

        public abstract StreamReader ReadFile();
    }
}
