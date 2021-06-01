using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public abstract class Saldo : ISaldo
    {
        protected Saldo(IConta conta, int id, double valorSaldo, char sinal)
        {
            Conta = conta;
            Id = id;
            ValorSaldo = valorSaldo;
            Sinal = sinal;
        }

        public IConta Conta { get; set; }
        public int Id { get; set; }
        public double ValorSaldo { get; set; }
        public char Sinal { get; set; }

    }
}
