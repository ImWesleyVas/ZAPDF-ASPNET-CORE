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
    public class EmpresaRepository : ICrudRepository<Empresa>
    {

        SqlConnection conn = null;
        SqlCommand comando = null;

        public EmpresaRepository()
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



        public bool Save(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Empresa obj)
        {
            throw new NotImplementedException();
        }

        public List<Empresa> findAll()
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
                using (var dr = comando.ExecuteReader())
                {
                    var empresa = new Empresa();

                    while (dr.Read())
                    {
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

        public Empresa findById(int id)
        {
            throw new NotImplementedException();
        }


        public bool Update(Empresa obj)
        {
            throw new NotImplementedException();
        }
    }
}
