using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class SaldoCosif : Saldo, ISaldo
    {
        public SaldoCosif( IConta conta, double valorSaldo, char sinal) : base( conta, valorSaldo, sinal)
        {
        }
    }
}
