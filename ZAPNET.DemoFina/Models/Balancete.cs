using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class Balancete
    {
        public PeriodoRef PeriodoRef { get; set; }

        //a chave do saldo é a conta cosif
        public IDictionary<int, ISaldo> Saldos { get; set; }

        public Balancete(PeriodoRef periodoRef )
        {
            PeriodoRef = periodoRef;
           
        }


    }
}
