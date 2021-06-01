using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class SaldoRepository : ISaldoRepository
    {
        public bool ImportarSaldos()
        {
            return true;
        }

        public bool LancarSaldo()
        {
            return true;
        }

        public bool ExcluirSaldo(Conta conta, string periodo)
        {
            return true;
        }

        public bool ExcluirTodosSaldos(string periodo)
        {
            return true;
        }
    }
}
