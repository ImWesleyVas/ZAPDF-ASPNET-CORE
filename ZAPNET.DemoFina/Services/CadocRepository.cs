using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;

namespace ZAPNET.DemoFina.Services
{
    public class CadocRepository : ICadocRepository
    {

        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;
        int contador = 0;

        //injeção de dependencia (container e consumo de serviço na startup)

        public CadocRepository(ConnectionDB conexao)
        {
            if (conn == null) conn = conexao.ObterConexao();

            if (comando == null) comando = new SqlCommand();
        }


        public async Task<bool> GravarCadocDBAsync(List<string[]> linhasCadoc)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;
                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;
                // abre conexao com DB
                conn.Open();

                await DeleteCadocTmpAsync();


                comando.CommandText = "insert into CADOC_TMP (linha, reg, campo1, campo2, campo3, campo4, campo5, campo6, campo7, campo8, campo9)" +
                    "values(@linha, @reg, @campo1, @campo2, @campo3, @campo4, @campo5, @campo6, @campo7, @campo8, @campo9)";

                foreach (var linha in linhasCadoc)
                {
                    comando.Parameters.AddWithValue("@linha", linha[0].Trim());
                    comando.Parameters.AddWithValue("@reg", linha[1].Trim());
                    comando.Parameters.AddWithValue("@campo1", linha[1].Trim());
                    comando.Parameters.AddWithValue("@campo2", linha[2].Trim());
                    comando.Parameters.AddWithValue("@campo3", linha[3].Trim());
                    if (linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo4", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo4", linha[4].Trim());
                    if (linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo5", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo5", linha[5].Trim());
                    if (linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo6", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo6", linha[6].Trim());
                    if (linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo7", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo7", linha[7].Trim());
                    if (linha[1] == "#A1" || linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo8", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo8", linha[8].Trim());
                    if (linha[1] == "#A1" || linha[1] == "@1")
                        comando.Parameters.AddWithValue("@campo9", DBNull.Value);
                    else
                        comando.Parameters.AddWithValue("@campo9", linha[9].Trim());

                    contador += await comando.ExecuteNonQueryAsync();
                    comando.Parameters.Clear();
                }

                result = contador >= 1 ? true : false;

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


        public async Task<bool> GravarValidacaoCadocDBAsync(List<string[]> linhasValidacaoCadoc)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;
                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;
                // abre conexao com DB
                conn.Open();                

                comando.CommandText = "insert into CADOC_VALIDA_TMP (linha, reg, campo, status, descricao)" +
                    "values(@linha, @reg, @campo, @status, @descricao)";

                foreach (var linha in linhasValidacaoCadoc)
                {
                    comando.Parameters.AddWithValue("@linha", linha[1].Trim());
                    comando.Parameters.AddWithValue("@reg", linha[2].Trim());
                    comando.Parameters.AddWithValue("@campo", linha[3].Trim());
                    comando.Parameters.AddWithValue("@status", linha[4].Trim());
                    comando.Parameters.AddWithValue("@descricao", linha[5].Trim());

                    contador += await comando.ExecuteNonQueryAsync();
                    comando.Parameters.Clear();
                }

                comando.Parameters.Clear();
                comando.CommandText = "exec dbo.SP_CONFIRMA_COSIF";
                contador = await comando.ExecuteNonQueryAsync();

                result = contador >= 1 ? true : false;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                comando.Parameters.Clear();

                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return result;
        }

        public async Task<List<string[]>> FindAllCadocAsync()
        {
            List<string[]> lista = new List<string[]>();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM CADOC_TMP ORDER BY 1";

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {

                        if (dr[1].ToString() == "#A1")
                        {
                            string[] cadoc = new string[9];

                            for (int i = 0; i < cadoc.Length; i++)
                            {
                                cadoc[i] = dr[i].ToString();
                            }

                            lista.Add(cadoc);

                        }
                        else if (dr[1].ToString() == "0")
                        {
                            string[] cadoc = new string[11];

                            for (int i = 0; i < cadoc.Length; i++)
                            {
                                cadoc[i] = dr[i].ToString();
                            }

                            lista.Add(cadoc);
                        }
                        else if (dr[1].ToString() == "@1")
                        {
                            string[] cadoc = new string[4];

                            for (int i = 0; i < cadoc.Length; i++)
                            {
                                cadoc[i] = dr[i].ToString();
                            }

                            lista.Add(cadoc);
                        }

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

        public async Task<List<string[]>> FindAllCadocValidacaoAsyncDB()
        {
            List<string[]> lista = new List<string[]>();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM CADOC_VALIDA_TMP ORDER BY 1";

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = await comando.ExecuteReaderAsync())
                {
                    while (dr.Read())
                    {
                        string[] cadoc = new string[5];

                        for (int i = 0; i < cadoc.Length; i++)
                        {
                            cadoc[i] = dr[i].ToString();
                        }

                        lista.Add(cadoc);
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


        public async Task<bool> DeleteCadocTmpAsync()
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                comando.CommandText = "delete from CADOC_VALIDA_TMP";
                await comando.ExecuteNonQueryAsync();
                comando.Parameters.Clear();

                // define a query a se executada
                comando.CommandText = "DELETE FROM CADOC_TMP";

                // abre conexao com DB
                if (conn.State.ToString() != "Open")
                    conn.Open();

                result = (await comando.ExecuteNonQueryAsync() >= 0) ? true : false;
                comando.Parameters.Clear();
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

    }
}
