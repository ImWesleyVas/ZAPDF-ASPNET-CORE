using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;

namespace ZAPNET.DemoFina.DAL
{
    public class SaldoCosifDAO
    {
        public Cosif Cosif { get; private set; }
        public PeriodoRef PeriodoRef { get; private set; }

        private SaldoCosif saldoCosif;

        private readonly ICosifRepository cosifRepository;

        private readonly ISaldoRepository saldoRepository;
        
 
        public SaldoCosifDAO(ICosifRepository cosifRepository, ISaldoRepository saldoRepository)
        {
            this.cosifRepository = cosifRepository;
            this.saldoRepository = saldoRepository;            
        }

        public async Task<bool> GravarSaldosDAOAsync(PeriodoRef periodo, string conta, double saldo)
        {
            bool result = false;

            PeriodoRef = periodo;
            Cosif = await cosifRepository.FindByContaAsync(conta);
            char sinal = saldo < 1 ? 'D' : 'C';
            var valorAbs = Math.Abs(saldo);

            saldoCosif = new SaldoCosif(Cosif, valorAbs, sinal);

            result = saldoRepository.GravarSaldos(PeriodoRef, saldoCosif);

            return result;

        }


    }
}
