using Microsoft.Extensions.Configuration;
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

        //injeção de dependencia (container e consumo de serviço na startup)
        private readonly ConnectionDB Conexao;

        public ModeloDFRepository(ConnectionDB conexao)
        {
            Conexao = conexao;

            if (conn == null)
            {
                conn = conexao.ObterConexao();
            }

            if (comando == null)
            {
                comando = new SqlCommand();

            }
        }

        public async Task<bool> Add(int? id, ModeloDF obj)
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
                result = await comando.ExecuteNonQueryAsync() >= 1 ? true : false;
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

        public async Task<List<ModeloDF>> FindAll(int? id)
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
                using (var dr = await comando.ExecuteReaderAsync())
                {


                    while (await dr.ReadAsync())
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
            var modelo = new ModeloDF();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM MODELO_DF WHERE ID = @ID";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@ID", id);


                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = comando.ExecuteReader())
                {


                    while (dr.Read())
                    {
                        
                        modelo.Id = Convert.ToInt32(dr["Id"]);
                        modelo.Nome = Convert.ToString(dr["Nome"].ToString());

                        
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

            return modelo;
        }

        public bool Update(ModeloDF obj)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @"UPDATE MODELO_DF SET Nome =  " +
                    "@Nome WHERE Id = @Id";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@Nome", obj.Nome);
                comando.Parameters.AddWithValue("@Id", obj.Id);

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
    }
}
