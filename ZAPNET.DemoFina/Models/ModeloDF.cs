using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZAPNET.DemoFina.Models
{
    public class ModeloDF
    {
        public int Id { get; set; }
        public string Demonstracao { get; set; }

        private List<Empresa> empresas = new List<Empresa>();
        private List<ContaRubricaDF> contasRubricasDF = new List<ContaRubricaDF>();

        public ModeloDF()
        {

        }

        public ModeloDF(int id, string demonstracao)
        {
            Id = id;
            Demonstracao = demonstracao;
        }


        public void addEmpresa(Empresa empresa)
        {
            empresas.Add(empresa);
        }

        public void addContaRubricaDF(ContaRubricaDF contaRubrica)
        {
            contasRubricasDF.Add(contaRubrica);
        }

    }
}
