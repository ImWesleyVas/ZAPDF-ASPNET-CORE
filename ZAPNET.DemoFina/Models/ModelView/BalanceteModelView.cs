using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class BalanceteModelView
    {
        public PeriodoRef PeriodoRef { get; private set; }

        public List<SaldoCosif> BalanceteSaldosCosif { get; private set; }

        public BalanceteModelView()
        {
        }

        public BalanceteModelView(PeriodoRef periodoRef, List<SaldoCosif> balanceteSaldosCosif)
        {
            PeriodoRef = periodoRef;
            BalanceteSaldosCosif = balanceteSaldosCosif;
        }
    }
}
