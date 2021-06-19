using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class SaldoDF : Saldo
    {
        public SaldoDF(int id, IConta conta, double valorSaldo, char sinal) : base(id, conta, valorSaldo, sinal)
        {
        }
    }
}
