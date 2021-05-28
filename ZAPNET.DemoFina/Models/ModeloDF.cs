using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Models
{
    public class ModeloDF
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string _periodo { get; set; }

        private List<Empresa> empresas = new List<Empresa>();
        private List<ContaDF> contasDF = new List<ContaDF>();
        

        public ModeloDF()
        {

        }

        public ModeloDF(int id, string demonstracao)
        {
            Id = id;
            Nome = demonstracao;
            
        }


        public ModeloDF(int id, string demonstracao, string periodo)
        {
            Id = id;
            Nome = demonstracao;
            _periodo = periodo;
        }


        public void addEmpresa(Empresa empresa)
        {
            empresas.Add(empresa);
        }

        public void addContaRubricaDF(ContaDF contaRubrica)
        {
            contasDF.Add(contaRubrica);
        }

    }
}
