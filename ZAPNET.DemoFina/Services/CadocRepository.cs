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

                comando.CommandText = "delete from CADOC_TMP";
                await comando.ExecuteNonQueryAsync();
                comando.Parameters.Clear();

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


        public async Task<bool> GravarCadocErrorDBAsync(List<string[]> linhasCadoc)
        {
            return false;
        }

        public async Task<List<string[]>> FindAllCadocAsync()
        {
            return new List<string[]>();
        }

    }
}
