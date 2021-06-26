using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class PeriodoRefRepository : IPeriodoRefRepository
    {
        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;
        int contador = 0;


        private readonly SaldoRepository saldoRepo;

        public PeriodoRefRepository(ConnectionDB conexao)
        {
            if (conn == null) conn = conexao.ObterConexao();

            if (comando == null) comando = new SqlCommand();

            saldoRepo = new SaldoRepository(conexao);
        }

        public async Task<bool> AddPeriodo(PeriodoRef periodo)
        {
            // implementar: 
            // se existir periodo já existir como 'F' fechado, não excluir e não adicionar, se houver periodo 'A' Aberto - apagar este e inserir o novo
            var periodoParam = await FindPeriodoRef(periodo.Periodo);

            if (periodoParam.Periodo != null)
            {                
                saldoRepo.ExcluirTodosSaldos(periodo.Periodo);
                DeletePeriodoRefDAO(periodo);
            }


            try
            {

                comando.Connection = conn;
                comando.CommandType = CommandType.Text;

                comando.CommandText = "insert into PERIODO_REFE ( PERIODO, STATUS, Cong, Empr )" +
                    "values (@periodo, @status, @Cong, @Empr)";

                comando.Parameters.AddWithValue("@periodo", periodo.Periodo);
                comando.Parameters.AddWithValue("@status", periodo.StatusPeriodo);
                comando.Parameters.AddWithValue("@Cong", 1);
                comando.Parameters.AddWithValue("@Empr", 1);

                conn.Open();

                result = comando.ExecuteNonQuery() >= 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {

                conn.Close();
            }

            return result;
        }

        public bool DeletePeriodoRefDAO(PeriodoRef periodo)
        {
            try
            {
                comando.Connection = conn;
                comando.CommandType = CommandType.Text;

                comando.CommandText = "delete from PERIODO_REFE where PERIODO = @periodo and Cong = @Cong and Empr = @Empr";

                comando.Parameters.AddWithValue("@periodo", periodo.Periodo);
                comando.Parameters.AddWithValue("@status", periodo.StatusPeriodo);
                comando.Parameters.AddWithValue("@Cong", 1);
                comando.Parameters.AddWithValue("@Empr", 1);

                conn.Open();

                result = comando.ExecuteNonQuery() >= 1 ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                comando.Parameters.Clear();
                conn.Close();
            }

            return result;
        }

        public async Task<PeriodoRef> FindPeriodoRef(string periodo)
        {
            PeriodoRef periodoRef = new PeriodoRef();

            try
            {
                comando.Connection = conn;

                comando.CommandType = CommandType.Text;

                comando.CommandText = "select * from PERIODO_REFE where PERIODO = @pFind";

                comando.Parameters.AddWithValue("@pFind", periodo);

                conn.Open();

                using (var dr = await comando.ExecuteReaderAsync())
                {
                    if (!dr.HasRows)
                        return periodoRef;
                    // é necessário ler o conteudo do DataReader (não esquecer)
                    while (dr.Read())
                    {
                        periodoRef.Periodo = Convert.ToString(dr[0].ToString().Trim());
                        periodoRef.StatusPeriodo = Convert.ToChar(dr[1].ToString()); 
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                comando.Parameters.Clear();
                conn.Close();
            }

            return periodoRef;

        }

        public List<PeriodoRef> FindAllPeriodosRef()
        {
            throw new Exception("Em construção");
        }
    }
}
