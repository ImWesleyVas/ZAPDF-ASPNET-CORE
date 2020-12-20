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
    public class ContaDFRepository : ICrudRepository<ContaDF>
    {

        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        public ContaDFRepository()
        {

            if (conn == null)
            {
                conn = (SqlConnection)ConnectionDB.ObterConexao();
            }

            if (comando == null)
            {
                comando = new SqlCommand();

            }

        }

        public bool Add(ContaDF obj)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @" ";

                // trocamos os parametros

                
                //comando.Parameters.AddWithValue("@Cong", );


                // abre conexao com DB
                conn.Open();


                // executamos o comando para inserir no BD
                result = comando.ExecuteNonQuery() >= 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return result;

        }

        public bool Delete(ContaDF obj)
        {
            throw new NotImplementedException();
        }

        public List<ContaDF> FindAll(int ? idModelo)
        {
            List<ContaDF> lista = new List<ContaDF>();


            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM CONTA_DF WHERE MODELO_ID = @MODELO_ID";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@MODELO_ID", idModelo);

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = comando.ExecuteReader())
                {


                    while (dr.Read())
                    {
                        var contaDF = new ContaDF();
                        contaDF.Id = Convert.ToInt32(dr["Id"]);
                        contaDF.Descricao = Convert.ToString(dr["Descricao"].ToString());
                        contaDF.Tipo = Convert.ToString(dr["Tipo"].ToString());


                        lista.Add(contaDF);
                    }

                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }

        public ContaDF FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ContaDF obj)
        {
            throw new NotImplementedException();
        }
    }
}
