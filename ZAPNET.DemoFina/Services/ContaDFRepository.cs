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

        public List<ContaDF> FindAll()
        {
            throw new NotImplementedException();
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
