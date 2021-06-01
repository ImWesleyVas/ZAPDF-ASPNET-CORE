using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class SaldoDF : Saldo
    {
        public SaldoDF(IConta conta, int id, double valorSaldo, char sinal) : base(conta, id, valorSaldo, sinal)
        {
        }
    }
}
