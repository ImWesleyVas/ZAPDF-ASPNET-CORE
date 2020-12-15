using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;

namespace ZAPNET.DemoFina.Services    
{
    public class EnderecoRepository<T> : ICrudRepository<T>
    {
        
        SqlConnection conn = null;
        SqlCommand comando = null;

        public EnderecoRepository()
        {

            if(conn == null)
            {
                conn = (SqlConnection)ConnectionDB.ObterConexao();
            }

            if(comando == null)
            {
                comando = new SqlCommand();

            }

        }

        public bool Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(T Obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
