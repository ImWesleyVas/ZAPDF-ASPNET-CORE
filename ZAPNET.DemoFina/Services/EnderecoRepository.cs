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

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public List<T> findAll()
        {
            throw new NotImplementedException();
        }

        public T findById(int id)
        {
            throw new NotImplementedException();
        }

        public T Save(T Obj)
        {
            throw new NotImplementedException();
        }
    }
}
