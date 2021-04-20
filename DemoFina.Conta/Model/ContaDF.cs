using System.ComponentModel.DataAnnotations;
using ZAPNET.DemoFina.Util;

namespace ZAPNET.DemoFina.Conta.Models
{
    public class ContaDF : Conta
    {
        [Range(1, 99999999)]
        public int CodigoContaDF { get; set; }

        

        public ContaDF()
        {
        }

        public ContaDF(int id, int codigoConta, string descricao, string tipo, string natureza, int nivel, string classe) : base(id, descricao, tipo, natureza, nivel, classe)
        {
            CodigoContaDF = codigoConta;
            
        }

    }
}
