using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.DB;
using ZAPNET.DemoFina.Models;

namespace ZAPNET.DemoFina.Services    
{
    public class EnderecoRepository : ICrudRepository<Endereco>
    {
        
        SqlConnection conn = null;
        SqlCommand comando = null;

        //injeção de dependencia (container e consumo de serviço na startup)
        private readonly ConnectionDB Conexao;

        public EnderecoRepository(ConnectionDB conexao)
        {
            Conexao = conexao;

            if (conn == null)
            {
                conn = conexao.ObterConexao();
            }

            if (comando == null)
            {
                comando = new SqlCommand();

            }
        }


        public bool Delete(Endereco obj)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Endereco>> FindAll(int? id)
        {
            throw new NotImplementedException();
        }

        public Endereco FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Add(int? id, Endereco Obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(Endereco obj)
        {
            throw new NotImplementedException();
        }
    }
}
