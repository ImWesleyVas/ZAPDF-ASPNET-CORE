using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Services;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.DAL
{
    public class CadocDAO
    {
        public SaldoCosifDAO SaldoCosifDAO { get; set; }
        private readonly ICadocRepository repo;
        private readonly IPeriodoRefRepository periodoRefRepo;


        public CadocDAO(ICadocRepository repo, IPeriodoRefRepository periodo)
        {
            this.repo = repo;
            periodoRefRepo = periodo;
        }

        public CadocDAO(SaldoCosifDAO saldoCosifDAO, ICadocRepository repo, IPeriodoRefRepository periodoRefRepo)
        {
            SaldoCosifDAO = saldoCosifDAO;
            this.repo = repo;
            this.periodoRefRepo = periodoRefRepo;
        }

        public async Task<bool> GravarSaldoCadocDAOAsync(List<string[]> lista)
        {
            bool result = false;

            try
            {
                // Gravar cadoc em tabela temporária
                List<string[]> linhasCosif = lista;
                result = await repo.GravarCadocDBAsync(linhasCosif);
                if (!result)
                    return result;

                var listaGravaSaldo = await repo.FindAllCadocAsync();

                // Obter a data do cadoc importado
                var regA1 = listaGravaSaldo.Find(l => l[1] == "#A1");
                var periodo = regA1[6].Substring(2, 4) + regA1[6].Substring(0, 2);

                // Gravar periodo
                // se existir um periodo 'F' fechado, não excluir e não adicionar, se houver periodo 'A' Aberto - apagar este e inserir o novo
                await periodoRefRepo.AddPeriodo(new PeriodoRef(periodo));

                ////Obter o periodo do BD
                //var periodoRef = await periodoRefRepo.FindPeriodoRef(periodo);

                //// Gravar Saldos
                //var reg0 = listaGravaSaldo.FindAll(l => l[1] == "0");

                //foreach (var item in reg0)
                //{
                //    var conta = int.Parse(item[3].ToString());
                //    var sinal = item[6];
                //    double saldo = Facilities.valorComSinalPorNatureza(conta, sinal, double.Parse(item[5]) / 100);

                //    // devemos colocar esse metodo depois da validação do saldo cosif, validação do periodo
                //    result = await SaldoCosifDAO.GravarSaldosDAOAsync(periodoRef, conta.ToString(), saldo);
                //    if (!result) throw new ArgumentException("Erro ao gravar Saldo");
                //}
            }
            catch (Exception e)
            {
                if (e.Message == "Periodo já existe.")
                {

                }
                throw e;
            }


            return result;
        }

        internal async Task<bool> GravarValidacaoCadocDAOAsync(List<string[]> linhasValidacao)
        {
            bool result = await repo.GravarValidacaoCadocDBAsync(linhasValidacao);

            return result;
        }

        public async Task<List<string[]>> ListaCadocDAOAsync()
        {
            return await repo.FindAllCadocAsync();
        }

        public async Task<bool> DeleteCadocTmpDAO()
        {
            return await repo.DeleteCadocTmpAsync();
        }

        internal async Task<List<string[]>> FindAllCadocValidacaoAsyncDAO()
        {
            return await repo.FindAllCadocValidacaoAsyncDB();
        }


    }
}
