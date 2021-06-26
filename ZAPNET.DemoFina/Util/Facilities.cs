using System;
using System.Text.RegularExpressions;

namespace ZAPNET.DemoFina.Util
{
    public static class Facilities
    {
        public static bool ehNumero(string valor, string qteCaract)
        {
            return Regex.IsMatch(valor, $"[0-9]{'{'+qteCaract+'}'}");
        }


        public static char sinalSaldo(double valor)
        {
            char sinal = valor < 1 ? 'D' : 'C';
            return sinal;
        }

        public static double valorComSinalPorNatureza(int conta, string sinal, double valorSaldo)
        {
            double saldo;
            var contaInic = conta.ToString().Substring(0, 1);

            if (contaInic == "1" || contaInic == "2" || contaInic == "3" || contaInic == "8")
            {
                if (sinal == "-")
                    saldo = valorSaldo;
                else
                    saldo = -valorSaldo;                
            }
            else if (contaInic == "4" || contaInic == "5" || contaInic == "6" || contaInic == "7" || contaInic == "9")
            {
                if (sinal == "-")
                    saldo = -valorSaldo;
                else
                    saldo = valorSaldo;                
            }
            else
                throw new ArgumentException("Caracter incial da conta inválido");

            return saldo;
        }
    }

}
