using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ZAPNET.DemoFina.DB
{
    public static class ConnectionDB 
    {
        static private IConfiguration configuration; // verificar depois como usar este para captura a conectionString


        // string de conexão com ZAP_DF

        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZAP_DF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // retornar uma conexão
        public static DbConnection ObterConexao()
        {
            var cn = new SqlConnection();
            cn.ConnectionString = conexao;
            return cn;
        }

        public static SqlConnection ObterConexao(string conectionString)
        {
            var cn = new SqlConnection();
            cn.ConnectionString = conectionString;
            return cn;
        }
    }
}
