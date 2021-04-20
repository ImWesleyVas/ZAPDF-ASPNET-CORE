using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAPNET.DemoFina.Empresa;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Contas
{
    public class ModeloDF
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        private List<IEmpresa> empresas = new List<IEmpresa>();
        private List<ContaDF> contasDF = new List<ContaDF>();

        public ModeloDF()
        {

        }

        public ModeloDF(int id, string demonstracao)
        {
            Id = id;
            Nome = demonstracao;
        }


        public void addEmpresa(IEmpresa empresa)
        {
            empresas.Add(empresa);
        }

        public void addContaRubricaDF(ContaDF contaRubrica)
        {
            contasDF.Add(contaRubrica);
        }

    }
}
