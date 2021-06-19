using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class PeriodoRef
    {
        public string Periodo { get; set; }
        public char StatusPeriodo { get; set; }

        public PeriodoRef()
        {

        }

        public PeriodoRef(string periodo)
        {
            Periodo = periodo;
            StatusPeriodo = 'A';
        }
    }
}
