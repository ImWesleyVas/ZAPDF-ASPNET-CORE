using System.Data.SqlClient;

namespace ZAPNET.DAL.DB
{
    //public class ConnectionDB
    public static class ConnectionDB
    {
        
        //static private IConfiguration configuration; // verificar depois como usar este para captura a conectionString
        // string de conexão com ZAP_DF
        // DESABILITAR PARA USAR O SERVIÇO DO .NET CORE  ==>>> APPSETTINGS.JSON
        static string conexao = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZAP_DF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //public IConfiguration Configuration { get; } // usando serviço de injeção de dependência do .NET Core
                
        // retornar uma conexão
        public static SqlConnection ObterConexao() 
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
