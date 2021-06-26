using System.Collections.Generic;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public interface ISaldoRepository
    {
        bool ExcluirSaldo(Conta conta, string periodo);
        bool ExcluirTodosSaldos(string periodo);
        bool GravarSaldos(PeriodoRef periodo, SaldoCosif saldo);
        bool LancarSaldo();
        Task<List<SaldoCosif>> ListaSaldosCosif(string periodo);
    }
}