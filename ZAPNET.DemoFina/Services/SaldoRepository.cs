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
    public class SaldoRepository : ISaldoRepository
    {

        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;
        int contador = 0;

        //injeção de dependencia (container e consumo de serviço na startup)

        public SaldoRepository(ConnectionDB conexao)
        {
            if (conn == null) conn = conexao.ObterConexao();

            if (comando == null) comando = new SqlCommand();

        }

        public bool GravarSaldos(PeriodoRef periodo, SaldoCosif saldo)
        {

            try
            {

                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;
                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;
                // abre conexao com DB
                conn.Open();


                comando.CommandText = @"insert into SALDO_COSIF (periodo,conta_cosif, saldo)" +
                    "values (@periodo, @conta_cosif, @saldo)";

                // define a query a se executada

                // trocamos os parametros
                comando.Parameters.AddWithValue("@periodo", periodo.Periodo.Trim());
                comando.Parameters.AddWithValue("@conta_cosif",  ((Cosif)saldo.Conta).ContaCosif);
                comando.Parameters.AddWithValue("@saldo", saldo.ValorSaldo);


                // executamos o comando para inserir no BD e retorna 1 a cada execução
                contador += comando.ExecuteNonQuery();

                comando.Parameters.Clear();

                result = contador >= 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                comando.Parameters.Clear();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return result;
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

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;
                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;
                // abre conexao com DB
                conn.Open();

                comando.CommandText = "delete from SALDO_COSIF where PERIODO = @periodo";

                // trocamos os parametros
                comando.Parameters.AddWithValue("@periodo", periodo.Trim());

                result = (comando.ExecuteNonQuery()) >= 1 ? true : false;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                comando.Parameters.Clear();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return result;
        }
    }
}
