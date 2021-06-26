using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.DAL
{
    public class SaldoCosifDAO
    {
        public Cosif Cosif { get; private set; }
        public PeriodoRef PeriodoRef { get; private set; }

        private SaldoCosif saldoCosif;

        private readonly IPeriodoRefRepository periodoRefRepository;

        private readonly ICadocRepository cadocRepository;

        private readonly ICosifRepository cosifRepository;

        private readonly ISaldoRepository saldoRepository;


        public SaldoCosifDAO(ICosifRepository cosifRepository, ISaldoRepository saldoRepository, ICadocRepository cadocRepository, IPeriodoRefRepository periodoRefRepository)
        {
            this.cosifRepository = cosifRepository;
            this.saldoRepository = saldoRepository;
            this.cadocRepository = cadocRepository;
            this.periodoRefRepository = periodoRefRepository;
        }

        public async Task<bool> GravarSaldosDAOAsync(PeriodoRef periodo, string conta, double saldo)
        {
            bool result = false;

            PeriodoRef = periodo;
            Cosif = await cosifRepository.FindByContaAsync(conta);
            char sinal = Facilities.sinalSaldo(saldo);
            var valorAbs = Math.Abs(saldo);

            saldoCosif = new SaldoCosif(Cosif, valorAbs, sinal);

            result = saldoRepository.GravarSaldos(PeriodoRef, saldoCosif);

            return result;

        }

        public async Task<List<SaldoCosif>> ListaSaldosCosifDAOAsync(string periodo)
        {
            var saldosCosif = await saldoRepository.ListaSaldosCosif(periodo);
            return saldosCosif;
        }

        // tratar a transformação do saldo cadoc em saldo cosif, nesse método separado (originou-se dentro do CadocDao em GravaSaldoCadocDaoAsync)
        public async void TransformarSaldoCADOCEmSaldoCosif()
        {

            bool result = false;

            var listaGravaSaldo = await cadocRepository.FindAllCadocAsync();

            // Obter a data do cadoc importado
            var regA1 = listaGravaSaldo.Find(l => l[1] == "#A1");
            var periodo = regA1[6].Substring(2, 4) + regA1[6].Substring(0, 2);

            //Obter o periodo do BD
            var periodoRef = await periodoRefRepository.FindPeriodoRef(periodo);

            // Gravar Saldos
            var reg0 = listaGravaSaldo.FindAll(l => l[1] == "0");

            foreach (var item in reg0)
            {
                var conta = int.Parse(item[3].ToString());
                var sinal = item[6];
                double saldo = Facilities.valorComSinalPorNatureza(conta, sinal, double.Parse(item[5]) / 100);

                // devemos colocar esse metodo depois da validação do saldo cosif, validação do periodo
                result = await GravarSaldosDAOAsync(periodoRef, conta.ToString(), saldo);
                if (!result) throw new ArgumentException("Erro ao gravar Saldo");
            }
        }
    }
}
