using System.ComponentModel.DataAnnotations;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Contas
{
    public class ContaDF : Conta, IContaDF
    {
        [Range(1, 99999999)]
        public int CodigoContaDF { get; set; }

        public IModeloDF ModeloDF { get; set; }

        public ContaDF()
        {
        }

        public ContaDF(int id, int codigoConta, string descricao, string tipo, string natureza, int nivel, string classe, IModeloDF modeloDF) : base(id, descricao, tipo, natureza, nivel, classe)
        {
            CodigoContaDF = codigoConta;
            ModeloDF = modeloDF;
        }

    }
}
