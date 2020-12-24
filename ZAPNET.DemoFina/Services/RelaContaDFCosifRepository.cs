using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;
using ZAPNET.DemoFina.Models.ModelView;

namespace ZAPNET.DemoFina.Services
{
    public class RelaContaDFCosifRepository : ICrudRepository<RelacaoPeriodoRefModelView>
    {


        SqlConnection conn = null;
        SqlCommand comando = null;
        bool result = false;

        public RelaContaDFCosifRepository()
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



        public List<Cosif> ListaCosifRelaByContaDF(int idModelo, int contaDF)
        {
            List<Cosif> listaCosif = new List<Cosif>();

            try
            {
                // adiciona a propriedade connection ao respectivo objeto de conexao
                comando.Connection = conn;

                // define que tipo de comando sera executado (using System.Data)
                comando.CommandType = CommandType.Text;

                // define a query a se executada
                comando.CommandText = "select * from COSIF where conta in " +
                    "(select COSIF from RELACOES where MODELO_DF_ID = @MODELO_DF_ID AND CONTA_DF = @CONTA_DF)";

                // trocamos os parametros                
                comando.Parameters.AddWithValue("@MODELO_DF_ID", idModelo);
                comando.Parameters.AddWithValue("@CONTA_DF", contaDF);

                // abre conexao com DB
                conn.Open();

                // executa o comando SQL
                using (var dr = comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var cosif = new Cosif();
                        cosif.Id = Convert.ToInt32(dr["Id"]);
                        cosif.ContaCosif = Convert.ToInt32(dr["Codigo_Conta_DF"]);
                        cosif.Descricao = Convert.ToString(dr["Descricao"].ToString());
                        cosif.Tipo = Convert.ToString(dr["Tipo"].ToString());
                        cosif.Nivel = Convert.ToInt32(dr["Nivel"]);
                        cosif.Natureza = Convert.ToString(dr["Natureza"].ToString());
                        cosif.Classe = Convert.ToString(dr["Classe"].ToString());
                        cosif.Validade = Convert.ToString(dr["Validade"].ToString());
                        cosif.AtributoInstitucional = Convert.ToString(dr["Atributo_institucional"].ToString());

                        listaCosif.Add(cosif);
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

            return listaCosif;
        }




        public bool Add(RelacaoPeriodoRefModelView obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(RelacaoPeriodoRefModelView obj)
        {
            throw new NotImplementedException();
        }

        public List<RelacaoPeriodoRefModelView> FindAll(int? id)
        {
            throw new NotImplementedException();
        }

        public RelacaoPeriodoRefModelView FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(RelacaoPeriodoRefModelView obj)
        {
            throw new NotImplementedException();
        }





    }
}
