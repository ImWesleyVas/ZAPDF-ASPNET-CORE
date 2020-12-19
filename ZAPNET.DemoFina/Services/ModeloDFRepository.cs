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
    public class ModeloDFRepository : ICrudRepository<ModeloDF>
    {

        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        public ModeloDFRepository()
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

        public bool Add(ModeloDF obj)
        {

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @"INSERT INTO MODELO_DF (Nome) " +
                    "VALUES (@Nome)";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@Nome", obj.Nome);

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

        public bool Delete(ModeloDF obj)
        {
            throw new NotImplementedException();
        }

        public List<ModeloDF> FindAll()
        {
            List<ModeloDF> lista = new List<ModeloDF>();


            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM MODELO_DF ORDER BY ID";

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = comando.ExecuteReader())
                {


                    while (dr.Read())
                    {
                        var modelo = new ModeloDF();
                        modelo.Id = Convert.ToInt32(dr["Id"]);
                        modelo.Nome = Convert.ToString(dr["Nome"].ToString());

                        lista.Add(modelo);
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

        public ModeloDF FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ModeloDF obj)
        {
            throw new NotImplementedException();
        }
    }
}
