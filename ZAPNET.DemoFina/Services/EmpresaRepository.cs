using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services
{
    public class EmpresaRepository : ICrudRepository<Empresa>
    {
        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        public EmpresaRepository(ConnectionDB conexao)
        {
            
            if (conn == null)
            {
                conn = conexao.ObterConexao();
            }

            if (comando == null)
            {
                comando = new SqlCommand();
            }
        }

        public async Task<bool> Add(int? id, Empresa obj)
        {
            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = @"INSERT INTO EMPRESA (Mnemonico, Cong, Empr, Razao_Social, Nome_Fantasia, Segmento, CNPJ, " +
                    "Inscricao_Municipal, Inscricao_Estadual, Nire, Id_Bacen_Cvm_Susep, Atributo_Institucional) " +
                    "VALUES (@Mnemonico, @Cong, @Empr, @Razao_Social, @Nome_Fantasia, @Segmento, @CNPJ, " +
                    "@Inscricao_Municipal, @Inscricao_Estadual, @Nire, @Id_Bacen_Cvm_Susep, @Atributo_Institucional)";

                // trocamos os parametros
                if(obj.Mnemonico != null)
                {
                    comando.Parameters.AddWithValue("@Mnemonico", obj.Mnemonico.Trim());
                } 
                else
                {
                    comando.Parameters.AddWithValue("@Mnemonico", DBNull.Value);
                }
                
                comando.Parameters.AddWithValue("@Cong", obj.Cong);
                comando.Parameters.AddWithValue("@Empr", obj.Empr);
                comando.Parameters.AddWithValue("@Razao_Social", obj.RazaoSocial.Trim());
                comando.Parameters.AddWithValue("@Nome_Fantasia", obj.NomeFantasia.Trim());
                comando.Parameters.AddWithValue("@Segmento", obj.Segmento.Trim());
                comando.Parameters.AddWithValue("@CNPJ", obj.CNPJ.Trim());

                if(obj.InscricaoMunicipal != null)
                {
                    comando.Parameters.AddWithValue("@Inscricao_Municipal", obj.InscricaoMunicipal.Trim());
                }
                else
                {
                    comando.Parameters.AddWithValue("@Inscricao_Municipal", DBNull.Value);
                }
                
                if(obj.InscricaoEstadual != null)
                {
                    comando.Parameters.AddWithValue("@Inscricao_Estadual", obj.InscricaoEstadual.Trim());
                }
                else
                {
                    comando.Parameters.AddWithValue("@Inscricao_Estadual", DBNull.Value);
                }
                
                if(obj.Nire != null)
                {
                    comando.Parameters.AddWithValue("@Nire", obj.Nire.Trim());
                }
                else
                {
                    comando.Parameters.AddWithValue("@Nire", DBNull.Value);
                }
                
                if(obj.Id_Bacen_Cvm_Susep != null)
                {
                    comando.Parameters.AddWithValue("@Id_Bacen_Cvm_Susep", obj.Id_Bacen_Cvm_Susep.Trim());
                }
                else
                {
                    comando.Parameters.AddWithValue("@Id_Bacen_Cvm_Susep", DBNull.Value);
                }
                
                if(obj.AtributoInstitucional != 0)
                {
                    comando.Parameters.AddWithValue("@Atributo_Institucional", obj.AtributoInstitucional.ToString());
                }
                else
                {
                    comando.Parameters.AddWithValue("@Atributo_Institucional", DBNull.Value);
                }
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

        public bool Delete(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Empresa>> FindAll(int? id)
        {
            List<Empresa> lista = new List<Empresa>();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "SELECT * FROM EMPRESA ORDER BY CONG, EMPR";

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = await comando.ExecuteReaderAsync())
                {
                    

                    while (await dr.ReadAsync())
                    {
                        var empresa = new Empresa();
                        empresa.Id = Convert.ToInt32(dr["Id"]);
                        empresa.Mnemonico = Convert.ToString(dr["Mnemonico"].ToString());
                        empresa.Cong = Convert.ToInt32(dr["Cong"]);
                        empresa.Empr = Convert.ToInt32(dr["Empr"]);
                        empresa.RazaoSocial = Convert.ToString(dr["Razao_Social"].ToString());
                        empresa.NomeFantasia = Convert.ToString(dr["Nome_Fantasia"].ToString());
                        empresa.Segmento = Convert.ToString(dr["Segmento"].ToString());
                        empresa.CNPJ = Convert.ToString(dr["CNPJ"].ToString());
                        empresa.InscricaoMunicipal = Convert.ToString(dr["Inscricao_Municipal"].ToString());
                        empresa.InscricaoEstadual = Convert.ToString(dr["Inscricao_Estadual"].ToString());
                        empresa.AtributoInstitucional = Convert.ToChar(dr["Atributo_Institucional"]);


                        lista.Add(empresa);
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

        public Empresa FindById(int id)
        {
            throw new NotImplementedException();
        }


        public bool Update(Empresa obj)
        {
            throw new NotImplementedException();
        }



    }
}
