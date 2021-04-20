using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DAL;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class ContaDFRepository : ICrudRepository<ContaDF>
    {
        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        //injeção de dependencia (container e consumo de serviço na startup)
        private readonly ICrudRepository<ModeloDF> _repo;

        public ContaDFRepository(ConnectionDB conexao, ICrudRepository<ModeloDF> repo)
        {
            
            _repo = repo;

            if (conn == null)
            {
                conn = conexao.ObterConexao();
            }

            if (comando == null)
            {
                comando = new SqlCommand();

            }
        }


        public bool AddContaDF(int modeloID, ContaDF obj)
        {
            
            throw new NotImplementedException();

        }

        public async Task<bool> Add(int? modeloID, ContaDF obj)
        {
            try
            {
                
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @"INSERT INTO CONTA_DF (Codigo_Conta_DF, Descricao, Tipo, Natureza, Nivel, Classe, Modelo_Id)
                                        VALUES (@Codigo_Conta_DF, @Descricao, @Tipo, @Natureza, @Nivel, @Classe, @Modelo_Id)";

                // trocamos os parametros

                comando.Parameters.AddWithValue("@Codigo_Conta_DF", obj.CodigoContaDF);
                comando.Parameters.AddWithValue("@Descricao", obj.Descricao.Trim());
                comando.Parameters.AddWithValue("@Tipo", obj.Tipo.Trim());
                comando.Parameters.AddWithValue("@Natureza", obj.Natureza.Trim());
                comando.Parameters.AddWithValue("@Nivel", obj.Nivel);
                comando.Parameters.AddWithValue("@Classe", obj.Classe);
                comando.Parameters.AddWithValue("@Modelo_Id", modeloID);

                // abre conexao com DB
                conn.Open();

                // executamos o comando para inserir no BD
                result = await (comando.ExecuteNonQueryAsync()) >= 1 ? true : false;
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
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @"DELETE FROM CONTA_DF WHERE Id=@Id AND Modelo_Id=@Modelo_Id";

                // trocamos os parametros
                comando.Parameters.AddWithValue("@Id", obj.Id);
                comando.Parameters.AddWithValue("@Modelo_Id", obj.ModeloDF.Id);

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

        public bool DeleteContaDF(ContaDF obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContaDF>> FindAll(int ? idModelo)
        {
            ModeloDF modelo = new ModeloDAO(_repo).FindByModeloID((int)idModelo);
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
                using (var dr = await comando.ExecuteReaderAsync())
                {   
                    while (await dr.ReadAsync())
                    {
                        var contaDF = new ContaDF();
                        contaDF.Id = Convert.ToInt32(dr["Id"]);
                        contaDF.CodigoContaDF = Convert.ToInt32(dr["Codigo_Conta_DF"]);
                        contaDF.Descricao = Convert.ToString(dr["Descricao"].ToString());
                        contaDF.Tipo = Convert.ToString(dr["Tipo"].ToString());
                        contaDF.Nivel = Convert.ToInt32(dr["Nivel"]);
                        contaDF.Natureza = Convert.ToString(dr["Natureza"].ToString());
                        contaDF.Classe = Convert.ToString(dr["Classe"].ToString());
                        contaDF.ModeloDF = modelo;

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
            ContaDF conta = new ContaDF();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM CONTA_DF WHERE ID = @ID";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@ID", id);

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = comando.ExecuteReader())
                {
                    dr.Read();
                    conta.Id = Convert.ToInt32(dr["Id"]);
                    conta.CodigoContaDF = Convert.ToInt32(dr["Codigo_Conta_DF"]);
                    conta.Descricao = Convert.ToString(dr["Descricao"].ToString());
                    conta.Tipo = Convert.ToString(dr["Tipo"].ToString());
                    conta.Nivel = Convert.ToInt32(dr["Nivel"]);
                    conta.Natureza = Convert.ToString(dr["Natureza"].ToString());
                    conta.Classe = Convert.ToString(dr["Classe"].ToString());
                    conta.ModeloDF =  new ModeloDAO(_repo).FindByModeloID(Convert.ToInt32(dr["Modelo_Id"].ToString()));
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

            return conta;
        }

        public bool Update(ContaDF obj)
        {
            throw new NotImplementedException();
        }
    }
}
