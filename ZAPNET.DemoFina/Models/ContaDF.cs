using System.ComponentModel.DataAnnotations;

namespace ZAPNET.DemoFina.Models
{
    public class ContaDF : Conta
    {
        [Range(1, 99999999)]
        public int CodigoContaDF { get; set; }

        public ModeloDF ModeloDF { get; set; }

        public PeriodoRef PeriodoRef { get; set; }

        public ContaDF()
        {
        }

        public ContaDF(int id, int codigoConta, string descricao, string tipo, string natureza, int nivel, string classe, ModeloDF modeloDF, PeriodoRef periodoRef) 
            : base(id, descricao, tipo, natureza, nivel, classe)
        {
            CodigoContaDF = codigoConta;
            ModeloDF = modeloDF;
            PeriodoRef = periodoRef;
        }

    }
}
