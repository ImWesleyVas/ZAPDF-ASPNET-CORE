using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class CosifRepository : ICosifRepository
    {
        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;
        int contador = 0;

        //injeção de dependencia (container e consumo de serviço na startup)
        
        public CosifRepository(ConnectionDB conexao)
        {           
            if (conn == null) conn = conexao.ObterConexao();            

            if (comando == null) comando = new SqlCommand();
        }

        public async Task<bool> ImportaCosifCSVAsync(List<string[]> contasCosif)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;
                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;
                // abre conexao com DB
                conn.Open();

                comando.CommandText = "delete from COSIF_TMP";
                await comando.ExecuteNonQueryAsync();
                comando.Parameters.Clear();

                comando.CommandText = @"INSERT INTO COSIF_TMP (plano, conta, nome_conta, dt_ini_vigen, dt_fim_vigen, natureza, doc_cd, segmento, numero_colunas, conta_grupo)" +
                                            "VALUES (@PLANO, @CONTA, @NOME_CONTA, @DT_INI_VIGEN, @DT_FIM_VIGEN, @NATUREZA, @DOC_CD, @SEGMENTO, @NUMERO_COLUNAS, @CONTA_GRUPO)";


                // listar contas do arquivo
                foreach (var conta in contasCosif)
                {
                    // define a query a se executada

                    // trocamos os parametros
                    comando.Parameters.AddWithValue("@PLANO", conta[0].Trim());
                    comando.Parameters.AddWithValue("@CONTA", conta[1].Trim());
                    comando.Parameters.AddWithValue("@NOME_CONTA", conta[2].Trim());
                    comando.Parameters.AddWithValue("@DT_INI_VIGEN", conta[3].Trim());
                    comando.Parameters.AddWithValue("@DT_FIM_VIGEN", conta[4].Trim());
                    comando.Parameters.AddWithValue("@NATUREZA", conta[5].Trim());
                    comando.Parameters.AddWithValue("@DOC_CD", conta[6].Trim());
                    comando.Parameters.AddWithValue("@SEGMENTO", conta[7].Trim());
                    comando.Parameters.AddWithValue("@NUMERO_COLUNAS", conta[8].Trim());
                    comando.Parameters.AddWithValue("@CONTA_GRUPO", conta[9].Trim());

                    // executamos o comando para inserir no BD e retorna 1 a cada execução
                    contador += await comando.ExecuteNonQueryAsync();

                    comando.Parameters.Clear();

                }

                comando.CommandText = "exec dbo.sp_carrega_plano_cosif";
                contador += await comando.ExecuteNonQueryAsync();

                result = contador >= 1 ? true : false;
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

        public async Task<bool> Add(Cosif obj)
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
                result = await comando.ExecuteNonQueryAsync() >= 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return result;

        }

        public bool Delete(Cosif obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cosif>> FindAllCosifAsync()
        {
            List<Cosif> lista = new List<Cosif>();


            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM COSIF ORDER BY CONTA";

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        var cosif = new Cosif();
                        cosif.Id = Convert.ToInt32(dr["Id"]);
                        cosif.ContaCosif = Convert.ToInt32(dr["Conta"]);
                        cosif.Descricao = Convert.ToString(dr["Descricao"].ToString());
                        cosif.Tipo = Convert.ToString(dr["Tipo"].ToString());
                        cosif.Natureza = Convert.ToString(dr["Natureza"].ToString());
                        cosif.Nivel = Convert.ToInt32(dr["Nivel"]);
                        cosif.Classe = Convert.ToString(dr["Classe"].ToString());
                        cosif.Validade = Convert.ToString(dr["Validade"].ToString());
                        cosif.AtributoInstitucional = Convert.ToString(dr["Atributo_Institucional"].ToString());

                        lista.Add(cosif);
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

        public bool Update(Cosif obj)
        {
            throw new NotImplementedException();
        }

       

        public async Task<Cosif> FindByContaAsync(string conta)
        {
            Cosif contaCosif = new Cosif();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "select * from COSIF where CONTA = @conta";

                comando.Parameters.AddWithValue("@conta", conta);

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {

                        contaCosif.Id = Convert.ToInt32(dr["Id"]);
                        contaCosif.ContaCosif = Convert.ToInt32(dr["Conta"]);
                        contaCosif.Descricao = Convert.ToString(dr["Descricao"].ToString());
                        contaCosif.Tipo = Convert.ToString(dr["Tipo"].ToString());
                        contaCosif.Natureza = Convert.ToString(dr["Natureza"].ToString());
                        contaCosif.Nivel = Convert.ToInt32(dr["Nivel"]);
                        contaCosif.Classe = Convert.ToString(dr["Classe"].ToString());
                        contaCosif.Validade = Convert.ToString(dr["Validade"].ToString());
                        contaCosif.AtributoInstitucional = Convert.ToString(dr["Atributo_Institucional"].ToString());
                        
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

            return contaCosif;
        }
    }
}
