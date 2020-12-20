
namespace ZAPNET.DemoFina.Models
{
    public class ContaDF : Conta
    {

        public int CodigoContaDF { get; set; }

        private ModeloDF modeloDF = new ModeloDF();

        public ContaDF()
        {
        }

        public ContaDF(int id, int codigoConta, string descricao, string tipo, string natureza, int nivel, string classe) : base(id, descricao, tipo, natureza, nivel, classe)
        {
            CodigoContaDF = codigoConta;
        }
    }
}
