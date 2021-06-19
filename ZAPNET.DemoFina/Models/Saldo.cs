using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public abstract class Saldo : ISaldo
    {
        protected Saldo(int id, IConta conta, double valorSaldo, char sinal)
        {
            Id = id;
            Conta = conta;
            ValorSaldo = valorSaldo;
            Sinal = sinal;
        }

        protected Saldo(IConta conta, double valorSaldo, char sinal)
        {            
            Conta = conta;
            ValorSaldo = valorSaldo;
            Sinal = sinal;
        }

        public int Id { get; set; }
        public IConta Conta { get; set; }
        public double ValorSaldo { get; set; }
        public char Sinal { get; set; }

    }
}
