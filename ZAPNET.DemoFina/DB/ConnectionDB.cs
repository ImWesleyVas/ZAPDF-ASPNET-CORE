using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ZAPNET.DemoFina.DB
{
    public class ConnectionDB: DbContext
    //public static class ConnectionDB
    {
        //IConfiguration configuration;

        //static private IConfiguration configuration; // verificar depois como usar este para captura a conectionString
        // string de conexão com ZAP_DF
        // DESABILITAR PARA USAR O SERVIÇO DO .NET CORE  ==>>> APPSETTINGS.JSON
        //static string conexao = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZAP_DF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //// usando serviço de injeção de dependência do .NET Core
        public IConfiguration Configuration { get; }

        public ConnectionDB(IConfiguration config)
        {
            Configuration = config;
        }


        // retornar uma conexão
        //public  SqlConnection ObterConexao()  // non-static
        public SqlConnection ObterConexao()
        {   
            var cn = new SqlConnection();

            //cn.ConnectionString = conexao; // static
            cn.ConnectionString = Configuration.GetConnectionString("Default");  // non-static
            //cn.ConnectionString = (ConfigurationManager.ConnectionStrings["Default"].ToString());
            return cn;
        }

        //public static SqlConnection ObterConexao(string conectionString)
        public static SqlConnection ObterConexao(string conectionString)

        {
            var cn = new SqlConnection();
            cn.ConnectionString = conectionString;
            return cn;
        }

    }
}
