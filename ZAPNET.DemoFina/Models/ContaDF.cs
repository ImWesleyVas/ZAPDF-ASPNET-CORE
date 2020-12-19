
namespace ZAPNET.DemoFina.Models
{
    public class ContaDF : Conta
    {

        private ModeloDF modeloDF = new ModeloDF();

        public ContaDF()
        {
        }

        public ContaDF(int id, string descricao, string tipo, string natureza, int nivel, string classe) : base(id, descricao, tipo, natureza, nivel, classe)
        {
        }
    }
}
