using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class ModeloDF
    {
        public int Id { get; set; }
        public string Nome { get; set; }

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
