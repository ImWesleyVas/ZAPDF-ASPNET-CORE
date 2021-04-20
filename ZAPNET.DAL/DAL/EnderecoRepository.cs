using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZAPNET.DAL.DB;
using ZAPNET.Interfaces;

namespace ZAPNET.DAL.DAL
{
    public class EnderecoRepository<T> : ICrudRepository<T>
    {
        
        SqlConnection conn = null;
        SqlCommand comando = null;

        public IConfiguration Configuration { get; }

        public EnderecoRepository(IConfiguration configuration)
        {

            if(conn == null)
            {
                conn = (SqlConnection) ConnectionDB.ObterConexao();
            }

            if(comando == null)
            {
                comando = new SqlCommand();

            }
            Configuration = configuration;
        }

        public bool Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> FindAll(int? id)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(T Obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
